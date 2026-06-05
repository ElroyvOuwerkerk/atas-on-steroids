using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATAS.Indicators;
using ATAS.Indicators.Drawing;
using OFT.Rendering.Settings;

[DisplayName("Zone Alert Notifier")]
[Category("Custom")]
public class ZoneAlertNotifier : Indicator
{
    #region State

    private static readonly HttpClient Http = new();
    private readonly Dictionary<string, DateTime> _lastAlertTimes = new();
    private readonly ValueDataSeries _dummySeries = new("ZoneAlertState") { IsHidden = true };
    private bool _startupSent = false;
    private int _lastCalculatedBar = -1;
    private bool _liveMode = false;

    #endregion

    #region Settings

    private string _telegramBotToken = "";
    [Display(Name = "Bot Token", GroupName = "1) Telegram", Order = 10,
        Description = "Telegram bot token from @BotFather")]
    public string TelegramBotToken
    {
        get => _telegramBotToken;
        set => _telegramBotToken = value;
    }

    private string _telegramChatId = "";
    [Display(Name = "Chat ID", GroupName = "1) Telegram", Order = 20,
        Description = "Telegram chat ID to send alerts to")]
    public string TelegramChatId
    {
        get => _telegramChatId;
        set => _telegramChatId = value;
    }

    private int _cooldownMinutes = 30;
    [Display(Name = "Cooldown (minutes)", GroupName = "2) Alerts", Order = 30,
        Description = "Minimum minutes between alerts for the same zone")]
    [Range(1, 1440)]
    public int CooldownMinutes
    {
        get => _cooldownMinutes;
        set => _cooldownMinutes = value;
    }

    private bool _filterByColor = false;
    [Display(Name = "Filter by Color", GroupName = "3) Filter", Order = 40,
        Description = "Only alert for zones matching the specified color")]
    public bool FilterByColor
    {
        get => _filterByColor;
        set => _filterByColor = value;
    }

    private Color _zoneColor = Color.White;
    [Display(Name = "Zone Color", GroupName = "3) Filter", Order = 50,
        Description = "Only Highlight Y zones drawn in this color will trigger alerts")]
    public Color ZoneColor
    {
        get => _zoneColor;
        set => _zoneColor = value;
    }

    #endregion

    #region Constructor

    public ZoneAlertNotifier()
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

        // Detect when historical loading is complete.
        if (!_liveMode)
        {
            if (bar <= _lastCalculatedBar && _lastCalculatedBar > 0)
                _liveMode = true;
            _lastCalculatedBar = Math.Max(_lastCalculatedBar, bar);
            if (!_liveMode)
                return;
        }

        // Only process the current live candle
        if (bar < CurrentBar - 1)
            return;

        if (string.IsNullOrWhiteSpace(_telegramBotToken) || string.IsNullOrWhiteSpace(_telegramChatId))
            return;

        if (!_startupSent)
        {
            _startupSent = true;
            _ = SendTelegramAsync(
                "\u2705 Zone Alert Notifier started — " + (InstrumentInfo?.Instrument ?? "Unknown"),
                false);
        }

        var drawingObjects = ChartInfo?.DrawingObjectsListInfo;
        if (drawingObjects == null)
            return;

        List<object> channels;
        try
        {
            var prop = drawingObjects.GetType().GetProperty("HorizontalChannels");
            if (prop == null)
                return;

            var val = prop.GetValue(drawingObjects);
            if (val is not System.Collections.IEnumerable enumerable)
                return;

            channels = new List<object>();
            foreach (var item in enumerable)
                channels.Add(item);
        }
        catch
        {
            return;
        }

        if (channels.Count == 0)
            return;

        var candle = GetCandle(bar);
        decimal h = candle.High;
        decimal l = candle.Low;
        decimal c = candle.Close;
        decimal o = candle.Open;

        foreach (var channel in channels)
        {
            try
            {
                ProcessChannel(channel, h, l, c, o);
            }
            catch { }
        }
    }

    private void ProcessChannel(object channel, decimal h, decimal l, decimal c, decimal o)
    {
        var chType = channel.GetType();

        // Read price boundaries — both must be valid positive prices.
        // (When a zone is being drawn, one bound may briefly be 0.)
        decimal firstPrice = GetDecimalProp(chType, channel, "FirstPrice");
        decimal secondPrice = GetDecimalProp(chType, channel, "SecondPrice");
        if (firstPrice <= 0 || secondPrice <= 0)
            return;

        decimal topPrice = Math.Max(firstPrice, secondPrice);
        decimal bottomPrice = Math.Min(firstPrice, secondPrice);

        if (topPrice == bottomPrice)
            return;

        // Optional color filter
        if (_filterByColor && !MatchesColor(chType, channel))
            return;

        // Price intersection check
        bool touches = l <= topPrice && h >= bottomPrice;
        if (!touches)
            return;

        // Cooldown check
        string zoneKey = $"{topPrice}_{bottomPrice}";
        var now = DateTime.Now;
        if (_lastAlertTimes.TryGetValue(zoneKey, out var lastAlert)
            && (now - lastAlert).TotalMinutes < _cooldownMinutes)
            return;

        _lastAlertTimes[zoneKey] = now;

        // Read zone color for context
        string colorName = GetZoneColorName(chType, channel);

        // Determine approach direction
        string direction = o < bottomPrice ? "Approaching from below"
                         : o > topPrice    ? "Approaching from above"
                         : "Inside zone";

        string instrument = InstrumentInfo?.Instrument ?? "Unknown";

        var sb = new StringBuilder();
        sb.AppendLine("\U0001F4E6 Zone Alert");
        sb.AppendLine("Instrument: " + instrument);
        if (!string.IsNullOrEmpty(colorName))
            sb.AppendLine("Color: " + colorName);
        sb.AppendLine("Price: " + c);
        sb.AppendLine("Zone: " + bottomPrice + " - " + topPrice);
        sb.AppendLine(direction);

        _ = SendTelegramAsync(sb.ToString(), false);
    }

    #endregion

    #region Color Reading

    private static string GetZoneColorName(Type chType, object channel)
    {
        try
        {
            // Read from Background property (confirmed available on HorizontalChannel)
            var bgProp = chType.GetProperty("Background");
            if (bgProp == null)
                return "";

            var val = bgProp.GetValue(channel);
            if (val is not Color color)
                return "";

            // Map common RGB values to readable names
            int r = color.R, g = color.G, b = color.B;

            if (r > 200 && g < 80 && b < 80) return "\U0001F534 Red";
            if (r < 80 && g > 200 && b < 80) return "\U0001F7E2 Green";
            if (r < 80 && g < 80 && b > 200) return "\U0001F535 Blue";
            if (r > 200 && g > 200 && b < 80) return "\U0001F7E1 Yellow";
            if (r > 200 && g > 100 && g < 180 && b < 80) return "\U0001F7E0 Orange";
            if (r > 150 && g < 80 && b > 150) return "\U0001F7E3 Purple";
            if (r > 200 && g > 200 && b > 200) return "\u26AA White";
            if (r < 80 && g > 200 && b > 200) return "Cyan";
            if (r > 200 && g < 80 && b > 200) return "Magenta";

            return $"RGB({r},{g},{b})";
        }
        catch
        {
            return "";
        }
    }

    #endregion

    #region Telegram

    private Task SendTelegramAsync(string message, bool useMarkdown = false)
    {
        return Task.Run(async () =>
        {
            try
            {
                string escapedMsg = message
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n")
                    .Replace("\r", "");

                string json = useMarkdown
                    ? "{\"chat_id\":\"" + _telegramChatId + "\",\"text\":\"" + escapedMsg + "\",\"parse_mode\":\"Markdown\"}"
                    : "{\"chat_id\":\"" + _telegramChatId + "\",\"text\":\"" + escapedMsg + "\"}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await Http.PostAsync(
                    "https://api.telegram.org/bot" + _telegramBotToken + "/sendMessage",
                    content);
            }
            catch { }
        });
    }

    private static string EscapeMarkdown(string text)
    {
        return text
            .Replace("_", "\\_")
            .Replace("*", "\\*")
            .Replace("[", "\\[")
            .Replace("`", "\\`");
    }

    #endregion

    #region Reflection Helpers

    private static decimal GetDecimalProp(Type type, object obj, string name)
    {
        var prop = type.GetProperty(name);
        if (prop == null) return 0;
        var val = prop.GetValue(obj);
        if (val is decimal d) return d;
        if (val is double dbl) return (decimal)dbl;
        if (val is float f) return (decimal)f;
        return 0;
    }

    private static string GetStringProp(Type type, object obj, string name)
    {
        var prop = type.GetProperty(name);
        if (prop == null) return "";
        var val = prop.GetValue(obj);
        return val as string ?? "";
    }

    private bool MatchesColor(Type chType, object channel)
    {
        try
        {
            // Use Background for color matching (confirmed property on HorizontalChannel)
            var bgProp = chType.GetProperty("Background");
            if (bgProp == null)
                return true;

            var val = bgProp.GetValue(channel);
            if (val is not Color lc)
                return true;

            return lc.R == _zoneColor.R
                && lc.G == _zoneColor.G
                && lc.B == _zoneColor.B;
        }
        catch
        {
            return true;
        }
    }

    #endregion
}
