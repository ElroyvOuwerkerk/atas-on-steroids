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

[DisplayName("CC Suite Levels")]
[Category("Custom")]
public class CCSuiteLevels : Indicator
{
    #region Fields

    private readonly ValueDataSeries _signalSeries = new("Signal") { IsHidden = true };

    // Probability tracking
    private readonly List<LevelSignalRecord> _cherryBearSignals = new();
    private readonly List<LevelSignalRecord> _cherryBullSignals = new();
    private readonly List<LevelSignalRecord> _magicBearSignals  = new();
    private readonly List<LevelSignalRecord> _magicBullSignals  = new();
    private readonly List<LevelSignalRecord> _moonshotBearSignals = new();
    private readonly List<LevelSignalRecord> _moonshotBullSignals = new();
    private readonly List<LevelSignalRecord> _ctxBearSignals    = new();
    private readonly List<LevelSignalRecord> _ctxBullSignals    = new();

    // Rendering
    private readonly RenderFont _tableHeaderFont = new("Arial", 14);
    private readonly RenderFont _tableBodyFont   = new("Consolas", 12);
    private readonly RenderFont _tableColFont    = new("Arial", 10);

    #endregion

    #region Helper

    private static string SymbolText(MarkerSymbol s)
    {
        return s switch
        {
            MarkerSymbol.TriangleUp   => "▲",
            MarkerSymbol.TriangleDown => "▼",
            MarkerSymbol.Circle       => "●",
            MarkerSymbol.Square       => "■",
            _ => "●",
        };
    }

    /// <summary>
    /// Checks whether a candle's range touches a given price level.
    /// </summary>
    private static bool CandleTouchesLevel(decimal candleLow, decimal candleHigh, decimal levelPrice)
    {
        return candleLow <= levelPrice && candleHigh >= levelPrice;
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  MASTER TOGGLES
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Master Toggles

    private bool _showCherry = true;
    [Display(Name = "Show Cherry on Top at Level", GroupName = "0) Master Toggles", Order = 1)]
    public bool ShowCherry
    {
        get => _showCherry;
        set { _showCherry = value; RecalculateValues(); }
    }

    private bool _showContext = true;
    [Display(Name = "Show Change of Context at Level", GroupName = "0) Master Toggles", Order = 2)]
    public bool ShowContext
    {
        get => _showContext;
        set { _showContext = value; RecalculateValues(); }
    }

    private bool _filterByColor = false;
    [Display(Name = "Filter Levels by Color", GroupName = "0) Master Toggles", Order = 3,
        Description = "When enabled, only horizontal lines matching the specified color will trigger signals")]
    public bool FilterByColor
    {
        get => _filterByColor;
        set { _filterByColor = value; RecalculateValues(); }
    }

    private Color _levelColor = Color.FromArgb(255, 0, 255, 255); // Cyan
    [Display(Name = "Level Line Color", GroupName = "0) Master Toggles", Order = 4,
        Description = "Only horizontal lines drawn in this color will be used as key levels")]
    public Color LevelColor
    {
        get => _levelColor;
        set { _levelColor = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  1) CHERRY ON TOP AT LEVEL
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Cherry on Top at Level

    private int _cherryDivisions = 4;
    [Display(Name = "Number of Divisions", GroupName = "1) Cherry on Top at Level", Order = 100)]
    [Range(2, 10)]
    public int CherryDivisions
    {
        get => _cherryDivisions;
        set { _cherryDivisions = value; RecalculateValues(); }
    }

    private MarkerSymbol _cherryBullSymbol = MarkerSymbol.TriangleUp;
    [Display(Name = "Bullish Symbol", GroupName = "1) Cherry on Top at Level", Order = 110)]
    public MarkerSymbol CherryBullSymbol
    {
        get => _cherryBullSymbol;
        set { _cherryBullSymbol = value; RecalculateValues(); }
    }

    private Color _cherryBullColor = Color.Green;
    [Display(Name = "Bullish Symbol Color", GroupName = "1) Cherry on Top at Level", Order = 120)]
    public Color CherryBullColor
    {
        get => _cherryBullColor;
        set { _cherryBullColor = value; RecalculateValues(); }
    }

    private MarkerLocation _cherryBullLocation = MarkerLocation.BelowBar;
    [Display(Name = "Bullish Symbol Location", GroupName = "1) Cherry on Top at Level", Order = 130)]
    public MarkerLocation CherryBullLocation
    {
        get => _cherryBullLocation;
        set { _cherryBullLocation = value; RecalculateValues(); }
    }

    private MarkerSymbol _cherryBearSymbol = MarkerSymbol.TriangleDown;
    [Display(Name = "Bearish Symbol", GroupName = "1) Cherry on Top at Level", Order = 140)]
    public MarkerSymbol CherryBearSymbol
    {
        get => _cherryBearSymbol;
        set { _cherryBearSymbol = value; RecalculateValues(); }
    }

    private Color _cherryBearColor = Color.Red;
    [Display(Name = "Bearish Symbol Color", GroupName = "1) Cherry on Top at Level", Order = 150)]
    public Color CherryBearColor
    {
        get => _cherryBearColor;
        set { _cherryBearColor = value; RecalculateValues(); }
    }

    private MarkerLocation _cherryBearLocation = MarkerLocation.AboveBar;
    [Display(Name = "Bearish Symbol Location", GroupName = "1) Cherry on Top at Level", Order = 160)]
    public MarkerLocation CherryBearLocation
    {
        get => _cherryBearLocation;
        set { _cherryBearLocation = value; RecalculateValues(); }
    }

    private MarkerSize _cherryMarkerSize = MarkerSize.Small;
    [Display(Name = "Marker Size", GroupName = "1) Cherry on Top at Level", Order = 170)]
    public MarkerSize CherryMarkerSize
    {
        get => _cherryMarkerSize;
        set { _cherryMarkerSize = value; RecalculateValues(); }
    }

    // ── Moonshot (COT + COC combined) ──

    private bool _moonshotEnabled = true;
    [Display(Name = "Enable Moonshot", GroupName = "1) Cherry on Top at Level", Order = 196,
        Description = "COT + COC combined: arriving candle crosses level and closes back in, signal candle sweeps both high and low, closes in extreme quartile")]
    public bool MoonshotEnabled
    {
        get => _moonshotEnabled;
        set { _moonshotEnabled = value; RecalculateValues(); }
    }

    private Color _moonshotBullColor = Color.FromArgb(255, 0, 255, 200);
    [Display(Name = "Moonshot Bullish Color", GroupName = "1) Cherry on Top at Level", Order = 197)]
    public Color MoonshotBullColor
    {
        get => _moonshotBullColor;
        set { _moonshotBullColor = value; RecalculateValues(); }
    }

    private Color _moonshotBearColor = Color.FromArgb(255, 255, 0, 200);
    [Display(Name = "Moonshot Bearish Color", GroupName = "1) Cherry on Top at Level", Order = 198)]
    public Color MoonshotBearColor
    {
        get => _moonshotBearColor;
        set { _moonshotBearColor = value; RecalculateValues(); }
    }

    // ── 1-Candle COT ──

    private bool _show1CandleCOT = false;
    [Display(Name = "Enable 1-Candle COT", GroupName = "1) Cherry on Top at Level", Order = 198,
        Description = "Single candle acts as both arrival and reversal (crosses level, closes back in extreme quartile). Off by default — most traders prefer the 2-candle pattern.")]
    public bool Show1CandleCOT
    {
        get => _show1CandleCOT;
        set { _show1CandleCOT = value; RecalculateValues(); }
    }

    private Color _cherry1CandleBullColor = Color.FromArgb(255, 100, 180, 100);
    [Display(Name = "1-Candle Bullish Color", GroupName = "1) Cherry on Top at Level", Order = 199,
        Description = "Color for 1-candle COT (single candle crosses level and closes back in extreme quartile)")]
    public Color Cherry1CandleBullColor
    {
        get => _cherry1CandleBullColor;
        set { _cherry1CandleBullColor = value; RecalculateValues(); }
    }

    private Color _cherry1CandleBearColor = Color.FromArgb(255, 180, 100, 100);
    [Display(Name = "1-Candle Bearish Color", GroupName = "1) Cherry on Top at Level", Order = 200,
        Description = "Color for 1-candle COT (single candle crosses level and closes back in extreme quartile)")]
    public Color Cherry1CandleBearColor
    {
        get => _cherry1CandleBearColor;
        set { _cherry1CandleBearColor = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  2) CHANGE OF CONTEXT AT LEVEL
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Change of Context at Level

    private MarkerSymbol _ctxBullSymbol = MarkerSymbol.Square;
    [Display(Name = "Bullish Symbol", GroupName = "2) Change of Context at Level", Order = 220)]
    public MarkerSymbol CtxBullSymbol
    {
        get => _ctxBullSymbol;
        set { _ctxBullSymbol = value; RecalculateValues(); }
    }

    private Color _ctxBullColor = Color.FromArgb(255, 0, 100, 255);
    [Display(Name = "Bullish Symbol Color", GroupName = "2) Change of Context at Level", Order = 230)]
    public Color CtxBullColor
    {
        get => _ctxBullColor;
        set { _ctxBullColor = value; RecalculateValues(); }
    }

    private MarkerLocation _ctxBullLocation = MarkerLocation.BelowBar;
    [Display(Name = "Bullish Symbol Location", GroupName = "2) Change of Context at Level", Order = 240)]
    public MarkerLocation CtxBullLocation
    {
        get => _ctxBullLocation;
        set { _ctxBullLocation = value; RecalculateValues(); }
    }

    private MarkerSymbol _ctxBearSymbol = MarkerSymbol.Square;
    [Display(Name = "Bearish Symbol", GroupName = "2) Change of Context at Level", Order = 250)]
    public MarkerSymbol CtxBearSymbol
    {
        get => _ctxBearSymbol;
        set { _ctxBearSymbol = value; RecalculateValues(); }
    }

    private Color _ctxBearColor = Color.FromArgb(255, 180, 0, 255);
    [Display(Name = "Bearish Symbol Color", GroupName = "2) Change of Context at Level", Order = 260)]
    public Color CtxBearColor
    {
        get => _ctxBearColor;
        set { _ctxBearColor = value; RecalculateValues(); }
    }

    private MarkerLocation _ctxBearLocation = MarkerLocation.AboveBar;
    [Display(Name = "Bearish Symbol Location", GroupName = "2) Change of Context at Level", Order = 270)]
    public MarkerLocation CtxBearLocation
    {
        get => _ctxBearLocation;
        set { _ctxBearLocation = value; RecalculateValues(); }
    }

    private MarkerSize _ctxMarkerSize = MarkerSize.Small;
    [Display(Name = "Marker Size", GroupName = "2) Change of Context at Level", Order = 280)]
    public MarkerSize CtxMarkerSize
    {
        get => _ctxMarkerSize;
        set { _ctxMarkerSize = value; RecalculateValues(); }
    }

    // ── COC Magic (arriving candle also closed back in = stronger) ──

    private bool _ctxMagicEnabled = true;
    [Display(Name = "Enable Magic COC", GroupName = "2) Change of Context at Level", Order = 290,
        Description = "Distinguish COC where the arriving candle crossed the level AND closed back in (no closes beyond level)")]
    public bool CtxMagicEnabled
    {
        get => _ctxMagicEnabled;
        set { _ctxMagicEnabled = value; RecalculateValues(); }
    }

    private Color _ctxMagicBullColor = Color.Cyan;
    [Display(Name = "Magic COC Bullish Color", GroupName = "2) Change of Context at Level", Order = 291)]
    public Color CtxMagicBullColor
    {
        get => _ctxMagicBullColor;
        set { _ctxMagicBullColor = value; RecalculateValues(); }
    }

    private Color _ctxMagicBearColor = Color.Magenta;
    [Display(Name = "Magic COC Bearish Color", GroupName = "2) Change of Context at Level", Order = 292)]
    public Color CtxMagicBearColor
    {
        get => _ctxMagicBearColor;
        set { _ctxMagicBearColor = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  3) PROBABILITY TABLE
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Probability Table

    private bool _showProbTable = true;
    [Display(Name = "Show Probability Table", GroupName = "3) Probability Table", Order = 300)]
    public bool ShowProbTable
    {
        get => _showProbTable;
        set { _showProbTable = value; RecalculateValues(); }
    }

    private LookbackType _probLookbackType = LookbackType.Signals;
    [Display(Name = "Lookback Type", GroupName = "3) Probability Table", Order = 310)]
    public LookbackType ProbLookbackType
    {
        get => _probLookbackType;
        set { _probLookbackType = value; RecalculateValues(); }
    }

    private int _probLookbackPeriod = 1000;
    [Display(Name = "Lookback Period", GroupName = "3) Probability Table", Order = 320)]
    [Range(10, 50000)]
    public int ProbLookbackPeriod
    {
        get => _probLookbackPeriod;
        set { _probLookbackPeriod = value; RecalculateValues(); }
    }

    private bool _probStoppedOut = true;
    [Display(Name = "Stopped Out", GroupName = "3) Probability Table", Order = 330)]
    public bool ProbStoppedOut
    {
        get => _probStoppedOut;
        set { _probStoppedOut = value; RecalculateValues(); }
    }

    private int _probContinuation = 3;
    [Display(Name = "Continuation Period", GroupName = "3) Probability Table", Order = 340)]
    [Range(1, 20)]
    public int ProbContinuation
    {
        get => _probContinuation;
        set { _probContinuation = value; RecalculateValues(); }
    }

    private TablePosition _probTablePosition = TablePosition.BottomRight;
    [Display(Name = "Table Position", GroupName = "3) Probability Table", Order = 350)]
    public TablePosition ProbTablePosition
    {
        get => _probTablePosition;
        set { _probTablePosition = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  CONSTRUCTOR
    // ═══════════════════════════════════════════════════════════════

    public CCSuiteLevels()
    {
        Panel = IndicatorDataProvider.CandlesPanel;
        EnableCustomDrawing = true;
        SubscribeToDrawingEvents(DrawingLayouts.Final);
        DataSeries.Add(_signalSeries);
    }

    // ═══════════════════════════════════════════════════════════════
    //  CALCULATION
    // ═══════════════════════════════════════════════════════════════

    protected override void OnCalculate(int bar, decimal value)
    {
        _signalSeries[bar] = 0;

        if (bar == 0)
            return;

        // ── Clear any stale markers / tracked signals for this bar ──
        // (Necessary because OnCalculate may run mid-bar; conditions can flip
        //  before the bar closes, leaving ghost markers from earlier ticks.)
        Labels.Remove("clU_" + bar);
        Labels.Remove("clB_" + bar);
        Labels.Remove("cxU_" + bar);
        Labels.Remove("cxB_" + bar);
        _cherryBullSignals.RemoveAll(s => s.Bar == bar);
        _cherryBearSignals.RemoveAll(s => s.Bar == bar);
        _moonshotBullSignals.RemoveAll(s => s.Bar == bar);
        _moonshotBearSignals.RemoveAll(s => s.Bar == bar);
        _ctxBullSignals.RemoveAll(s => s.Bar == bar);
        _ctxBearSignals.RemoveAll(s => s.Bar == bar);
        _magicBullSignals.RemoveAll(s => s.Bar == bar);
        _magicBearSignals.RemoveAll(s => s.Bar == bar);

        // Read user-drawn horizontal lines from the chart
        var drawingObjects = ChartInfo?.DrawingObjectsListInfo;
        if (drawingObjects == null)
            return;

        var horizontalLines = drawingObjects.HorizontalLines;
        if (horizontalLines == null)
            return;

        // Collect level prices (optionally filtered by color)
        var levelPrices = new List<decimal>();
        foreach (var line in horizontalLines)
        {
            if (_filterByColor)
            {
                try
                {
                    var lineType = line.GetType();
                    Color? found = null;

                    // Scan ALL properties for a Color value (direct or nested inside a Pen/Brush)
                    foreach (var prop in lineType.GetProperties())
                    {
                        try
                        {
                            var val = prop.GetValue(line);
                            if (val == null) continue;

                            if (val is Color directColor)
                            {
                                found = directColor;
                                break;
                            }

                            // Check if this property has a nested .Color (e.g. Pen.Color, Brush.Color)
                            var nestedColorProp = val.GetType().GetProperty("Color");
                            if (nestedColorProp != null)
                            {
                                var nestedVal = nestedColorProp.GetValue(val);
                                if (nestedVal is Color nestedColor)
                                {
                                    found = nestedColor;
                                    break;
                                }
                            }
                        }
                        catch { /* skip inaccessible properties */ }
                    }

                    if (found == null)
                        continue;

                    var lc = found.Value;
                    if (lc.R != _levelColor.R || lc.G != _levelColor.G || lc.B != _levelColor.B)
                        continue;
                }
                catch
                {
                    continue;
                }
            }
            levelPrices.Add(line.Price);
        }

        if (levelPrices.Count == 0)
            return;

        var candle = GetCandle(bar);
        decimal h = candle.High;
        decimal l = candle.Low;
        decimal c = candle.Close;
        decimal o = candle.Open;
        decimal rng = h - l;

        var prevCandle = GetCandle(bar - 1);
        decimal prevH = prevCandle.High;
        decimal prevL = prevCandle.Low;
        decimal prevC = prevCandle.Close;
        decimal prevO = prevCandle.Open;
        bool prevBullish = prevC > prevO;
        bool prevBearish = prevC < prevO;

        int signalCode = 0;

        // ── 1) Cherry on Top at Level ──
        // ALL COT signals require extreme quartile close.
        // Moonshot = COT + COC combined (sweeps both sides + quartile).
        if (_showCherry && rng > 0)
        {
            decimal cherryDivF = _cherryDivisions;
            decimal cherryLowerTh = l + rng * (1.0m / cherryDivF);
            decimal cherryUpperTh = l + rng * ((cherryDivF - 1.0m) / cherryDivF);

            bool sweepsHigh = h > prevH;
            bool sweepsLow  = l < prevL;
            bool closesLow  = c <= cherryLowerTh;
            bool closesHigh = c >= cherryUpperTh;
            bool currentBearish = c < o;
            bool currentBullish = c > o;

            // ── Bearish COT at Level ──
            // 2-candle: arriving bull crosses above level, closes back below.
            //           Signal sweeps prevH, closes bearish, closes in lower quartile.
            // 1-candle: single candle crosses above level, closes back below in lower quartile.
            // Moonshot: 2-candle COT where signal also takes prevL (COT + COC combined).

            // 2-candle bearish at level (requires extreme quartile)
            bool bear2CandleAtLevel = false;
            if (prevBullish && currentBearish && sweepsHigh && closesLow)
            {
                foreach (decimal lvl in levelPrices)
                {
                    if (prevH > lvl && prevC < lvl && c < lvl)
                    {
                        bear2CandleAtLevel = true;
                        break;
                    }
                }
            }

            // 1-candle bearish at level (hammer-like: opened below level, wick went above, closed back in quartile)
            bool bear1CandleAtLevel = false;
            if (_show1CandleCOT && currentBearish && closesLow)
            {
                foreach (decimal lvl in levelPrices)
                {
                    if (h > lvl && o < lvl && c < lvl)
                    {
                        bear1CandleAtLevel = true;
                        break;
                    }
                }
            }

            if (bear2CandleAtLevel || bear1CandleAtLevel)
            {
                bool above = _cherryBearLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_cherryMarkerSize + 15;
                int yOff = above ? -gap : gap;

                Color col;
                // Priority: Moonshot > 2-candle > 1-candle
                if (bear2CandleAtLevel && sweepsLow && _moonshotEnabled)
                {
                    col = _moonshotBearColor;
                    AddText("clB_" + bar, SymbolText(_cherryBearSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_moonshotBearSignals, bar, false);
                    signalCode = 10; // Moonshot_Bear
                }
                else if (bear2CandleAtLevel)
                {
                    col = _cherryBearColor;
                    AddText("clB_" + bar, SymbolText(_cherryBearSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_cherryBearSignals, bar, false);
                    signalCode = 2; // COT_Bear
                }
                else if (bear1CandleAtLevel)
                {
                    col = _cherry1CandleBearColor;
                    AddText("clB_" + bar, SymbolText(_cherryBearSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_cherryBearSignals, bar, false);
                    signalCode = 4; // COT_1Candle_Bear
                }
            }

            // ── Bullish COT at Level ──
            bool bull2CandleAtLevel = false;
            if (prevBearish && currentBullish && sweepsLow && closesHigh)
            {
                foreach (decimal lvl in levelPrices)
                {
                    if (prevL < lvl && prevC > lvl && c > lvl)
                    {
                        bull2CandleAtLevel = true;
                        break;
                    }
                }
            }

            // 1-candle bullish at level (hammer-like: opened above level, wick went below, closed back in quartile)
            bool bull1CandleAtLevel = false;
            if (_show1CandleCOT && currentBullish && closesHigh)
            {
                foreach (decimal lvl in levelPrices)
                {
                    if (l < lvl && o > lvl && c > lvl)
                    {
                        bull1CandleAtLevel = true;
                        break;
                    }
                }
            }

            if (bull2CandleAtLevel || bull1CandleAtLevel)
            {
                bool above = _cherryBullLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_cherryMarkerSize + 15;
                int yOff = above ? -gap : gap;

                Color col;
                // Priority: Moonshot > 2-candle > 1-candle
                if (bull2CandleAtLevel && sweepsHigh && _moonshotEnabled)
                {
                    col = _moonshotBullColor;
                    AddText("clU_" + bar, SymbolText(_cherryBullSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_moonshotBullSignals, bar, true);
                    signalCode = 9; // Moonshot_Bull
                }
                else if (bull2CandleAtLevel)
                {
                    col = _cherryBullColor;
                    AddText("clU_" + bar, SymbolText(_cherryBullSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_cherryBullSignals, bar, true);
                    signalCode = 1; // COT_Bull
                }
                else if (bull1CandleAtLevel)
                {
                    col = _cherry1CandleBullColor;
                    AddText("clU_" + bar, SymbolText(_cherryBullSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_cherryMarkerSize, DrawingText.TextAlign.Center);
                    TrackSignal(_cherryBullSignals, bar, true);
                    signalCode = 3; // COT_1Candle_Bull
                }
            }
        }

        // ── 2) Change of Context at Level ──
        // Arriving candle crosses the level (doesn't have to close back in).
        // Reversal candle: takes prev low/high, closes back in bearish/bullish.
        // Magic COC: arriving candle crossed AND closed back in (stronger — no closes beyond level).
        if (_showContext)
        {
            bool currentBearish = c < o;
            bool currentBullish = c > o;

            // Bearish COC at Level:
            //   Previous BULLISH candle crosses through the level (range spans it).
            //   Reversal candle takes prevL (l < prevL), closes bearish, closes below level.
            //   Magic variant: previous candle also closed back below the level.
            if (prevBullish && currentBearish && l < prevL)
            {
                bool cocAtLevel = false;
                bool cocMagicAtLevel = false;

                foreach (decimal lvl in levelPrices)
                {
                    if (prevL < lvl && prevH > lvl && c < lvl)
                    {
                        cocAtLevel = true;
                        if (prevC < lvl)
                            cocMagicAtLevel = true;
                        break;
                    }
                }

                if (cocAtLevel)
                {
                    bool above = _ctxBearLocation == MarkerLocation.AboveBar;
                    decimal price = above ? h : l;
                    int cotOffset = (int)_cherryMarkerSize + 15;
                    int gap = cotOffset + (int)_ctxMarkerSize + 5;
                    int yOff = above ? -gap : gap;

                    Color col = (_ctxMagicEnabled && cocMagicAtLevel) ? _ctxMagicBearColor : _ctxBearColor;

                    AddText("clcxB_" + bar, SymbolText(_ctxBearSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_ctxMarkerSize, DrawingText.TextAlign.Center);

                    if (cocMagicAtLevel && _ctxMagicEnabled)
                    {
                        TrackSignal(_magicBearSignals, bar, false);
                        if (signalCode == 0)
                            signalCode = 6; // COC_Magic_Bear
                    }
                    else
                    {
                        TrackSignal(_ctxBearSignals, bar, false);
                        if (signalCode == 0)
                            signalCode = 8; // COC_Bear
                    }
                }
            }

            // Bullish COC at Level:
            //   Previous BEARISH candle crosses through the level (range spans it).
            //   Reversal candle takes prevH (h > prevH), closes bullish, closes above level.
            //   Magic variant: previous candle also closed back above the level.
            if (prevBearish && currentBullish && h > prevH)
            {
                bool cocAtLevel = false;
                bool cocMagicAtLevel = false;

                foreach (decimal lvl in levelPrices)
                {
                    if (prevL < lvl && prevH > lvl && c > lvl)
                    {
                        cocAtLevel = true;
                        if (prevC > lvl)
                            cocMagicAtLevel = true;
                        break;
                    }
                }

                if (cocAtLevel)
                {
                    bool above = _ctxBullLocation == MarkerLocation.AboveBar;
                    decimal price = above ? h : l;
                    int cotOffset = (int)_cherryMarkerSize + 15;
                    int gap = cotOffset + (int)_ctxMarkerSize + 5;
                    int yOff = above ? -gap : gap;

                    Color col = (_ctxMagicEnabled && cocMagicAtLevel) ? _ctxMagicBullColor : _ctxBullColor;

                    AddText("clcxU_" + bar, SymbolText(_ctxBullSymbol), above, bar, price, yOff, 0,
                        col, Color.Transparent, Color.Transparent,
                        (int)_ctxMarkerSize, DrawingText.TextAlign.Center);

                    if (cocMagicAtLevel && _ctxMagicEnabled)
                    {
                        TrackSignal(_magicBullSignals, bar, true);
                        if (signalCode == 0)
                            signalCode = 5; // COC_Magic_Bull
                    }
                    else
                    {
                        TrackSignal(_ctxBullSignals, bar, true);
                        if (signalCode == 0)
                            signalCode = 7; // COC_Bull
                    }
                }
            }
        }

        _signalSeries[bar] = signalCode;
    }

    // ═══════════════════════════════════════════════════════════════
    //  PROBABILITY TABLE
    // ═══════════════════════════════════════════════════════════════

    #region Probability Logic

    private struct LevelSignalRecord
    {
        public int Bar;
        public bool IsBull;
        public decimal SignalHigh;
        public decimal SignalLow;
    }

    private void TrackSignal(List<LevelSignalRecord> list, int bar, bool isBull)
    {
        var candle = GetCandle(bar);
        list.Add(new LevelSignalRecord
        {
            Bar        = bar,
            IsBull     = isBull,
            SignalHigh = candle.High,
            SignalLow  = candle.Low,
        });
    }

    private (int total, int continued, int stopped) EvaluateSignals(List<LevelSignalRecord> signals)
    {
        int lastBar = CurrentBar - 1;
        int total = 0, continued = 0, stopped = 0;

        int startIdx = 0;
        if (_probLookbackType == LookbackType.Signals)
        {
            startIdx = Math.Max(0, signals.Count - _probLookbackPeriod);
        }
        else
        {
            int cutoffBar = Math.Max(0, lastBar - _probLookbackPeriod);
            for (int i = signals.Count - 1; i >= 0; i--)
            {
                if (signals[i].Bar < cutoffBar) { startIdx = i + 1; break; }
            }
        }

        for (int i = startIdx; i < signals.Count; i++)
        {
            var sig = signals[i];
            if (sig.Bar + _probContinuation > lastBar)
                continue;

            total++;
            bool didContinue = false;
            bool didStop = false;

            for (int offset = 1; offset <= _probContinuation; offset++)
            {
                var futureCandle = GetCandle(sig.Bar + offset);

                if (sig.IsBull)
                {
                    if (_probStoppedOut && futureCandle.Close < sig.SignalLow)
                    { didStop = true; break; }
                    if (futureCandle.Close > sig.SignalHigh)
                    { didContinue = true; break; }
                }
                else
                {
                    if (_probStoppedOut && futureCandle.Close > sig.SignalHigh)
                    { didStop = true; break; }
                    if (futureCandle.Close < sig.SignalLow)
                    { didContinue = true; break; }
                }
            }

            if (didContinue) continued++;
            if (didStop) stopped++;
        }

        return (total, continued, stopped);
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  CUSTOM RENDERING — probability table overlay
    // ═══════════════════════════════════════════════════════════════

    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
        if (!_showProbTable)
            return;

        var chBear = EvaluateSignals(_cherryBearSignals);
        var chBull = EvaluateSignals(_cherryBullSignals);
        var msBear = EvaluateSignals(_moonshotBearSignals);
        var msBull = EvaluateSignals(_moonshotBullSignals);
        var cxBear = EvaluateSignals(_ctxBearSignals);
        var cxBull = EvaluateSignals(_ctxBullSignals);
        var mgCocBear = EvaluateSignals(_magicBearSignals);
        var mgCocBull = EvaluateSignals(_magicBullSignals);

        // Build data rows
        var rows = new List<(string label, int n, string cont, string stop, Color accent)>();

        rows.Add(FormatTableRow("COT Bull",  chBull, Color.FromArgb(255, 38, 166, 91)));
        rows.Add(FormatTableRow("COT Bear",  chBear, Color.FromArgb(255, 239, 83, 80)));

        if (_moonshotEnabled)
        {
            rows.Add(FormatTableRow("Moon Bull", msBull, Color.FromArgb(255, 0, 255, 200)));
            rows.Add(FormatTableRow("Moon Bear", msBear, Color.FromArgb(255, 255, 0, 200)));
        }

        rows.Add(FormatTableRow("CoC Bull",  cxBull, Color.FromArgb(255, 0, 100, 255)));
        rows.Add(FormatTableRow("CoC Bear",  cxBear, Color.FromArgb(255, 180, 0, 255)));

        if (_ctxMagicEnabled)
        {
            rows.Add(FormatTableRow("MCoC Bull", mgCocBull, Color.Cyan));
            rows.Add(FormatTableRow("MCoC Bear", mgCocBear, Color.Magenta));
        }

        // ── Layout ──
        int rowH    = 26;
        int titleH  = 22;
        int subH    = 18;
        int hdrH    = 22;
        int pad     = 12;
        int topArea = titleH + subH + 6;            // title + subtitle + gap
        int dataTop = topArea + hdrH;                // where column headers sit
        int tableH  = dataTop + rows.Count * rowH + pad;

        // Column positions (absolute offsets from table left edge)
        int col0 = pad + 8;                          // accent bar + label
        int col1 = 190;                              // n
        int col2 = 240;                              // Cont%
        int col3 = 340;                              // Stop%
        int tableW = col3 + 80;                      // total width

        var bounds = context.ClipBounds;
        int x, y;
        switch (_probTablePosition)
        {
            case TablePosition.TopLeft:
                x = bounds.X + 10; y = bounds.Y + 10; break;
            case TablePosition.TopRight:
                x = bounds.Right - tableW - 10; y = bounds.Y + 10; break;
            case TablePosition.BottomLeft:
                x = bounds.X + 10; y = bounds.Bottom - tableH - 10; break;
            default:
                x = bounds.Right - tableW - 10; y = bounds.Bottom - tableH - 10; break;
        }

        // ── Background ──
        context.FillRectangle(Color.FromArgb(235, 10, 10, 18),
            new Rectangle(x, y, tableW, tableH));
        context.DrawRectangle(new RenderPen(Color.FromArgb(100, 80, 80, 100), 1),
            new Rectangle(x, y, tableW, tableH));

        // ── Title ──
        context.DrawString("CC Suite Levels", _tableHeaderFont,
            Color.FromArgb(255, 240, 240, 250), x + pad, y + 3);

        // ── Subtitle ──
        string sub = $"Lookback {_probLookbackPeriod} {_probLookbackType}  /  Cont {_probContinuation} bars";
        context.DrawString(sub, _tableColFont,
            Color.FromArgb(150, 140, 140, 160), x + pad, y + titleH + 2);

        // ── Separator below subtitle ──
        var sepPen = new RenderPen(Color.FromArgb(50, 255, 255, 255), 1);
        int sepY = y + topArea - 2;
        context.DrawLine(sepPen, x + 4, sepY, x + tableW - 4, sepY);

        // ── Column headers ──
        int chy = y + topArea + 2;
        var hdrCol = Color.FromArgb(130, 150, 150, 170);
        context.DrawString("SIGNAL",  _tableColFont, hdrCol, x + col0, chy);
        context.DrawString("N",       _tableColFont, hdrCol, x + col1, chy);
        context.DrawString("CONT%",   _tableColFont, hdrCol, x + col2, chy);
        context.DrawString("STOP%",   _tableColFont, hdrCol, x + col3, chy);

        // ── Header underline ──
        int hdrLineY = y + dataTop - 2;
        context.DrawLine(sepPen, x + 4, hdrLineY, x + tableW - 4, hdrLineY);

        // ── Data rows ──
        var rowBgAlt   = Color.FromArgb(20, 255, 255, 255);
        var labelColor = Color.FromArgb(255, 220, 220, 230);
        var nColor     = Color.FromArgb(180, 170, 170, 180);
        var contColor  = Color.FromArgb(255, 76, 195, 130);
        var stopColor  = Color.FromArgb(255, 239, 100, 100);

        for (int i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            int ry = y + dataTop + i * rowH;

            // Alternating background
            if (i % 2 == 0)
                context.FillRectangle(rowBgAlt, new Rectangle(x + 1, ry, tableW - 2, rowH));

            // Accent bar (left)
            context.FillRectangle(row.accent,
                new Rectangle(x + 2, ry + 4, 4, rowH - 8));

            // Label
            context.DrawString(row.label, _tableBodyFont, labelColor,
                x + col0, ry + 4);

            // N
            context.DrawString(row.n.ToString(), _tableBodyFont, nColor,
                x + col1, ry + 4);

            // Cont%
            context.DrawString(row.cont, _tableBodyFont, contColor,
                x + col2, ry + 4);

            // Stop%
            context.DrawString(row.stop, _tableBodyFont, stopColor,
                x + col3, ry + 4);
        }

        // ── Bottom line ──
        int botY = y + dataTop + rows.Count * rowH;
        context.DrawLine(sepPen, x + 4, botY, x + tableW - 4, botY);
    }

    private static (string label, int n, string cont, string stop, Color accent) FormatTableRow(
        string label, (int total, int continued, int stopped) stats, Color accent)
    {
        if (stats.total == 0)
            return (label, 0, "-", "-", accent);

        double contPct = 100.0 * stats.continued / stats.total;
        double stopPct = 100.0 * stats.stopped / stats.total;

        return (label, stats.total, $"{contPct:F1}%", $"{stopPct:F1}%", accent);
    }
}
