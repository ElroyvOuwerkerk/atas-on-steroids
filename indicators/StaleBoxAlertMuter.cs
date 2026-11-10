using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ATAS.Indicators;
using ATAS.Indicators.Drawing;
using OFT.Rendering.Context;
using OFT.Rendering.Settings;
using OFT.Rendering.Tools;

// ═══════════════════════════════════════════════════════════════════════════
//  STALE BOX ALERT MUTER
//
//  ATAS fires a rectangle's approximation alert purely on PRICE — it never
//  looks at the box's time anchors. So a box drawn around last Tuesday's
//  range keeps pinging forever, every time price revisits that band. This
//  indicator switches a box's alert off once the whole box sits in the past.
//
//  HOW IT REACHES THE BOXES
//  The documented API is a dead end: ChartInfo.DrawingObjectsListInfo
//  (ATAS.Indicators.IDrawingObjectsListInfo) declares only HorizontalLines
//  and HorizontalChannels. Rectangles are not on it.
//
//  The object behind that interface is
//  OFT.Platform.ViewModels.Charting.ChartData+DrawingObjectsListCollection,
//  which holds a private field of type
//  OFT.Platform.DrawingObjects.DrawingObjectsCollection. That class has two
//  PUBLIC methods returning every drawing object on the chart:
//
//      ICollection<DrawingObject> GetAllObjects(Instrument)          <- preferred
//      IEnumerable<DrawingObject> GetObjectsForSnapshot(bool global) <- fallback
//
//  Both bottom out in GlobalTradingObjectsStore, which copies REFERENCES into
//  a fresh List — no cloning — so the objects handed back are the live ones the
//  alert engine ticks (verified by decompiling OFT.Platform.Core, ATAS X
//  8.0.15.640). Mutating them mutates what ATAS alerts on.
//
//  RectangleObj.OnNewTicks() opens with `if (!UseApproximationAlert) return;`,
//  so clearing that one bool kills the alert from the very next tick.
//
//  FRAGILITY NOTE — READ BEFORE UPDATING ATAS
//  Field names inside OFT.Platform.Core are obfuscated (the collection field is
//  literally called `#=zb0NzAVH7_Gph`), so this code finds it by FIELD TYPE
//  NAME, never by field name. Public method and property names are not
//  obfuscated and are matched normally. If a future ATAS build moves things,
//  every lookup fails soft: the indicator does nothing and says so on the chart
//  instead of throwing.
//
//  SIDE EFFECT
//  Muting edits the box's own setting, so it persists once you save the
//  workspace. It is plain reversible state (tick "Use alert" back on in the
//  box's properties). Run with "Dry run" on first — it reports what it would
//  touch and changes nothing.
// ═══════════════════════════════════════════════════════════════════════════

/// <summary>Which clock "in the past" is measured against.</summary>
public enum StaleBoxTimeBase
{
    /// <summary>Time of the newest candle (same time axis the box is drawn on).</summary>
    LastCandle,

    /// <summary>Machine local time.</summary>
    LocalNow,

    /// <summary>UTC.</summary>
    UtcNow,
}

[DisplayName("Stale Box Alert Muter")]
[Category("Custom")]
public class StaleBoxAlertMuter : Indicator
{
    #region Nested

    /// <summary>Reflection handles for one drawing-object type, resolved once.</summary>
    private sealed class BoxAccessors
    {
        public PropertyInfo FirstTime;
        public PropertyInfo SecondTime;
        public PropertyInfo ExtendRight;
        public PropertyInfo UseAlert;
        public PropertyInfo Id;
        public PropertyInfo Text;
        public PropertyInfo InstrumentName;
        public PropertyInfo InstrumentUniqName;
        public PropertyInfo TrueFirstPrice;
        public PropertyInfo TrueSecondPrice;

        public bool Usable => FirstTime != null && SecondTime != null && UseAlert != null;
    }

    #endregion

    #region State

    private readonly ValueDataSeries _dummySeries = new("StaleBoxMuterState") { IsHidden = true };

    // Ids of boxes THIS indicator muted. Re-enabling is limited to these so a
    // box you muted by hand is never switched back on behind your back.
    private readonly HashSet<string> _mutedByUs = new(StringComparer.Ordinal);

    private readonly Dictionary<Type, BoxAccessors> _accessors = new();

    private FieldInfo _collectionField;
    private PropertyInfo _instrumentProp;
    private MethodInfo _getAllObjects;
    private MethodInfo _getSnapshot;
    private bool _reflectionResolved;

    private DateTime _lastScanUtc = DateTime.MinValue;

    // Last scan result, for the on-chart status block.
    private string _status = "not run yet";
    private string _error = "";
    private int _boxCount;
    private int _staleCount;
    private int _mutedCount;      // cumulative for this session
    private int _restoredCount;   // cumulative for this session
    private DateTime _referenceTime;
    // Swapped wholesale, never mutated in place: OnRender reads it from the
    // render thread while OnCalculate writes it from the calculation thread.
    private string[] _preview = Array.Empty<string>();
    private string _lastPreviewKey = "";
    private bool _writeVerifyFailed;
    private bool _usedFallbackList;

    private readonly RenderFont _titleFont = new("Arial", 11);
    private readonly RenderFont _bodyFont  = new("Consolas", 10);

    #endregion

    #region Settings

    private bool _enabled = true;
    [Display(Name = "Enabled", GroupName = "1) Behaviour", Order = 10,
        Description = "Master switch. Off = the indicator does nothing at all.")]
    public bool IsEnabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    private bool _dryRun = true;
    [Display(Name = "Dry run", GroupName = "1) Behaviour", Order = 20,
        Description = "Report which boxes WOULD be muted without changing anything. " +
                      "Turn this off to arm the indicator.")]
    public bool DryRun
    {
        get => _dryRun;
        set => _dryRun = value;
    }

    private int _graceMinutes = 0;
    [Display(Name = "Grace (minutes)", GroupName = "1) Behaviour", Order = 30,
        Description = "A box must be past by at least this long before its alert is muted. " +
                      "Also absorbs a small clock mismatch between box anchors and candles.")]
    [Range(0, 10080)]
    public int GraceMinutes
    {
        get => _graceMinutes;
        set => _graceMinutes = value;
    }

    private bool _reEnable = false;
    [Display(Name = "Re-enable when box is current again", GroupName = "1) Behaviour", Order = 40,
        Description = "If you drag a muted box back into the present, switch its alert on again. " +
                      "Only applies to boxes this indicator muted during this session.")]
    public bool ReEnable
    {
        get => _reEnable;
        set => _reEnable = value;
    }

    private StaleBoxTimeBase _timeBase = StaleBoxTimeBase.LastCandle;
    [Display(Name = "Time reference", GroupName = "2) Scope", Order = 50,
        Description = "What 'now' means. Last candle is safest: box anchors and candles share " +
                      "the same time axis, so there is no timezone mismatch.")]
    public StaleBoxTimeBase TimeBase
    {
        get => _timeBase;
        set => _timeBase = value;
    }

    private int _scanSeconds = 10;
    [Display(Name = "Scan interval (seconds)", GroupName = "2) Scope", Order = 60,
        Description = "How often the drawing objects are walked.")]
    [Range(1, 3600)]
    public int ScanSeconds
    {
        get => _scanSeconds;
        set => _scanSeconds = value;
    }

    private bool _showStatus = true;
    [Display(Name = "Show status on chart", GroupName = "3) Display", Order = 70)]
    public bool ShowStatus
    {
        get => _showStatus;
        set => _showStatus = value;
    }

    private TablePosition _statusPosition = TablePosition.TopLeft;
    [Display(Name = "Status position", GroupName = "3) Display", Order = 80)]
    public TablePosition StatusPosition
    {
        get => _statusPosition;
        set => _statusPosition = value;
    }

    private string _logPath = DefaultLogPath();
    [Display(Name = "Log file", GroupName = "3) Display", Order = 90,
        Description = "Every mute / re-enable is appended here. Leave empty for no file log.")]
    public string LogPath
    {
        get => _logPath;
        set => _logPath = value;
    }

    #endregion

    #region Constructor

    public StaleBoxAlertMuter()
    {
        Panel = IndicatorDataProvider.CandlesPanel;
        EnableCustomDrawing = true;
        SubscribeToDrawingEvents(DrawingLayouts.Final);
        DataSeries.Add(_dummySeries);
    }

    #endregion

    #region Calculation

    protected override void OnCalculate(int bar, decimal value)
    {
        _dummySeries[bar] = 0;

        if (!_enabled)
        {
            _status = "disabled";
            return;
        }

        // Only the live bar drives scans, and only on the interval — the walk
        // is cheap but there is no reason to run it during historical replay.
        if (bar < CurrentBar - 1)
            return;

        var now = DateTime.UtcNow;
        if ((now - _lastScanUtc).TotalSeconds < _scanSeconds)
            return;

        _lastScanUtc = now;

        try
        {
            Scan();
        }
        catch (Exception e)
        {
            _error = e.GetType().Name + ": " + e.Message;
        }
    }

    private void Scan()
    {
        _error = "";
        _writeVerifyFailed = false;

        var objects = GetDrawingObjects();
        if (objects == null)
            return;

        var reference = GetReferenceTime();
        var grace = TimeSpan.FromMinutes(_graceMinutes);

        int boxes = 0, stale = 0, muted = 0, restored = 0;
        var preview = new List<string>();

        foreach (var obj in objects)
        {
            if (obj == null)
                continue;

            var type = obj.GetType();
            if (type.Name != "RectangleObj")
                continue;

            var acc = GetAccessors(type);
            if (!acc.Usable)
                continue;

            if (_usedFallbackList && !BelongsToThisChart(obj, acc))
                continue;

            boxes++;

            var t1 = ToDateTime(acc.FirstTime.GetValue(obj));
            var t2 = ToDateTime(acc.SecondTime.GetValue(obj));

            // A box still being drawn can carry an unset anchor.
            if (t1 == default || t2 == default)
                continue;

            var rightEdge = t1 > t2 ? t1 : t2;

            bool extendsRight = acc.ExtendRight != null && ToBool(acc.ExtendRight.GetValue(obj));
            bool isPast = !extendsRight && rightEdge.Add(grace) < reference;
            bool alertOn = ToBool(acc.UseAlert.GetValue(obj));
            string id = acc.Id?.GetValue(obj) as string ?? "";

            if (isPast && alertOn)
            {
                stale++;
                string label = Describe(obj, acc, rightEdge);

                if (_dryRun)
                {
                    preview.Add(label);
                    continue;
                }

                acc.UseAlert.SetValue(obj, false);

                // Read back: if the platform ever hands out copies instead of
                // live objects, this is where we find out rather than silently
                // doing nothing.
                if (!ToBool(acc.UseAlert.GetValue(obj)))
                {
                    muted++;
                    if (id.Length > 0)
                        _mutedByUs.Add(id);
                    Log("muted   " + label);
                }
                else
                {
                    _writeVerifyFailed = true;
                }
            }
            else if (_reEnable && !isPast && !alertOn && id.Length > 0 && _mutedByUs.Contains(id))
            {
                acc.UseAlert.SetValue(obj, true);
                if (ToBool(acc.UseAlert.GetValue(obj)))
                {
                    restored++;
                    _mutedByUs.Remove(id);
                    Log("restored " + Describe(obj, acc, rightEdge));
                }
            }
        }

        _boxCount = boxes;
        _staleCount = _dryRun ? preview.Count : stale;
        // Cumulative for the session: a per-scan count would read 0 forever once
        // the backlog is cleared, which looks like the indicator is doing nothing.
        _mutedCount += muted;
        _restoredCount += restored;
        _referenceTime = reference;

        _preview = preview.ToArray();

        // Log the dry-run list once per distinct set, not once per scan.
        if (_dryRun && preview.Count > 0)
        {
            string key = string.Join("|", preview);
            if (key != _lastPreviewKey)
            {
                _lastPreviewKey = key;
                Log("dry run: " + preview.Count + " box(es) would be muted");
                foreach (var line in preview)
                    Log("  would mute " + line);
            }
        }

        _status = _dryRun ? "DRY RUN" : "active";
    }

    #endregion

    #region Drawing-object access

    /// <summary>
    /// Walks ChartInfo.DrawingObjectsListInfo down to the platform's live
    /// DrawingObjectsCollection. Returns null (with <see cref="_error"/> set)
    /// if any hop is missing, which is what a future ATAS refactor looks like.
    /// </summary>
    private IEnumerable GetDrawingObjects()
    {
        var listInfo = ChartInfo?.DrawingObjectsListInfo;
        if (listInfo == null)
        {
            _error = "no DrawingObjectsListInfo";
            return null;
        }

        if (!_reflectionResolved)
        {
            var listType = listInfo.GetType();

            // Match on field TYPE, never on field name — the names are obfuscated.
            _collectionField = listType
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(f => f.FieldType.Name == "DrawingObjectsCollection");

            _instrumentProp = listType.GetProperty("Instrument",
                BindingFlags.Instance | BindingFlags.Public);

            _reflectionResolved = true;
        }

        if (_collectionField == null)
        {
            _error = "DrawingObjectsCollection field not found (ATAS internals changed)";
            return null;
        }

        var collection = _collectionField.GetValue(listInfo);
        if (collection == null)
        {
            _error = "drawing collection is null";
            return null;
        }

        if (_getAllObjects == null && _getSnapshot == null)
        {
            var colType = collection.GetType();
            _getAllObjects = colType.GetMethod("GetAllObjects");
            _getSnapshot = colType.GetMethod("GetObjectsForSnapshot");
        }

        // GetAllObjects already scopes to this chart's instrument plus globals,
        // which is exactly the set of boxes you can see.
        var instrument = _instrumentProp?.GetValue(listInfo);
        if (_getAllObjects != null && instrument != null)
        {
            if (_getAllObjects.Invoke(collection, new[] { instrument }) is IEnumerable byInstrument)
            {
                _usedFallbackList = false;
                return byInstrument;
            }
        }

        if (_getSnapshot != null)
        {
            // GetObjectsForSnapshot filters on chart id and globals only, with no
            // instrument filter, so the caller has to scope it (see Scan).
            if (_getSnapshot.Invoke(collection, new object[] { true }) is IEnumerable snapshot)
            {
                _usedFallbackList = true;
                return snapshot;
            }
        }

        _error = "no accessible object list on DrawingObjectsCollection";
        return null;
    }

    /// <summary>
    /// Only needed on the fallback list, which is scoped by chart id rather than
    /// by instrument and so can carry another symbol's global boxes.
    /// </summary>
    private bool BelongsToThisChart(object obj, BoxAccessors acc)
    {
        string chart = InstrumentInfo?.Instrument;
        if (string.IsNullOrEmpty(chart))
            return true;

        string name = acc.InstrumentName?.GetValue(obj) as string;
        string uniq = acc.InstrumentUniqName?.GetValue(obj) as string;

        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(uniq))
            return true;

        return string.Equals(name, chart, StringComparison.OrdinalIgnoreCase)
            || string.Equals(uniq, chart, StringComparison.OrdinalIgnoreCase);
    }

    private BoxAccessors GetAccessors(Type type)
    {
        if (_accessors.TryGetValue(type, out var cached))
            return cached;

        var acc = new BoxAccessors
        {
            FirstTime       = type.GetProperty("_firstTime"),
            SecondTime      = type.GetProperty("_secondTime"),
            ExtendRight     = type.GetProperty("ExtendRight"),
            UseAlert        = type.GetProperty("UseApproximationAlert"),
            Id              = type.GetProperty("Id"),
            Text            = type.GetProperty("Text"),
            InstrumentName     = type.GetProperty("InstrumentName"),
            InstrumentUniqName = type.GetProperty("InstrumentUniqName"),
            TrueFirstPrice  = type.GetProperty("TrueFirstPrice"),
            TrueSecondPrice = type.GetProperty("TrueSecondPrice"),
        };

        _accessors[type] = acc;
        return acc;
    }

    private DateTime GetReferenceTime()
    {
        switch (_timeBase)
        {
            case StaleBoxTimeBase.LocalNow:
                return DateTime.Now;

            case StaleBoxTimeBase.UtcNow:
                return DateTime.UtcNow;

            default:
                int last = CurrentBar - 1;
                if (last < 0)
                    return DateTime.Now;

                var candle = GetCandle(last);
                return candle.LastTime > candle.Time ? candle.LastTime : candle.Time;
        }
    }

    #endregion

    #region Formatting / logging

    private static string Describe(object obj, BoxAccessors acc, DateTime rightEdge)
    {
        decimal p1 = ToDecimal(acc.TrueFirstPrice?.GetValue(obj));
        decimal p2 = ToDecimal(acc.TrueSecondPrice?.GetValue(obj));
        decimal low = Math.Min(p1, p2);
        decimal high = Math.Max(p1, p2);

        var sb = new StringBuilder();

        string name = acc.Text?.GetValue(obj) as string;
        if (!string.IsNullOrWhiteSpace(name))
            sb.Append(name.Trim()).Append(' ');

        sb.Append(low.ToString(CultureInfo.InvariantCulture))
          .Append('-')
          .Append(high.ToString(CultureInfo.InvariantCulture))
          .Append(" (ended ")
          .Append(rightEdge.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture))
          .Append(')');

        return sb.ToString();
    }

    private static string DefaultLogPath()
    {
        try
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string atas = Path.Combine(home, "Library", "Application Support", "ATAS");
            string dir = Directory.Exists(atas) ? atas : home;
            return Path.Combine(dir, "StaleBoxAlertMuter.log");
        }
        catch
        {
            return "";
        }
    }

    private void Log(string line)
    {
        if (string.IsNullOrWhiteSpace(_logPath))
            return;

        try
        {
            string stamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string chart = InstrumentInfo?.Instrument ?? "?";
            File.AppendAllText(_logPath, stamp + "  [" + chart + "] " + line + Environment.NewLine);
        }
        catch
        {
            // A log that cannot be written must never take the indicator down.
        }
    }

    private static DateTime ToDateTime(object v) => v is DateTime d ? d : default;

    private static bool ToBool(object v) => v is bool b && b;

    private static decimal ToDecimal(object v)
    {
        if (v is decimal d) return d;
        if (v is double dbl) return (decimal)dbl;
        if (v is float f) return (decimal)f;
        return 0m;
    }

    #endregion

    #region Rendering

    protected override void OnRender(RenderContext context, DrawingLayouts layout)
    {
        if (!_showStatus || ChartInfo == null)
            return;

        var lines = new List<(string text, Color color)>();

        Color ok = Color.FromArgb(210, 220, 230);
        Color warn = Color.FromArgb(255, 190, 80);
        Color bad = Color.FromArgb(255, 110, 110);

        if (!_enabled)
        {
            lines.Add(("disabled", warn));
        }
        else if (_error.Length > 0)
        {
            lines.Add((_error, bad));
        }
        else
        {
            lines.Add(("boxes " + _boxCount
                       + " · stale " + _staleCount
                       + (_dryRun ? "" : " · muted " + _mutedCount
                                         + (_restoredCount > 0 ? " · restored " + _restoredCount : "")),
                       ok));

            lines.Add(("ref " + _referenceTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                       + " (" + _timeBase + ")", Color.FromArgb(150, 160, 175)));

            if (_writeVerifyFailed)
                lines.Add(("write did not stick: ATAS returned copies", bad));

            var preview = _preview;
            for (int i = 0; i < preview.Length && i < 4; i++)
                lines.Add(("would mute: " + preview[i], warn));

            if (preview.Length > 4)
                lines.Add(("… and " + (preview.Length - 4) + " more", warn));
        }

        string title = "Stale Box Alert Muter" + (_enabled ? " · " + _status : "");

        var titleSize = context.MeasureString(title, _titleFont);
        int width = titleSize.Width;
        int lineHeight = 0;

        foreach (var line in lines)
        {
            var size = context.MeasureString(line.text, _bodyFont);
            if (size.Width > width) width = size.Width;
            if (size.Height > lineHeight) lineHeight = size.Height;
        }

        const int pad = 6;
        int boxWidth = width + pad * 2;
        int boxHeight = pad + titleSize.Height + 3 + lines.Count * lineHeight + pad;

        var bounds = context.ClipBounds;
        int x, y;

        switch (_statusPosition)
        {
            case TablePosition.TopRight:
                x = bounds.Right - boxWidth - 10;
                y = bounds.Y + 10;
                break;
            case TablePosition.BottomLeft:
                x = bounds.X + 10;
                y = bounds.Bottom - boxHeight - 10;
                break;
            case TablePosition.BottomRight:
                x = bounds.Right - boxWidth - 10;
                y = bounds.Bottom - boxHeight - 10;
                break;
            default:
                x = bounds.X + 10;
                y = bounds.Y + 10;
                break;
        }

        context.FillRectangle(Color.FromArgb(235, 10, 10, 18),
            new Rectangle(x, y, boxWidth, boxHeight));
        context.DrawRectangle(new RenderPen(Color.FromArgb(100, 80, 80, 100), 1),
            new Rectangle(x, y, boxWidth, boxHeight));

        int ty = y + pad;
        context.DrawString(title, _titleFont, Color.FromArgb(235, 235, 245), x + pad, ty);
        ty += titleSize.Height + 3;

        foreach (var line in lines)
        {
            context.DrawString(line.text, _bodyFont, line.color, x + pad, ty);
            ty += lineHeight;
        }
    }

    #endregion
}
