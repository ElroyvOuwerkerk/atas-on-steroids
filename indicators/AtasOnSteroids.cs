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

#region Enums

public enum MarkerSymbol
{
    [Description("▲ Triangle Up")]    TriangleUp,
    [Description("▼ Triangle Down")]  TriangleDown,
    [Description("● Circle")]         Circle,
    [Description("■ Square")]         Square,
}

public enum MarkerLocation
{
    AboveBar,
    BelowBar,
}

public enum MarkerSize
{
    Tiny   = 8,
    Small  = 11,
    Normal = 14,
    Large  = 18,
}

public enum TablePosition
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
}

public enum LookbackType
{
    Signals,
    Bars,
}

#endregion

[DisplayName("CC Suite")]
[Category("Custom")]
public class AtasOnSteroids : Indicator
{
    #region Fields

    private readonly PaintbarsDataSeries _paintBars = new("Division Bar Colors") { IsHidden = true };
    private readonly ValueDataSeries _signalSeries = new("Signal") { IsHidden = true };

    // Probability tracking
    private readonly List<SignalRecord> _cherryBearSignals = new();
    private readonly List<SignalRecord> _cherryBullSignals = new();
    private readonly List<SignalRecord> _magicBearSignals  = new();
    private readonly List<SignalRecord> _magicBullSignals  = new();
    private readonly List<SignalRecord> _ctxBearSignals    = new();
    private readonly List<SignalRecord> _ctxBullSignals    = new();

    // Rendering
    private readonly RenderFont _tableHeaderFont = new("Arial", 12);
    private readonly RenderFont _tableBodyFont   = new("Arial", 10);

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

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  MASTER TOGGLES
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Master Toggles

    private bool _showDivisionOverlay = true;
    [Display(Name = "Show Division Candle Overlay", GroupName = "0) Master Toggles", Order = 1)]
    public bool ShowDivisionOverlay
    {
        get => _showDivisionOverlay;
        set { _showDivisionOverlay = value; RecalculateValues(); }
    }

    private bool _showCherry = true;
    [Display(Name = "Show Cherry on Top", GroupName = "0) Master Toggles", Order = 2)]
    public bool ShowCherry
    {
        get => _showCherry;
        set { _showCherry = value; RecalculateValues(); }
    }

    private bool _showContext = true;
    [Display(Name = "Show Change of Context", GroupName = "0) Master Toggles", Order = 3)]
    public bool ShowContext
    {
        get => _showContext;
        set { _showContext = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  1) DIVISION CANDLE OVERLAY
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Division Candle Overlay

    private int _divOverlayDivisions = 4;
    [Display(Name = "Number of Divisions", GroupName = "1) Division Candle Overlay", Order = 10)]
    [Range(2, 10)]
    public int DivOverlayDivisions
    {
        get => _divOverlayDivisions;
        set { _divOverlayDivisions = value; RecalculateValues(); }
    }

    private bool _divUpperEnabled = true;
    [Display(Name = "Enable Upper Zone (Green)", GroupName = "1) Division Candle Overlay", Order = 20)]
    public bool DivUpperEnabled
    {
        get => _divUpperEnabled;
        set { _divUpperEnabled = value; RecalculateValues(); }
    }

    private Color _divUpperColor = Color.Green;
    [Display(Name = "Upper Zone Color", GroupName = "1) Division Candle Overlay", Order = 30)]
    public Color DivUpperColor
    {
        get => _divUpperColor;
        set { _divUpperColor = value; RecalculateValues(); }
    }

    private bool _divLowerEnabled = true;
    [Display(Name = "Enable Lower Zone (Red)", GroupName = "1) Division Candle Overlay", Order = 40)]
    public bool DivLowerEnabled
    {
        get => _divLowerEnabled;
        set { _divLowerEnabled = value; RecalculateValues(); }
    }

    private Color _divLowerColor = Color.Red;
    [Display(Name = "Lower Zone Color", GroupName = "1) Division Candle Overlay", Order = 50)]
    public Color DivLowerColor
    {
        get => _divLowerColor;
        set { _divLowerColor = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  2) CHERRY ON TOP
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Cherry on Top

    private int _cherryDivisions = 4;
    [Display(Name = "Number of Divisions", GroupName = "2) Cherry on Top", Order = 100)]
    [Range(2, 10)]
    public int CherryDivisions
    {
        get => _cherryDivisions;
        set { _cherryDivisions = value; RecalculateValues(); }
    }

    private MarkerSymbol _cherryBullSymbol = MarkerSymbol.TriangleUp;
    [Display(Name = "Bullish Symbol", GroupName = "2) Cherry on Top", Order = 110)]
    public MarkerSymbol CherryBullSymbol
    {
        get => _cherryBullSymbol;
        set { _cherryBullSymbol = value; RecalculateValues(); }
    }

    private Color _cherryBullColor = Color.Green;
    [Display(Name = "Bullish Symbol Color", GroupName = "2) Cherry on Top", Order = 120)]
    public Color CherryBullColor
    {
        get => _cherryBullColor;
        set { _cherryBullColor = value; RecalculateValues(); }
    }

    private MarkerLocation _cherryBullLocation = MarkerLocation.BelowBar;
    [Display(Name = "Bullish Symbol Location", GroupName = "2) Cherry on Top", Order = 130)]
    public MarkerLocation CherryBullLocation
    {
        get => _cherryBullLocation;
        set { _cherryBullLocation = value; RecalculateValues(); }
    }

    private MarkerSymbol _cherryBearSymbol = MarkerSymbol.TriangleDown;
    [Display(Name = "Bearish Symbol", GroupName = "2) Cherry on Top", Order = 140)]
    public MarkerSymbol CherryBearSymbol
    {
        get => _cherryBearSymbol;
        set { _cherryBearSymbol = value; RecalculateValues(); }
    }

    private Color _cherryBearColor = Color.Red;
    [Display(Name = "Bearish Symbol Color", GroupName = "2) Cherry on Top", Order = 150)]
    public Color CherryBearColor
    {
        get => _cherryBearColor;
        set { _cherryBearColor = value; RecalculateValues(); }
    }

    private MarkerLocation _cherryBearLocation = MarkerLocation.AboveBar;
    [Display(Name = "Bearish Symbol Location", GroupName = "2) Cherry on Top", Order = 160)]
    public MarkerLocation CherryBearLocation
    {
        get => _cherryBearLocation;
        set { _cherryBearLocation = value; RecalculateValues(); }
    }

    private MarkerSize _cherryMarkerSize = MarkerSize.Small;
    [Display(Name = "Marker Size", GroupName = "2) Cherry on Top", Order = 170)]
    public MarkerSize CherryMarkerSize
    {
        get => _cherryMarkerSize;
        set { _cherryMarkerSize = value; RecalculateValues(); }
    }

    // ── Magic Candle (takes both prior high AND low) ──

    private bool _cherryMagicEnabled = true;
    [Display(Name = "Enable Magic Candle", GroupName = "2) Cherry on Top", Order = 180,
        Description = "Highlight candles that take BOTH the prior high and low (double liquidity sweep)")]
    public bool CherryMagicEnabled
    {
        get => _cherryMagicEnabled;
        set { _cherryMagicEnabled = value; RecalculateValues(); }
    }

    private Color _cherryMagicBullColor = Color.Cyan;
    [Display(Name = "Magic Bullish Color", GroupName = "2) Cherry on Top", Order = 190,
        Description = "Color when magic candle closes in upper division")]
    public Color CherryMagicBullColor
    {
        get => _cherryMagicBullColor;
        set { _cherryMagicBullColor = value; RecalculateValues(); }
    }

    private Color _cherryMagicBearColor = Color.Magenta;
    [Display(Name = "Magic Bearish Color", GroupName = "2) Cherry on Top", Order = 195,
        Description = "Color when magic candle closes in lower division")]
    public Color CherryMagicBearColor
    {
        get => _cherryMagicBearColor;
        set { _cherryMagicBearColor = value; RecalculateValues(); }
    }

    // ── Weak COT (1-candle, previous candle same direction = weaker signal) ──

    private Color _cherryWeakBullColor = Color.FromArgb(255, 100, 180, 100);
    [Display(Name = "Weak Bullish Color", GroupName = "2) Cherry on Top", Order = 196,
        Description = "Color for 1-candle COT where previous candle was NOT the opposite direction (weaker signal)")]
    public Color CherryWeakBullColor
    {
        get => _cherryWeakBullColor;
        set { _cherryWeakBullColor = value; RecalculateValues(); }
    }

    private Color _cherryWeakBearColor = Color.FromArgb(255, 180, 100, 100);
    [Display(Name = "Weak Bearish Color", GroupName = "2) Cherry on Top", Order = 197,
        Description = "Color for 1-candle COT where previous candle was NOT the opposite direction (weaker signal)")]
    public Color CherryWeakBearColor
    {
        get => _cherryWeakBearColor;
        set { _cherryWeakBearColor = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  3) CHANGE OF CONTEXT
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Change of Context

    private bool _contextUseDivisions = false;
    [Display(Name = "Include Division Filter", GroupName = "3) Change of Context", Order = 200,
        Description = "When enabled, the candle must also close in the extreme division of its own range")]
    public bool ContextUseDivisions
    {
        get => _contextUseDivisions;
        set { _contextUseDivisions = value; RecalculateValues(); }
    }

    private int _ctxDivisions = 4;
    [Display(Name = "Number of Divisions", GroupName = "3) Change of Context", Order = 210)]
    [Range(2, 10)]
    public int CtxDivisions
    {
        get => _ctxDivisions;
        set { _ctxDivisions = value; RecalculateValues(); }
    }

    private MarkerSymbol _ctxBullSymbol = MarkerSymbol.Square;
    [Display(Name = "Bullish Symbol", GroupName = "3) Change of Context", Order = 220)]
    public MarkerSymbol CtxBullSymbol
    {
        get => _ctxBullSymbol;
        set { _ctxBullSymbol = value; RecalculateValues(); }
    }

    private Color _ctxBullColor = Color.FromArgb(255, 0, 100, 255);
    [Display(Name = "Bullish Symbol Color", GroupName = "3) Change of Context", Order = 230)]
    public Color CtxBullColor
    {
        get => _ctxBullColor;
        set { _ctxBullColor = value; RecalculateValues(); }
    }

    private MarkerLocation _ctxBullLocation = MarkerLocation.BelowBar;
    [Display(Name = "Bullish Symbol Location", GroupName = "3) Change of Context", Order = 240)]
    public MarkerLocation CtxBullLocation
    {
        get => _ctxBullLocation;
        set { _ctxBullLocation = value; RecalculateValues(); }
    }

    private MarkerSymbol _ctxBearSymbol = MarkerSymbol.Square;
    [Display(Name = "Bearish Symbol", GroupName = "3) Change of Context", Order = 250)]
    public MarkerSymbol CtxBearSymbol
    {
        get => _ctxBearSymbol;
        set { _ctxBearSymbol = value; RecalculateValues(); }
    }

    private Color _ctxBearColor = Color.FromArgb(255, 180, 0, 255);
    [Display(Name = "Bearish Symbol Color", GroupName = "3) Change of Context", Order = 260)]
    public Color CtxBearColor
    {
        get => _ctxBearColor;
        set { _ctxBearColor = value; RecalculateValues(); }
    }

    private MarkerLocation _ctxBearLocation = MarkerLocation.AboveBar;
    [Display(Name = "Bearish Symbol Location", GroupName = "3) Change of Context", Order = 270)]
    public MarkerLocation CtxBearLocation
    {
        get => _ctxBearLocation;
        set { _ctxBearLocation = value; RecalculateValues(); }
    }

    private MarkerSize _ctxMarkerSize = MarkerSize.Small;
    [Display(Name = "Marker Size", GroupName = "3) Change of Context", Order = 280)]
    public MarkerSize CtxMarkerSize
    {
        get => _ctxMarkerSize;
        set { _ctxMarkerSize = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  4) PROBABILITY TABLE
    // ═══════════════════════════════════════════════════════════════

    #region Settings — Probability Table

    private bool _showProbTable = true;
    [Display(Name = "Show Probability Table", GroupName = "4) Probability Table", Order = 300)]
    public bool ShowProbTable
    {
        get => _showProbTable;
        set { _showProbTable = value; RecalculateValues(); }
    }

    private LookbackType _probLookbackType = LookbackType.Signals;
    [Display(Name = "Lookback Type", GroupName = "4) Probability Table", Order = 310)]
    public LookbackType ProbLookbackType
    {
        get => _probLookbackType;
        set { _probLookbackType = value; RecalculateValues(); }
    }

    private int _probLookbackPeriod = 1000;
    [Display(Name = "Lookback Period", GroupName = "4) Probability Table", Order = 320)]
    [Range(10, 50000)]
    public int ProbLookbackPeriod
    {
        get => _probLookbackPeriod;
        set { _probLookbackPeriod = value; RecalculateValues(); }
    }

    private bool _probStoppedOut = true;
    [Display(Name = "Stopped Out", GroupName = "4) Probability Table", Order = 330)]
    public bool ProbStoppedOut
    {
        get => _probStoppedOut;
        set { _probStoppedOut = value; RecalculateValues(); }
    }

    private int _probContinuation = 3;
    [Display(Name = "Continuation Period", GroupName = "4) Probability Table", Order = 340)]
    [Range(1, 20)]
    public int ProbContinuation
    {
        get => _probContinuation;
        set { _probContinuation = value; RecalculateValues(); }
    }

    private TablePosition _probTablePosition = TablePosition.BottomRight;
    [Display(Name = "Table Position", GroupName = "4) Probability Table", Order = 350)]
    public TablePosition ProbTablePosition
    {
        get => _probTablePosition;
        set { _probTablePosition = value; RecalculateValues(); }
    }

    #endregion

    // ═══════════════════════════════════════════════════════════════
    //  CONSTRUCTOR
    // ═══════════════════════════════════════════════════════════════

    public AtasOnSteroids()
    {
        Panel = IndicatorDataProvider.CandlesPanel;
        EnableCustomDrawing = true;
        DataSeries.Add(_paintBars);
        DataSeries.Add(_signalSeries);
    }

    // ═══════════════════════════════════════════════════════════════
    //  CALCULATION
    // ═══════════════════════════════════════════════════════════════

    protected override void OnCalculate(int bar, decimal value)
    {
        var candle = GetCandle(bar);
        decimal h = candle.High;
        decimal l = candle.Low;
        decimal c = candle.Close;
        decimal o = candle.Open;
        decimal rng = h - l;

        // Signal code for this bar (highest-priority wins)
        // 0=None, 1=COT_Bull, 2=COT_Bear, 3=COT_Weak_Bull, 4=COT_Weak_Bear,
        // 5=Magic_Bull, 6=Magic_Bear, 7=COC_Bull, 8=COC_Bear,
        // 9=Momentum_Bull, 10=Momentum_Bear
        int signalCode = 0;

        // ── 1) Division Candle Overlay (bar coloring) ──
        if (_showDivisionOverlay && rng > 0)
        {
            decimal divF = _divOverlayDivisions;
            decimal lowerTh = l + rng * (1.0m / divF);
            decimal upperTh = l + rng * ((divF - 1.0m) / divF);

            bool upperCond = _divUpperEnabled && c >= upperTh;
            bool lowerCond = _divLowerEnabled && c <= lowerTh;

            if (upperCond && !lowerCond)
            {
                _paintBars[bar] = _divUpperColor;
                signalCode = 9; // Momentum_Bull
            }
            else if (lowerCond && !upperCond)
            {
                _paintBars[bar] = _divLowerColor;
                signalCode = 10; // Momentum_Bear
            }
            else
                _paintBars[bar] = null;
        }
        else
        {
            _paintBars[bar] = null;
        }

        // Need previous bar for signal indicators
        if (bar == 0)
        {
            _signalSeries[bar] = signalCode;
            return;
        }

        var prevCandle = GetCandle(bar - 1);
        decimal prevH = prevCandle.High;
        decimal prevL = prevCandle.Low;
        decimal prevC = prevCandle.Close;
        decimal prevO = prevCandle.Open;
        bool prevBullish = prevC > prevO;
        bool prevBearish = prevC < prevO;

        // ── 2) Cherry on Top ──
        if (_showCherry && rng > 0)
        {
            decimal cherryDivF = _cherryDivisions;
            decimal cherryLowerTh = l + rng * (1.0m / cherryDivF);
            decimal cherryUpperTh = l + rng * ((cherryDivF - 1.0m) / cherryDivF);

            bool sweepsHigh = h > prevH;
            bool sweepsLow  = l < prevL;
            bool closesLow  = c <= cherryLowerTh;
            bool closesHigh = c >= cherryUpperTh;

            // Magic candle: takes BOTH prior high AND low
            bool isMagic = sweepsHigh && sweepsLow;
            bool currentBearish = c < o;
            bool currentBullish = c > o;

            // ── Bearish COT ──
            // 2-candle: prev bullish + current red + sweeps high (no extreme quartile needed)
            // 1-candle: current red + sweeps high + MUST close in extreme quartile
            //   → if prev was bullish = strong, if prev was same direction = weak (different color)
            bool bearish2Candle = sweepsHigh && prevBullish && currentBearish;
            bool bearish1Candle = sweepsHigh && currentBearish && closesLow && prevBearish;
            bool bearish1CandleStrong = sweepsHigh && currentBearish && closesLow && prevBullish;

            if (bearish2Candle || bearish1Candle || bearish1CandleStrong)
            {
                bool above = _cherryBearLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_cherryMarkerSize + 15;
                int yOff = above ? -gap : gap;

                Color col;
                if (_cherryMagicEnabled && isMagic)
                    col = _cherryMagicBearColor;
                else if (bearish1Candle)
                    col = _cherryWeakBearColor;
                else
                    col = _cherryBearColor;

                AddText("chB_" + bar, SymbolText(_cherryBearSymbol), above, bar, price, yOff, 0,
                    col, Color.Transparent, Color.Transparent,
                    (int)_cherryMarkerSize, DrawingText.TextAlign.Center);

                if (isMagic)
                {
                    TrackSignal(_magicBearSignals, bar, false);
                    signalCode = 6; // Magic_Bear
                }
                else if (bearish1Candle)
                {
                    TrackSignal(_cherryBearSignals, bar, false);
                    signalCode = 4; // COT_Weak_Bear
                }
                else
                {
                    TrackSignal(_cherryBearSignals, bar, false);
                    signalCode = 2; // COT_Bear
                }
            }

            // ── Bullish COT ──
            // 2-candle: prev bearish + current green + sweeps low (no extreme quartile needed)
            // 1-candle: current green + sweeps low + MUST close in extreme quartile
            //   → if prev was bearish = strong, if prev was same direction = weak
            bool bullish2Candle = sweepsLow && prevBearish && currentBullish;
            bool bullish1Candle = sweepsLow && currentBullish && closesHigh && prevBullish;
            bool bullish1CandleStrong = sweepsLow && currentBullish && closesHigh && prevBearish;

            if (bullish2Candle || bullish1Candle || bullish1CandleStrong)
            {
                bool above = _cherryBullLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_cherryMarkerSize + 15;
                int yOff = above ? -gap : gap;

                Color col;
                if (_cherryMagicEnabled && isMagic)
                    col = _cherryMagicBullColor;
                else if (bullish1Candle)
                    col = _cherryWeakBullColor;
                else
                    col = _cherryBullColor;

                AddText("chU_" + bar, SymbolText(_cherryBullSymbol), above, bar, price, yOff, 0,
                    col, Color.Transparent, Color.Transparent,
                    (int)_cherryMarkerSize, DrawingText.TextAlign.Center);

                if (isMagic)
                {
                    TrackSignal(_magicBullSignals, bar, true);
                    signalCode = 5; // Magic_Bull
                }
                else if (bullish1Candle)
                {
                    TrackSignal(_cherryBullSignals, bar, true);
                    signalCode = 3; // COT_Weak_Bull
                }
                else
                {
                    TrackSignal(_cherryBullSignals, bar, true);
                    signalCode = 1; // COT_Bull
                }
            }
        }

        // ── 3) Change of Context ──
        // Only fires when the candle CHANGES direction from the previous candle.
        // Bearish CoC: previous candle was bullish, current doesn't take prior high,
        //              and closes below prior low.
        // Bullish CoC: previous candle was bearish, current doesn't take prior low,
        //              and closes above prior high.
        if (_showContext)
        {
            decimal ctxDivF = _ctxDivisions;
            decimal ctxLowerTh = l + rng * (1.0m / ctxDivF);
            decimal ctxUpperTh = l + rng * ((ctxDivF - 1.0m) / ctxDivF);

            // Bearish CoC: prev was bullish, no higher high, takes low liquidity, closes red
            bool baseBear = prevBullish && h <= prevH && l < prevL && c < o;
            bool bearDivOk = !_contextUseDivisions || (rng > 0 && c <= ctxLowerTh);

            if (baseBear && bearDivOk)
            {
                bool above = _ctxBearLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_ctxMarkerSize + 15;
                int yOff = above ? -gap : gap;

                AddText("cxB_" + bar, SymbolText(_ctxBearSymbol), above, bar, price, yOff, 0,
                    _ctxBearColor, Color.Transparent, Color.Transparent,
                    (int)_ctxMarkerSize, DrawingText.TextAlign.Center);

                TrackSignal(_ctxBearSignals, bar, false);
                if (signalCode == 0 || signalCode >= 9)
                    signalCode = 8; // COC_Bear
            }

            // Bullish CoC: prev was bearish, no lower low, takes high liquidity, closes green
            bool baseBull = prevBearish && l >= prevL && h > prevH && c > o;
            bool bullDivOk = !_contextUseDivisions || (rng > 0 && c >= ctxUpperTh);

            if (baseBull && bullDivOk)
            {
                bool above = _ctxBullLocation == MarkerLocation.AboveBar;
                decimal price = above ? h : l;
                int gap = (int)_ctxMarkerSize + 15;
                int yOff = above ? -gap : gap;

                AddText("cxU_" + bar, SymbolText(_ctxBullSymbol), above, bar, price, yOff, 0,
                    _ctxBullColor, Color.Transparent, Color.Transparent,
                    (int)_ctxMarkerSize, DrawingText.TextAlign.Center);

                TrackSignal(_ctxBullSignals, bar, true);
                if (signalCode == 0 || signalCode >= 9)
                    signalCode = 7; // COC_Bull
            }
        }

        _signalSeries[bar] = signalCode;
    }

    // ═══════════════════════════════════════════════════════════════
    //  PROBABILITY TABLE — signal tracking & outcome evaluation
    // ═══════════════════════════════════════════════════════════════

    #region Probability Logic

    private struct SignalRecord
    {
        public int Bar;
        public bool IsBull;
        public decimal SignalHigh;
        public decimal SignalLow;
    }

    private void TrackSignal(List<SignalRecord> list, int bar, bool isBull)
    {
        var candle = GetCandle(bar);
        list.Add(new SignalRecord
        {
            Bar        = bar,
            IsBull     = isBull,
            SignalHigh = candle.High,
            SignalLow  = candle.Low,
        });
    }

    private (int total, int continued, int stopped) EvaluateSignals(List<SignalRecord> signals)
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
    //  CUSTOM RENDERING — draws the probability table on the chart
    // ═══════════════════════════════════════════════════════════════

    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
        if (!_showProbTable)
            return;

        var chBear = EvaluateSignals(_cherryBearSignals);
        var chBull = EvaluateSignals(_cherryBullSignals);
        var mgBear = EvaluateSignals(_magicBearSignals);
        var mgBull = EvaluateSignals(_magicBullSignals);
        var cxBear = EvaluateSignals(_ctxBearSignals);
        var cxBull = EvaluateSignals(_ctxBullSignals);

        var lines = new List<string>
        {
            "CC Suite - Probability Table",
            $"Lookback: {_probLookbackPeriod} {_probLookbackType} | Cont: {_probContinuation} bars",
            "",
            FormatRow("Cherry Bull", chBull),
            FormatRow("Cherry Bear", chBear),
        };

        if (_cherryMagicEnabled)
        {
            lines.Add(FormatRow("Magic Bull", mgBull));
            lines.Add(FormatRow("Magic Bear", mgBear));
        }

        lines.Add(FormatRow("CoC Bull", cxBull));
        lines.Add(FormatRow("CoC Bear", cxBear));

        int lineHeight = 18;
        int padding = 10;
        int tableWidth = 340;
        int tableHeight = lines.Count * lineHeight + padding * 2;

        var bounds = context.ClipBounds;
        int x, y;

        switch (_probTablePosition)
        {
            case TablePosition.TopLeft:
                x = bounds.X + 10; y = bounds.Y + 10; break;
            case TablePosition.TopRight:
                x = bounds.Right - tableWidth - 10; y = bounds.Y + 10; break;
            case TablePosition.BottomLeft:
                x = bounds.X + 10; y = bounds.Bottom - tableHeight - 10; break;
            default:
                x = bounds.Right - tableWidth - 10; y = bounds.Bottom - tableHeight - 10; break;
        }

        // Background
        context.FillRectangle(Color.FromArgb(200, 20, 20, 30),
            new Rectangle(x, y, tableWidth, tableHeight));

        // Border
        context.DrawRectangle(new RenderPen(Color.FromArgb(120, 255, 255, 255), 1),
            new Rectangle(x, y, tableWidth, tableHeight));

        // Draw text lines
        var headerColor = Color.FromArgb(255, 200, 200, 200);
        var bodyColor = Color.White;

        for (int i = 0; i < lines.Count; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
                continue;

            var font = i < 2 ? _tableHeaderFont : _tableBodyFont;
            var color = i < 2 ? headerColor : bodyColor;

            context.DrawString(lines[i], font, color,
                x + padding, y + padding + i * lineHeight);
        }
    }

    private static string FormatRow(string label, (int total, int continued, int stopped) stats)
    {
        if (stats.total == 0)
            return $"{label,-12}  n=0";

        double contPct = 100.0 * stats.continued / stats.total;
        double stopPct = 100.0 * stats.stopped / stats.total;

        return $"{label,-12}  n={stats.total,4}  cont={contPct,5:F1}%  stop={stopPct,5:F1}%";
    }
}
