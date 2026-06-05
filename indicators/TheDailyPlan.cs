using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using ATAS.Indicators;
using ATAS.Indicators.Drawing;
using OFT.Rendering.Context;
using OFT.Rendering.Settings;
using OFT.Rendering.Tools;

// ═══════════════════════════════════════════════════════════════════════════
//  THE DAILY PLAN
//
//  Draws 5 previous-day levels on the chart (Settlement, Equilibrium,
//  A-session POC, B-session POC, A+B combined POC) and renders a textual
//  analysis block (OTF direction, prev-RTH value relation, gap, quartile
//  close, excess, session open, overnight inventory).
//
//  EXACT VALUES ONLY — no OHLCV approximations of volume profile, no
//  fallbacks. If the required footprint data is missing, calculation
//  fails loudly.
//
//  Data assumptions (documented for the reader):
//    * Settlement = close of the last intraday bar of the previous
//      trading session (as identified by ATAS's IsNewSession). For CME
//      futures feeds that write the official settlement into that bar's
//      close (Rithmic, CQG, most broker feeds), this IS the exact
//      settlement. If the feed does NOT do this, the user should switch
//      settlement source — see the notes at the top of the repo.
//    * POC / VAH / VAL come from IndicatorCandle footprint via
//      GetAllPriceLevels(). If the chart has no cluster data loaded,
//      OnCalculate throws to surface the problem immediately.
//
//  Reuses TablePosition enum from AtasOnSteroids.cs (same project).
// ═══════════════════════════════════════════════════════════════════════════

[DisplayName("The Daily Plan")]
[Category("Custom")]
public class TheDailyPlan : Indicator
{
    #region Shared enums

    public enum LineStyleOption
    {
        Solid,
        Dash,
        Dot,
    }

    public enum PocTieBreak
    {
        /// <summary>Lowest price wins (conservative).</summary>
        Lowest,
        /// <summary>Highest price wins.</summary>
        Highest,
        /// <summary>Price closest to session VWAP wins.</summary>
        ClosestToVwap,
    }

    #endregion

    #region Per-session aggregate

    /// <summary>
    /// Everything we need to remember about one trading session so the
    /// next day's indicator output can be computed without re-scanning
    /// all bars.
    /// </summary>
    private sealed class SessionData
    {
        public DateTime StartTime;     // time of first bar in session (exchange tz)
        public int FirstBar;           // index of first bar in session
        public int LastBar;            // index of last bar in session

        public decimal High = decimal.MinValue;
        public decimal Low  = decimal.MaxValue;
        public decimal Open;           // open of FirstBar
        public decimal Close;          // close of LastBar (= "settlement" per data assumption)

        // Volume-by-price, accumulated exactly from footprint price levels.
        // key = price, value = total volume at that price within the filter.
        public readonly Dictionary<decimal, decimal> VolByPrice_A  = new();
        public readonly Dictionary<decimal, decimal> VolByPrice_B  = new();
        public readonly Dictionary<decimal, decimal> VolByPrice_AB = new();
        public readonly Dictionary<decimal, decimal> VolByPrice_Full = new();

        // Running VWAP numerator/denominator across the full session
        // (used for POC tie-break and for reporting if we ever want it).
        public decimal VwapNum; // Σ price * volume
        public decimal VwapDen; // Σ volume

        // First bar whose time >= 09:30 ET on this session date (the "RTH open" bar).
        // Used for gap and overnight-inventory calculations.
        public int? RthOpenBar;
        public decimal? RthOpenPrice;

        public bool HasVolumeA  => VolByPrice_A.Count  > 0;
        public bool HasVolumeB  => VolByPrice_B.Count  > 0;
        public bool HasVolumeAB => VolByPrice_AB.Count > 0;
        public bool HasVolumeFull => VolByPrice_Full.Count > 0;
    }

    #endregion

    #region State

    // Only two sessions are ever needed: the one in progress and the
    // previous one (the one whose levels we draw).
    private SessionData _current;
    private SessionData _previous;
    private SessionData _priorToPrevious; // for prev-RTH-vs-prior Value comparison

    // Rolling history of completed sessions for OTF analysis.
    // Kept small — we only need the last ~90 daily sessions to build
    // monthly OTF (~3 monthly bars).
    private readonly List<SessionData> _sessionHistory = new();
    private const int MaxSessionHistory = 120;

    private int _lastProcessedBar = -1;

    // ATAS LineSeries — rendered natively by ATAS as a horizontal line
    // across the whole chart WITH a price badge on the Y-axis and the
    // series Name as an inline label at the right edge. This is the
    // convention that matches the user's screenshot (pdval / rwpoc /
    // nkey style).
    private readonly LineSeries _settleLine = new("Settlement")   { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _eqLine     = new("Equilibrium")  { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _aPocLine   = new("A-POC")        { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _bPocLine   = new("B-POC")        { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _abPocLine  = new("AB-POC")       { Value = 0, UseScale = true, IsHidden = true };

    // Per-line "have we computed a value from a finalized session yet"
    // flags. Kept separate from Value==0 so a legitimate price of 0 (or
    // a line that was already populated but recomputed to null) can't
    // accidentally hide the line.
    private bool _settleHasValue, _eqHasValue, _aPocHasValue, _bPocHasValue, _abPocHasValue;

    // Rendering
    private readonly RenderFont _headerFont     = new("Arial", 13);
    private readonly RenderFont _bodyFont       = new("Consolas", 11);
    private readonly RenderFont _sectionFont    = new("Arial", 10);
    private readonly RenderFont _lineLabelFont  = new("Arial", 10);
    private readonly RenderFont _axisBadgeFont  = new("Arial", 10);

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  SETTINGS
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Master Toggles

    private bool _showSettlement = true;
    [Display(Name = "Show Settlement",      GroupName = "0) Master Toggles", Order = 1)]
    public bool ShowSettlement { get => _showSettlement; set { _showSettlement = value; RecalculateValues(); } }

    private bool _showEquilibrium = true;
    [Display(Name = "Show Equilibrium",     GroupName = "0) Master Toggles", Order = 2)]
    public bool ShowEquilibrium { get => _showEquilibrium; set { _showEquilibrium = value; RecalculateValues(); } }

    private bool _showAPoc = true;
    [Display(Name = "Show A-Session POC",   GroupName = "0) Master Toggles", Order = 3)]
    public bool ShowAPoc { get => _showAPoc; set { _showAPoc = value; RecalculateValues(); } }

    private bool _showBPoc = true;
    [Display(Name = "Show B-Session POC",   GroupName = "0) Master Toggles", Order = 4)]
    public bool ShowBPoc { get => _showBPoc; set { _showBPoc = value; RecalculateValues(); } }

    private bool _showAbPoc = true;
    [Display(Name = "Show A+B POC",         GroupName = "0) Master Toggles", Order = 5)]
    public bool ShowAbPoc { get => _showAbPoc; set { _showAbPoc = value; RecalculateValues(); } }

    private bool _showAnalysis = true;
    [Display(Name = "Show Analysis Block",  GroupName = "0) Master Toggles", Order = 6)]
    public bool ShowAnalysis { get => _showAnalysis; set { _showAnalysis = value; RecalculateValues(); } }

    #endregion

    #region Settings — Day & Sessions

    // All times are exchange-local (same zone as Candle.Time on the chart).
    // Defaults match CME equity-index and BTC futures:
    //   A-session  = 09:30–10:00 ET  (opening drive window)
    //   B-session  = 10:00–10:30 ET  (second window)
    //   RTH close  = 16:00 ET        (used for gap calc)
    // Trading-day boundaries are taken from ATAS's IsNewSession.

    private TimeSpan _aStart = new(9, 30, 0);
    [Display(Name = "A-Session Start (HH:mm:ss)", GroupName = "1) Sessions", Order = 10)]
    public TimeSpan ASessionStart { get => _aStart; set { _aStart = value; RecalculateValues(); } }

    private TimeSpan _aEnd = new(10, 0, 0);
    [Display(Name = "A-Session End (HH:mm:ss)",   GroupName = "1) Sessions", Order = 11)]
    public TimeSpan ASessionEnd { get => _aEnd; set { _aEnd = value; RecalculateValues(); } }

    private TimeSpan _bStart = new(10, 0, 0);
    [Display(Name = "B-Session Start (HH:mm:ss)", GroupName = "1) Sessions", Order = 12)]
    public TimeSpan BSessionStart { get => _bStart; set { _bStart = value; RecalculateValues(); } }

    private TimeSpan _bEnd = new(10, 30, 0);
    [Display(Name = "B-Session End (HH:mm:ss)",   GroupName = "1) Sessions", Order = 13)]
    public TimeSpan BSessionEnd { get => _bEnd; set { _bEnd = value; RecalculateValues(); } }

    private TimeSpan _rthOpen = new(9, 30, 0);
    [Display(Name = "RTH Open Time (HH:mm:ss)",   GroupName = "1) Sessions", Order = 14,
        Description = "Used to identify the session-open bar for gap / overnight-inventory calcs.")]
    public TimeSpan RthOpenTime { get => _rthOpen; set { _rthOpen = value; RecalculateValues(); } }

    #endregion

    #region Settings — Level styling (one group per line)

    // Settlement
    private Color _settleColor = Color.FromArgb(255, 255, 200, 0);
    [Display(Name = "Color",  GroupName = "2) Settlement Line", Order = 20)]
    public Color SettleColor { get => _settleColor; set { _settleColor = value; RecalculateValues(); } }
    private LineStyleOption _settleStyle = LineStyleOption.Solid;
    [Display(Name = "Style",  GroupName = "2) Settlement Line", Order = 21)]
    public LineStyleOption SettleStyle { get => _settleStyle; set { _settleStyle = value; RecalculateValues(); } }
    private int _settleWidth = 1;
    [Display(Name = "Width",  GroupName = "2) Settlement Line", Order = 22)]
    [Range(1, 5)]
    public int SettleWidth { get => _settleWidth; set { _settleWidth = value; RecalculateValues(); } }
    private string _settleLabel = "pdSettle";
    [Display(Name = "Label",  GroupName = "2) Settlement Line", Order = 23)]
    public string SettleLabel { get => _settleLabel; set { _settleLabel = value ?? ""; RecalculateValues(); } }

    // Equilibrium
    private Color _eqColor = Color.FromArgb(255, 180, 180, 180);
    [Display(Name = "Color",  GroupName = "3) Equilibrium Line", Order = 30)]
    public Color EqColor { get => _eqColor; set { _eqColor = value; RecalculateValues(); } }
    private LineStyleOption _eqStyle = LineStyleOption.Dash;
    [Display(Name = "Style",  GroupName = "3) Equilibrium Line", Order = 31)]
    public LineStyleOption EqStyle { get => _eqStyle; set { _eqStyle = value; RecalculateValues(); } }
    private int _eqWidth = 1;
    [Display(Name = "Width",  GroupName = "3) Equilibrium Line", Order = 32)]
    [Range(1, 5)]
    public int EqWidth { get => _eqWidth; set { _eqWidth = value; RecalculateValues(); } }
    private string _eqLabel = "pdEq";
    [Display(Name = "Label",  GroupName = "3) Equilibrium Line", Order = 33)]
    public string EqLabel { get => _eqLabel; set { _eqLabel = value ?? ""; RecalculateValues(); } }

    // A POC
    private Color _aPocColor = Color.FromArgb(255, 0, 200, 255);
    [Display(Name = "Color",  GroupName = "4) A-Session POC",   Order = 40)]
    public Color APocColor { get => _aPocColor; set { _aPocColor = value; RecalculateValues(); } }
    private LineStyleOption _aPocStyle = LineStyleOption.Solid;
    [Display(Name = "Style",  GroupName = "4) A-Session POC",   Order = 41)]
    public LineStyleOption APocStyle { get => _aPocStyle; set { _aPocStyle = value; RecalculateValues(); } }
    private int _aPocWidth = 1;
    [Display(Name = "Width",  GroupName = "4) A-Session POC",   Order = 42)]
    [Range(1, 5)]
    public int APocWidth { get => _aPocWidth; set { _aPocWidth = value; RecalculateValues(); } }
    private string _aPocLabel = "pdPOC[A]";
    [Display(Name = "Label",  GroupName = "4) A-Session POC",   Order = 43)]
    public string APocLabel { get => _aPocLabel; set { _aPocLabel = value ?? ""; RecalculateValues(); } }

    // B POC
    private Color _bPocColor = Color.FromArgb(255, 255, 120, 0);
    [Display(Name = "Color",  GroupName = "5) B-Session POC",   Order = 50)]
    public Color BPocColor { get => _bPocColor; set { _bPocColor = value; RecalculateValues(); } }
    private LineStyleOption _bPocStyle = LineStyleOption.Solid;
    [Display(Name = "Style",  GroupName = "5) B-Session POC",   Order = 51)]
    public LineStyleOption BPocStyle { get => _bPocStyle; set { _bPocStyle = value; RecalculateValues(); } }
    private int _bPocWidth = 1;
    [Display(Name = "Width",  GroupName = "5) B-Session POC",   Order = 52)]
    [Range(1, 5)]
    public int BPocWidth { get => _bPocWidth; set { _bPocWidth = value; RecalculateValues(); } }
    private string _bPocLabel = "pdPOC[B]";
    [Display(Name = "Label",  GroupName = "5) B-Session POC",   Order = 53)]
    public string BPocLabel { get => _bPocLabel; set { _bPocLabel = value ?? ""; RecalculateValues(); } }

    // A+B POC
    private Color _abPocColor = Color.FromArgb(255, 255, 255, 255);
    [Display(Name = "Color",  GroupName = "6) A+B POC",         Order = 60)]
    public Color AbPocColor { get => _abPocColor; set { _abPocColor = value; RecalculateValues(); } }
    private LineStyleOption _abPocStyle = LineStyleOption.Solid;
    [Display(Name = "Style",  GroupName = "6) A+B POC",         Order = 61)]
    public LineStyleOption AbPocStyle { get => _abPocStyle; set { _abPocStyle = value; RecalculateValues(); } }
    private int _abPocWidth = 2;
    [Display(Name = "Width",  GroupName = "6) A+B POC",         Order = 62)]
    [Range(1, 5)]
    public int AbPocWidth { get => _abPocWidth; set { _abPocWidth = value; RecalculateValues(); } }
    private string _abPocLabel = "pdPOC[AB]";
    [Display(Name = "Label",  GroupName = "6) A+B POC",         Order = 63)]
    public string AbPocLabel { get => _abPocLabel; set { _abPocLabel = value ?? ""; RecalculateValues(); } }

    private PocTieBreak _pocTieBreak = PocTieBreak.ClosestToVwap;
    [Display(Name = "POC Tie-break", GroupName = "6) A+B POC", Order = 64,
        Description = "Rule when two or more prices share the max volume.")]
    public PocTieBreak PocTieBreakRule { get => _pocTieBreak; set { _pocTieBreak = value; RecalculateValues(); } }

    #endregion

    #region Settings — Analysis Block

    private TablePosition _analysisPosition = TablePosition.TopLeft;
    [Display(Name = "Position", GroupName = "7) Analysis Block", Order = 70)]
    public TablePosition AnalysisPosition { get => _analysisPosition; set { _analysisPosition = value; RecalculateValues(); } }

    private int _quartileDivisions = 4;
    [Display(Name = "Quartile Divisions", GroupName = "7) Analysis Block", Order = 71)]
    [Range(2, 10)]
    public int QuartileDivisions { get => _quartileDivisions; set { _quartileDivisions = value; RecalculateValues(); } }

    private decimal _valueAreaPct = 70m;
    [Display(Name = "Value Area %", GroupName = "7) Analysis Block", Order = 72,
        Description = "Portion of session volume centered on POC that defines VAH/VAL.")]
    [Range(50.0, 90.0)]
    public decimal ValueAreaPct { get => _valueAreaPct; set { _valueAreaPct = value; RecalculateValues(); } }

    private int _otfLookback = 3;
    [Display(Name = "OTF Lookback (bars)", GroupName = "7) Analysis Block", Order = 73,
        Description = "Number of consecutive daily/weekly/monthly bars that must make HH+HL (up) or LL+LH (down).")]
    [Range(2, 10)]
    public int OtfLookback { get => _otfLookback; set { _otfLookback = value; RecalculateValues(); } }

    private decimal _excessVolumePct = 5m;
    [Display(Name = "Excess Max Volume %", GroupName = "7) Analysis Block", Order = 74,
        Description = "Prev-RTH high is flagged as a 'poor high' if the volume at that top price is <= this percent of the session's max per-price volume. Mirrored for low.")]
    [Range(0.0, 50.0)]
    public decimal ExcessVolumePct { get => _excessVolumePct; set { _excessVolumePct = value; RecalculateValues(); } }

    private Color _analysisTextColor = Color.FromArgb(255, 230, 230, 240);
    [Display(Name = "Text Color", GroupName = "7) Analysis Block", Order = 75)]
    public Color AnalysisTextColor { get => _analysisTextColor; set { _analysisTextColor = value; RecalculateValues(); } }

    private Color _analysisBgColor = Color.FromArgb(225, 10, 10, 18);
    [Display(Name = "Background Color", GroupName = "7) Analysis Block", Order = 76)]
    public Color AnalysisBgColor { get => _analysisBgColor; set { _analysisBgColor = value; RecalculateValues(); } }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  CONSTRUCTOR
    // ═══════════════════════════════════════════════════════════════

    public TheDailyPlan()
    {
        Panel = IndicatorDataProvider.CandlesPanel;
        EnableCustomDrawing = true;
        SubscribeToDrawingEvents(DrawingLayouts.Final);

        // Hidden dummy data series so ATAS doesn't complain about an
        // indicator with no series.
        DataSeries.Add(new ValueDataSeries("TheDailyPlan_dummy") { IsHidden = true });

        // Register the 5 horizontal lines with ATAS so the engine draws
        // them natively and paints the Y-axis price badge.
        LineSeries.Add(_settleLine);
        LineSeries.Add(_eqLine);
        LineSeries.Add(_aPocLine);
        LineSeries.Add(_bPocLine);
        LineSeries.Add(_abPocLine);
    }

    // ═══════════════════════════════════════════════════════════════
    //  CALCULATION
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Fires once at the start of every recalc (setting change, data
    /// update, initial load). This is THE correct place to wipe state —
    /// not inside OnCalculate where bar==0 checks race with live ticks.
    /// </summary>
    protected override void OnRecalculate()
    {
        _current = null;
        _previous = null;
        _priorToPrevious = null;
        _sessionHistory.Clear();
        _lastProcessedBar = -1;
        _settleHasValue = _eqHasValue = _aPocHasValue = _bPocHasValue = _abPocHasValue = false;
    }

    protected override void OnCalculate(int bar, decimal value)
    {
        // Skip bars we've already accumulated. ATAS re-calls OnCalculate
        // on the current (live) bar on every tick — fold it in once, then
        // just keep the line-series display in sync with any settings
        // changes the user makes.
        if (bar <= _lastProcessedBar)
        {
            SyncLineSeriesFromSettings();
            return;
        }

        var candle = GetCandle(bar);

        // Session boundary: close the previous session, start a new one.
        bool newSession = bar == 0 || IsNewSession(bar);
        if (newSession)
        {
            if (_current != null)
            {
                _current.LastBar = bar - 1;
                FinalizeCompletedSession(_current);

                // Shift history: _previous → _priorToPrevious, _current → _previous
                _priorToPrevious = _previous;
                _previous = _current;

                _sessionHistory.Add(_current);
                if (_sessionHistory.Count > MaxSessionHistory)
                    _sessionHistory.RemoveAt(0);

                // Update the line values now that _previous is known.
                UpdateLineValuesFromPrevious();
            }

            _current = new SessionData
            {
                StartTime = candle.Time,
                FirstBar  = bar,
                LastBar   = bar,
                Open      = candle.Open,
            };
        }

        // Accumulate bar into current session.
        if (_current != null)
            AccumulateBar(_current, bar, candle);

        _lastProcessedBar = bar;
        SyncLineSeriesFromSettings();
    }

    /// <summary>
    /// Recompute the 5 level prices from the completed previous session
    /// and push them into the LineSeries so ATAS draws them.
    /// </summary>
    private void UpdateLineValuesFromPrevious()
    {
        var prev = _previous;
        if (prev == null) return;

        _settleLine.Value = prev.Close;
        _settleHasValue = true;

        if (prev.High > decimal.MinValue && prev.Low < decimal.MaxValue)
        {
            _eqLine.Value = (prev.High + prev.Low) / 2m;
            _eqHasValue = true;
        }

        decimal vwap = prev.VwapDen > 0 ? prev.VwapNum / prev.VwapDen : 0m;
        var aPoc  = ComputePoc(prev.VolByPrice_A,  vwap);
        var bPoc  = ComputePoc(prev.VolByPrice_B,  vwap);
        var abPoc = ComputePoc(prev.VolByPrice_AB, vwap);

        if (aPoc.HasValue)  { _aPocLine.Value  = aPoc.Value;  _aPocHasValue  = true; }
        if (bPoc.HasValue)  { _bPocLine.Value  = bPoc.Value;  _bPocHasValue  = true; }
        if (abPoc.HasValue) { _abPocLine.Value = abPoc.Value; _abPocHasValue = true; }
    }

    /// <summary>
    /// Mirror user-facing settings onto the line series every call so
    /// color/style/width/label changes apply immediately without a full
    /// recalc, and so master toggles (IsHidden) work.
    /// </summary>
    private void SyncLineSeriesFromSettings()
    {
        ApplyLine(_settleLine, _showSettlement && _settleHasValue, _settleLabel, _settleColor, _settleStyle, _settleWidth);
        ApplyLine(_eqLine,     _showEquilibrium && _eqHasValue,    _eqLabel,     _eqColor,     _eqStyle,     _eqWidth);
        ApplyLine(_aPocLine,   _showAPoc        && _aPocHasValue,  _aPocLabel,   _aPocColor,   _aPocStyle,   _aPocWidth);
        ApplyLine(_bPocLine,   _showBPoc        && _bPocHasValue,  _bPocLabel,   _bPocColor,   _bPocStyle,   _bPocWidth);
        ApplyLine(_abPocLine,  _showAbPoc       && _abPocHasValue, _abPocLabel,  _abPocColor,  _abPocStyle,  _abPocWidth);
    }

    private static void ApplyLine(LineSeries ls, bool visible, string label, Color color, LineStyleOption style, int width)
    {
        ls.IsHidden = !visible;
        if (!string.IsNullOrWhiteSpace(label))
            ls.Name = label;
        ls.Color = color;
        ls.Width = width;
        ls.LineDashStyle = style switch
        {
            LineStyleOption.Dash => LineDashStyle.Dash,
            LineStyleOption.Dot  => LineDashStyle.Dot,
            _                    => LineDashStyle.Solid,
        };
    }

    /// <summary>
    /// Fold a single intraday bar into the running session aggregate.
    /// Pulls exact price-by-price volume from footprint; throws if
    /// the chart has no cluster data.
    /// </summary>
    private void AccumulateBar(SessionData s, int bar, IndicatorCandle candle)
    {
        s.LastBar = bar;
        s.Close   = candle.Close;
        if (candle.High > s.High) s.High = candle.High;
        if (candle.Low  < s.Low)  s.Low  = candle.Low;

        // Is this bar inside the RTH open minute? Capture it for gap calcs.
        if (s.RthOpenBar == null && BarInWindow(candle, _rthOpen, _rthOpen + TimeSpan.FromMinutes(1)))
        {
            s.RthOpenBar   = bar;
            s.RthOpenPrice = candle.Open;
        }

        bool inA  = BarInWindow(candle, _aStart, _aEnd);
        bool inB  = BarInWindow(candle, _bStart, _bEnd);
        bool inAB = inA || inB;

        // Footprint — hard requirement. If we can't get price levels
        // from a bar that should have them (i.e. volume > 0), throw.
        IEnumerable<PriceVolumeInfo> levels = null;
        try { levels = candle.GetAllPriceLevels(); }
        catch { levels = null; }

        if (levels == null)
        {
            if (candle.Volume > 0)
                throw new InvalidOperationException(
                    $"[The Daily Plan] Footprint data not available on bar {bar} " +
                    $"(time {candle.Time:u}). Load cluster/footprint data on the chart " +
                    $"or remove this indicator. Exact POC cannot be computed without it.");
            return; // zero-volume bar, nothing to add
        }

        foreach (var pv in levels)
        {
            if (pv == null) continue;
            decimal px = pv.Price;
            decimal vol = pv.Volume;
            if (vol <= 0) continue;

            AddVol(s.VolByPrice_Full, px, vol);
            s.VwapNum += px * vol;
            s.VwapDen += vol;

            if (inA)  AddVol(s.VolByPrice_A,  px, vol);
            if (inB)  AddVol(s.VolByPrice_B,  px, vol);
            if (inAB) AddVol(s.VolByPrice_AB, px, vol);
        }
    }

    private static void AddVol(Dictionary<decimal, decimal> map, decimal price, decimal vol)
    {
        if (map.TryGetValue(price, out var cur))
            map[price] = cur + vol;
        else
            map[price] = vol;
    }

    /// <summary>
    /// Called once when a session transitions from "current" to "previous".
    /// Hook for any per-session post-processing.
    /// </summary>
    private void FinalizeCompletedSession(SessionData s)
    {
        // Nothing heavy to do here — all per-row analytics are computed
        // lazily in OnRender, so that setting changes don't require a
        // full recalc.
        _ = s;
    }

    private bool BarInWindow(IndicatorCandle candle, TimeSpan startOfDay, TimeSpan endOfDay)
    {
        if (startOfDay == endOfDay) return false;

        var t = candle.Time.TimeOfDay;

        // Windows never wrap across midnight for this indicator's use
        // (A/B sessions are both intraday). Guard anyway.
        if (endOfDay > startOfDay)
            return t >= startOfDay && t < endOfDay;
        return t >= startOfDay || t < endOfDay;
    }

    // ═══════════════════════════════════════════════════════════════
    //  PROFILE ANALYTICS — POC / VAH / VAL / Excess
    // ═══════════════════════════════════════════════════════════════

    #region Profile analytics

    /// <summary>
    /// Compute POC from a price→volume map. Returns null if empty.
    /// Ties broken by the configured rule.
    /// </summary>
    private decimal? ComputePoc(Dictionary<decimal, decimal> map, decimal vwap)
    {
        if (map == null || map.Count == 0) return null;

        decimal maxVol = map.Values.Max();
        var ties = map.Where(kv => kv.Value == maxVol).Select(kv => kv.Key).ToList();
        if (ties.Count == 1) return ties[0];

        return _pocTieBreak switch
        {
            PocTieBreak.Lowest        => ties.Min(),
            PocTieBreak.Highest       => ties.Max(),
            PocTieBreak.ClosestToVwap => ties.OrderBy(p => Math.Abs(p - vwap)).First(),
            _                         => ties.Min(),
        };
    }

    /// <summary>
    /// Classic Market-Profile value-area expansion: start at POC and
    /// expand up/down, each step picking the side with the larger
    /// combined two-ticks volume, until the accumulated volume reaches
    /// ValueAreaPct of the total.
    /// </summary>
    private (decimal val, decimal vah)? ComputeValueArea(Dictionary<decimal, decimal> map, decimal poc)
    {
        if (map == null || map.Count == 0) return null;

        // Sort prices ascending for index-based neighbour access.
        var prices = map.Keys.OrderBy(p => p).ToList();
        int pocIdx = prices.IndexOf(poc);
        if (pocIdx < 0) return null;

        decimal totalVol = map.Values.Sum();
        if (totalVol <= 0) return null;
        decimal target = totalVol * (_valueAreaPct / 100m);

        decimal accum = map[poc];
        int lo = pocIdx;
        int hi = pocIdx;

        while (accum < target && (lo > 0 || hi < prices.Count - 1))
        {
            decimal upPair = 0;
            if (hi < prices.Count - 1)
            {
                upPair += map[prices[hi + 1]];
                if (hi + 2 < prices.Count) upPair += map[prices[hi + 2]];
            }
            decimal downPair = 0;
            if (lo > 0)
            {
                downPair += map[prices[lo - 1]];
                if (lo - 2 >= 0) downPair += map[prices[lo - 2]];
            }

            bool goUp;
            if (hi >= prices.Count - 1) goUp = false;
            else if (lo <= 0)           goUp = true;
            else                        goUp = upPair >= downPair;

            if (goUp)
            {
                hi = Math.Min(prices.Count - 1, hi + 2);
                accum += upPair;
            }
            else
            {
                lo = Math.Max(0, lo - 2);
                accum += downPair;
            }

            if (upPair == 0 && downPair == 0) break; // safety
        }

        return (prices[lo], prices[hi]);
    }

    /// <summary>
    /// Detect poor high / poor low by volume-at-extreme heuristic:
    /// the extreme price's volume is at most N% of the session's max
    /// per-price volume.
    /// </summary>
    private (bool poorHigh, bool poorLow) DetectExcess(SessionData s)
    {
        if (!s.HasVolumeFull) return (false, false);

        decimal maxVol = s.VolByPrice_Full.Values.Max();
        if (maxVol <= 0) return (false, false);

        decimal threshold = maxVol * (_excessVolumePct / 100m);
        decimal topPrice = s.VolByPrice_Full.Keys.Max();
        decimal botPrice = s.VolByPrice_Full.Keys.Min();

        bool poorHigh = s.VolByPrice_Full[topPrice] <= threshold;
        bool poorLow  = s.VolByPrice_Full[botPrice] <= threshold;
        return (poorHigh, poorLow);
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  OTF — one-time framing direction
    // ═══════════════════════════════════════════════════════════════

    #region OTF

    private enum OtfDirection { Up, Down, Balance, NotEnoughData }

    /// <summary>
    /// Aggregate completed daily sessions into synthetic bars at the
    /// requested period, then apply OTF rule to the last N.
    /// </summary>
    private OtfDirection ComputeOtf(string timeframe)
    {
        var daily = _sessionHistory;
        if (daily.Count < 2) return OtfDirection.NotEnoughData;

        List<(decimal high, decimal low)> bars = timeframe switch
        {
            "daily"   => daily.Select(d => (d.High, d.Low)).ToList(),
            "weekly"  => GroupBy(daily, d => IsoWeekKey(d.StartTime)),
            "monthly" => GroupBy(daily, d => new DateTime(d.StartTime.Year, d.StartTime.Month, 1).Ticks),
            _         => new List<(decimal, decimal)>(),
        };

        int n = _otfLookback;
        if (bars.Count < n) return OtfDirection.NotEnoughData;

        var tail = bars.Skip(bars.Count - n).ToList();

        bool allUp = true;
        bool allDown = true;
        for (int i = 1; i < tail.Count; i++)
        {
            if (!(tail[i].high > tail[i - 1].high && tail[i].low > tail[i - 1].low)) allUp = false;
            if (!(tail[i].high < tail[i - 1].high && tail[i].low < tail[i - 1].low)) allDown = false;
        }
        if (allUp)   return OtfDirection.Up;
        if (allDown) return OtfDirection.Down;
        return OtfDirection.Balance;
    }

    private static long IsoWeekKey(DateTime dt)
    {
        // Group by ISO-year + ISO-week
        var cal = System.Globalization.ISOWeek.GetWeekOfYear(dt);
        var yr  = System.Globalization.ISOWeek.GetYear(dt);
        return (long)yr * 100 + cal;
    }

    private static List<(decimal high, decimal low)> GroupBy<TKey>(List<SessionData> sessions, Func<SessionData, TKey> keyFn)
    {
        var result = new List<(decimal, decimal)>();
        TKey curKey = default;
        bool hasKey = false;
        decimal h = decimal.MinValue, l = decimal.MaxValue;

        foreach (var s in sessions)
        {
            var k = keyFn(s);
            if (!hasKey || !Equals(k, curKey))
            {
                if (hasKey) result.Add((h, l));
                curKey = k;
                hasKey = true;
                h = s.High;
                l = s.Low;
            }
            else
            {
                if (s.High > h) h = s.High;
                if (s.Low  < l) l = s.Low;
            }
        }
        if (hasKey) result.Add((h, l));
        return result;
    }

    private static string OtfString(OtfDirection d) => d switch
    {
        OtfDirection.Up            => "OTFU",
        OtfDirection.Down          => "OTFD",
        OtfDirection.Balance       => "OTFB",
        _                          => "n/a",
    };

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  RENDERING
    // ═══════════════════════════════════════════════════════════════

    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
        // The 5 horizontal levels are drawn by ATAS itself from the
        // LineSeries registered in the constructor. We overlay the
        // inline name label and the Y-axis price badge on top because
        // LineSeries doesn't render those natively in this SDK version.
        if (_previous == null) return;

        DrawLineLabels(context);

        if (_showAnalysis)
            DrawAnalysis(context);
    }

    #region Line labels + axis badges

    private void DrawLineLabels(RenderContext context)
    {
        // One entry per line in the same order they appear on the chart.
        var items = new (bool on, bool hasVal, decimal price, Color color, string label)[]
        {
            (_showSettlement,  _settleHasValue, _settleLine.Value, _settleColor, _settleLabel),
            (_showEquilibrium, _eqHasValue,     _eqLine.Value,     _eqColor,     _eqLabel),
            (_showAPoc,        _aPocHasValue,   _aPocLine.Value,   _aPocColor,   _aPocLabel),
            (_showBPoc,        _bPocHasValue,   _bPocLine.Value,   _bPocColor,   _bPocLabel),
            (_showAbPoc,       _abPocHasValue,  _abPocLine.Value,  _abPocColor,  _abPocLabel),
        };

        int rightPad = 2;
        int xRight   = context.ClipBounds.Right - rightPad;

        foreach (var it in items)
        {
            if (!it.on || !it.hasVal) continue;

            int y = ChartInfo.PriceChartContainer.GetYByPrice(it.price, false);

            // ── Axis-style price badge (filled rectangle at right edge) ──
            string priceText = it.price.ToString(ChartInfo.StringFormat);
            var priceSize = context.MeasureString(priceText, _axisBadgeFont);
            int badgeW = priceSize.Width + 10;
            int badgeH = priceSize.Height + 3;
            int badgeX = xRight - badgeW;
            int badgeY = y - badgeH / 2;

            context.FillRectangle(it.color, new Rectangle(badgeX, badgeY, badgeW, badgeH));

            Color priceTextColor = IsDarkColor(it.color) ? Color.White : Color.Black;
            context.DrawString(priceText, _axisBadgeFont, priceTextColor,
                badgeX + 5, badgeY + 1);

            // ── Inline name label, sitting just above the line to the
            //    left of the badge (matches pdval / rwpoc / nkey style).
            if (!string.IsNullOrWhiteSpace(it.label))
            {
                var nameSize = context.MeasureString(it.label, _lineLabelFont);
                int nameX = badgeX - nameSize.Width - 4;
                int nameY = y - nameSize.Height - 1;
                context.DrawString(it.label, _lineLabelFont, it.color, nameX, nameY);
            }
        }
    }

    /// <summary>Perceptual-luminance dark-color check (for picking text color on colored badge).</summary>
    private static bool IsDarkColor(Color c) => (c.R * 299 + c.G * 587 + c.B * 114) < 128 * 1000;

    #endregion

    #region Analysis block rendering

    private void DrawAnalysis(RenderContext context)
    {
        var prev = _previous;
        if (prev == null) return;

        var rows = BuildAnalysisRows(prev);

        // Lay out.
        int pad = 10;
        int lineH = 18;
        int titleH = 22;
        int sectionGap = 6;

        // Measure widest string.
        int maxW = (int)context.MeasureString("The Daily Plan", _headerFont).Width;
        foreach (var r in rows)
        {
            int w = (int)context.MeasureString(r.text, r.kind == RowKind.Section ? _sectionFont : _bodyFont).Width;
            if (w > maxW) maxW = w;
        }

        int boxW = maxW + pad * 2;
        int boxH = titleH + pad;
        foreach (var r in rows)
        {
            boxH += r.kind == RowKind.Gap ? sectionGap : lineH;
        }
        boxH += pad;

        var bounds = context.ClipBounds;
        int x, y;
        switch (_analysisPosition)
        {
            case TablePosition.TopLeft:     x = bounds.X + 10;              y = bounds.Y + 10;               break;
            case TablePosition.TopRight:    x = bounds.Right - boxW - 10;   y = bounds.Y + 10;               break;
            case TablePosition.BottomLeft:  x = bounds.X + 10;              y = bounds.Bottom - boxH - 10;   break;
            default:                        x = bounds.Right - boxW - 10;   y = bounds.Bottom - boxH - 10;   break;
        }

        context.FillRectangle(_analysisBgColor, new Rectangle(x, y, boxW, boxH));
        context.DrawRectangle(new RenderPen(Color.FromArgb(100, 80, 80, 100), 1),
            new Rectangle(x, y, boxW, boxH));

        // Title
        context.DrawString("The Daily Plan", _headerFont, _analysisTextColor, x + pad, y + 4);

        int cy = y + titleH + 2;
        foreach (var r in rows)
        {
            if (r.kind == RowKind.Gap) { cy += sectionGap; continue; }
            var font = r.kind == RowKind.Section ? _sectionFont : _bodyFont;
            var color = r.color ?? _analysisTextColor;
            context.DrawString(r.text, font, color, x + pad, cy);
            cy += lineH;
        }
    }

    private enum RowKind { Section, Body, Gap }

    private struct AnalysisRow
    {
        public RowKind kind;
        public string text;
        public Color? color;
    }

    private List<AnalysisRow> BuildAnalysisRows(SessionData prev)
    {
        var rows = new List<AnalysisRow>();

        void Section(string t) => rows.Add(new AnalysisRow { kind = RowKind.Section, text = t, color = Color.FromArgb(255, 140, 180, 230) });
        void Body(string t)    => rows.Add(new AnalysisRow { kind = RowKind.Body,    text = t });
        void Gap()             => rows.Add(new AnalysisRow { kind = RowKind.Gap,     text = "" });

        // ── Context (OTF) ──
        Section("Context");
        Body($"  Monthly : {OtfString(ComputeOtf("monthly"))}");
        Body($"  Weekly  : {OtfString(ComputeOtf("weekly"))}");
        Body($"  Daily   : {OtfString(ComputeOtf("daily"))}");

        Gap();

        // ── Previous RTH ──
        Section("Previous RTH");

        // Value Higher/Lower/Overlapping
        string valueLine = "  Value   : n/a";
        if (_priorToPrevious != null && prev.HasVolumeFull && _priorToPrevious.HasVolumeFull)
        {
            decimal pVwap = prev.VwapDen > 0 ? prev.VwapNum / prev.VwapDen : 0m;
            decimal qVwap = _priorToPrevious.VwapDen > 0 ? _priorToPrevious.VwapNum / _priorToPrevious.VwapDen : 0m;

            var pPoc = ComputePoc(prev.VolByPrice_Full, pVwap);
            var qPoc = ComputePoc(_priorToPrevious.VolByPrice_Full, qVwap);
            if (pPoc.HasValue && qPoc.HasValue)
            {
                var pVa = ComputeValueArea(prev.VolByPrice_Full, pPoc.Value);
                var qVa = ComputeValueArea(_priorToPrevious.VolByPrice_Full, qPoc.Value);
                if (pVa.HasValue && qVa.HasValue)
                {
                    string verdict;
                    if (pVa.Value.val >= qVa.Value.vah)      verdict = "Higher";
                    else if (pVa.Value.vah <= qVa.Value.val) verdict = "Lower";
                    else                                     verdict = "Overlapping";
                    valueLine = $"  Value   : {verdict}";
                }
            }
        }
        Body(valueLine);

        // Gap (between prev-session close and current session's RTH open)
        string gapLine = "  Gap     : n/a";
        if (_current != null && _current.RthOpenPrice.HasValue)
        {
            decimal gap = _current.RthOpenPrice.Value - prev.Close;
            string dir = gap > 0 ? "Up" : (gap < 0 ? "Down" : "None");
            gapLine = $"  Gap     : {dir} {Math.Abs(gap).ToString(ChartInfo.StringFormat)}";
        }
        Body(gapLine);

        // Close quartile
        string qLine = "  Close   : n/a";
        if (prev.High > prev.Low)
        {
            decimal rng = prev.High - prev.Low;
            decimal divF = _quartileDivisions;
            int bucket = (int)Math.Floor(((prev.Close - prev.Low) / rng) * divF) + 1;
            if (bucket < 1) bucket = 1;
            if (bucket > _quartileDivisions) bucket = _quartileDivisions;
            bool extreme = bucket == 1 || bucket == _quartileDivisions;
            qLine = $"  Close   : quartile {bucket}/{_quartileDivisions}  -  extreme: {(extreme ? "YES" : "NO")}";
        }
        Body(qLine);

        // Excess
        var ex = DetectExcess(prev);
        if (ex.poorHigh || ex.poorLow)
        {
            string where =
                ex.poorHigh && ex.poorLow ? $"Poor High @ {prev.High.ToString(ChartInfo.StringFormat)} / Poor Low @ {prev.Low.ToString(ChartInfo.StringFormat)}" :
                ex.poorHigh ? $"Poor High @ {prev.High.ToString(ChartInfo.StringFormat)}" :
                              $"Poor Low @ {prev.Low.ToString(ChartInfo.StringFormat)}";
            Body($"  Excess  : {where}");
        }
        else
        {
            Body($"  Excess  : none");
        }

        Gap();

        // ── Overnight / today's open ──
        Section("Overnight");

        string openLine = "  Session Open : n/a";
        string invLine  = "  Inventory    : n/a";
        if (_current != null && _current.RthOpenPrice.HasValue)
        {
            decimal openPx = _current.RthOpenPrice.Value;

            // Inside / outside prior value
            if (prev.HasVolumeFull)
            {
                decimal pVwap = prev.VwapDen > 0 ? prev.VwapNum / prev.VwapDen : 0m;
                var pPoc = ComputePoc(prev.VolByPrice_Full, pVwap);
                if (pPoc.HasValue)
                {
                    var va = ComputeValueArea(prev.VolByPrice_Full, pPoc.Value);
                    if (va.HasValue)
                    {
                        bool inside = openPx >= va.Value.val && openPx <= va.Value.vah;
                        openLine = $"  Session Open : {(inside ? "Inside" : "Outside")} prior value";
                    }
                }
            }

            // Overnight inventory displacement vs prev settlement
            decimal disp = openPx - prev.Close;
            string side = disp > 0 ? "Long" : (disp < 0 ? "Short" : "Flat");
            invLine = $"  Inventory    : {side} ({(disp >= 0 ? "+" : "")}{disp.ToString(ChartInfo.StringFormat)} from settle)";
        }
        Body(openLine);
        Body(invLine);

        return rows;
    }

    #endregion
}
