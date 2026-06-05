using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using ATAS.Indicators;
using ATAS.Indicators.Drawing;
using OFT.Rendering.Settings;

[DisplayName("Opening Range Breakout")]
[Category("Custom")]
public class OpeningRangeBreakout : Indicator
{
    #region State

    private decimal _orbHigh = decimal.MinValue;
    private decimal _orbLow = decimal.MaxValue;
    private bool _orbComplete;
    private int _lastProcessedBar = -1;

    private readonly LineSeries _orbHighLine = new("orb high") { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _orbLowLine  = new("orb low")  { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _t1UpLine    = new("T1 up")     { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _t2UpLine    = new("T2 up")     { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _t1DownLine  = new("T1 down")   { Value = 0, UseScale = true, IsHidden = true };
    private readonly LineSeries _t2DownLine  = new("T2 down")   { Value = 0, UseScale = true, IsHidden = true };

    private bool _orbHasValue;

    #endregion

    #region Settings — Session

    private TimeSpan _rthOpen = new(9, 30, 0);
    [Display(Name = "RTH Open Time (HH:mm:ss)", GroupName = "0) Session", Order = 1,
        Description = "Start of the opening range window (exchange-local time).")]
    public TimeSpan RthOpenTime { get => _rthOpen; set { _rthOpen = value; RecalculateValues(); } }

    private int _orbMinutes = 15;
    [Display(Name = "ORB Duration (minutes)", GroupName = "0) Session", Order = 2,
        Description = "Length of the opening range window in minutes.")]
    [Range(1, 120)]
    public int OrbMinutes { get => _orbMinutes; set { _orbMinutes = value; RecalculateValues(); } }

    #endregion

    #region Settings — ORB Lines

    private Color _orbColor = Color.Yellow;
    [Display(Name = "Color", GroupName = "1) ORB Lines", Order = 10)]
    public Color OrbColor { get => _orbColor; set { _orbColor = value; RecalculateValues(); } }

    private int _orbWidth = 1;
    [Display(Name = "Width", GroupName = "1) ORB Lines", Order = 11)]
    [Range(1, 5)]
    public int OrbWidth { get => _orbWidth; set { _orbWidth = value; RecalculateValues(); } }

    private string _orbHighLabel = "orb high";
    [Display(Name = "High Label", GroupName = "1) ORB Lines", Order = 12)]
    public string OrbHighLabel { get => _orbHighLabel; set { _orbHighLabel = value ?? ""; RecalculateValues(); } }

    private string _orbLowLabel = "orb low";
    [Display(Name = "Low Label", GroupName = "1) ORB Lines", Order = 13)]
    public string OrbLowLabel { get => _orbLowLabel; set { _orbLowLabel = value ?? ""; RecalculateValues(); } }

    #endregion

    #region Settings — Target Lines

    private Color _targetColor = Color.FromArgb(255, 0, 200, 200);
    [Display(Name = "Color", GroupName = "2) Target Lines", Order = 20)]
    public Color TargetColor { get => _targetColor; set { _targetColor = value; RecalculateValues(); } }

    private int _targetWidth = 1;
    [Display(Name = "Width", GroupName = "2) Target Lines", Order = 21)]
    [Range(1, 5)]
    public int TargetWidth { get => _targetWidth; set { _targetWidth = value; RecalculateValues(); } }

    private decimal _t1Pct = 100m;
    [Display(Name = "T1 Extension %", GroupName = "2) Target Lines", Order = 22,
        Description = "First target as a percentage of the ORB range.")]
    [Range(10.0, 500.0)]
    public decimal T1Pct { get => _t1Pct; set { _t1Pct = value; RecalculateValues(); } }

    private decimal _t2Pct = 150m;
    [Display(Name = "T2 Extension %", GroupName = "2) Target Lines", Order = 23,
        Description = "Second target as a percentage of the ORB range.")]
    [Range(10.0, 500.0)]
    public decimal T2Pct { get => _t2Pct; set { _t2Pct = value; RecalculateValues(); } }

    private string _t1Label = "T1";
    [Display(Name = "T1 Label", GroupName = "2) Target Lines", Order = 24)]
    public string T1Label { get => _t1Label; set { _t1Label = value ?? ""; RecalculateValues(); } }

    private string _t2Label = "T2";
    [Display(Name = "T2 Label", GroupName = "2) Target Lines", Order = 25)]
    public string T2Label { get => _t2Label; set { _t2Label = value ?? ""; RecalculateValues(); } }

    #endregion

    #region Constructor

    public OpeningRangeBreakout()
    {
        Panel = IndicatorDataProvider.CandlesPanel;
        DataSeries.Add(new ValueDataSeries("ORB_dummy") { IsHidden = true });

        LineSeries.Add(_orbHighLine);
        LineSeries.Add(_orbLowLine);
        LineSeries.Add(_t1UpLine);
        LineSeries.Add(_t2UpLine);
        LineSeries.Add(_t1DownLine);
        LineSeries.Add(_t2DownLine);
    }

    #endregion

    #region Calculation

    protected override void OnRecalculate()
    {
        _orbHigh = decimal.MinValue;
        _orbLow = decimal.MaxValue;
        _orbComplete = false;
        _lastProcessedBar = -1;
        _orbHasValue = false;
    }

    protected override void OnCalculate(int bar, decimal value)
    {
        if (bar <= _lastProcessedBar)
        {
            SyncLines();
            return;
        }

        var candle = GetCandle(bar);
        bool newSession = bar == 0 || IsNewSession(bar);

        if (newSession)
        {
            _orbHigh = decimal.MinValue;
            _orbLow = decimal.MaxValue;
            _orbComplete = false;
            _orbHasValue = false;
        }

        if (!_orbComplete)
        {
            var orbEnd = _rthOpen + TimeSpan.FromMinutes(_orbMinutes);
            if (BarInWindow(candle, _rthOpen, orbEnd))
            {
                if (candle.High > _orbHigh) _orbHigh = candle.High;
                if (candle.Low < _orbLow) _orbLow = candle.Low;
                _orbHasValue = _orbHigh > decimal.MinValue && _orbLow < decimal.MaxValue;
            }
            else if (candle.Time.TimeOfDay >= orbEnd)
            {
                _orbComplete = true;
            }
        }

        _lastProcessedBar = bar;
        SyncLines();
    }

    private bool BarInWindow(IndicatorCandle candle, TimeSpan start, TimeSpan end)
    {
        if (start == end) return false;
        var t = candle.Time.TimeOfDay;
        if (end > start)
            return t >= start && t < end;
        return t >= start || t < end;
    }

    private void SyncLines()
    {
        bool showTargets = _orbHasValue;

        ApplyLine(_orbHighLine, _orbHasValue, _orbHighLabel, _orbColor, _orbWidth);
        ApplyLine(_orbLowLine, _orbHasValue, _orbLowLabel, _orbColor, _orbWidth);
        ApplyLine(_t1UpLine, showTargets, _t1Label, _targetColor, _targetWidth);
        ApplyLine(_t2UpLine, showTargets, _t2Label, _targetColor, _targetWidth);
        ApplyLine(_t1DownLine, showTargets, _t1Label, _targetColor, _targetWidth);
        ApplyLine(_t2DownLine, showTargets, _t2Label, _targetColor, _targetWidth);

        if (_orbHasValue)
        {
            _orbHighLine.Value = _orbHigh;
            _orbLowLine.Value = _orbLow;

            var range = _orbHigh - _orbLow;
            _t1UpLine.Value = _orbHigh + range * (_t1Pct / 100m);
            _t2UpLine.Value = _orbHigh + range * (_t2Pct / 100m);
            _t1DownLine.Value = _orbLow - range * (_t1Pct / 100m);
            _t2DownLine.Value = _orbLow - range * (_t2Pct / 100m);
        }
    }

    private static void ApplyLine(LineSeries ls, bool visible, string label, Color color, int width)
    {
        ls.IsHidden = !visible;
        if (!string.IsNullOrWhiteSpace(label))
            ls.Name = label;
        ls.Color = color;
        ls.Width = width;
        ls.LineDashStyle = LineDashStyle.Dot;
    }

    #endregion
}
