using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATAS.DataFeedsCore;
using ATAS.Indicators;
using ATAS.Strategies.Chart;
using Newtonsoft.Json;

[DisplayName("CC Trade Logger")]
[Category("Custom")]
public class TradeLogger : ChartStrategy
{
    #region Signal Codes

    private static readonly string[] SignalNames =
    {
        "None",            // 0
        "COT_Bull",        // 1
        "COT_Bear",        // 2
        "COT_1Candle_Bull",// 3
        "COT_1Candle_Bear",// 4
        "MCoC_Bull",       // 5
        "MCoC_Bear",       // 6
        "COC_Bull",        // 7
        "COC_Bear",        // 8
        "Moon_Bull",       // 9
        "Moon_Bear",       // 10
    };

    #endregion

    #region Internal State

    private static readonly HttpClient Http = new();

    private bool _inPosition;
    private OrderDirections _entryDirection;
    private decimal _entryPrice;
    private decimal _entrySize;
    private DateTime _entryTime;
    private int _entryBar;
    private string _entrySignal = "None";
    private string _entrySignalSource = "";

    // Internal indicator for signal reading
    private readonly CCSuiteLevels _ccLevels = new();

    #endregion

    #region Settings

    private string _webhookUrl = "http://localhost:3001/api/trades";
    [Display(Name = "Webhook URL", GroupName = "1) Backend", Order = 10)]
    public string WebhookUrl
    {
        get => _webhookUrl;
        set => _webhookUrl = value;
    }

    private bool _enableObs = true;
    [Display(Name = "Enable OBS Replay", GroupName = "2) OBS", Order = 20)]
    public bool EnableObs
    {
        get => _enableObs;
        set => _enableObs = value;
    }

    private string _obsUrl = "ws://localhost:4455";
    [Display(Name = "OBS WebSocket URL", GroupName = "2) OBS", Order = 30)]
    public string ObsUrl
    {
        get => _obsUrl;
        set => _obsUrl = value;
    }

    private string _obsPassword = "";
    [Display(Name = "OBS WebSocket Password", GroupName = "2) OBS", Order = 40)]
    public string ObsPassword
    {
        get => _obsPassword;
        set => _obsPassword = value;
    }

    private int _replayDelaySeconds = 2;
    [Display(Name = "Replay Save Delay (seconds)", GroupName = "2) OBS", Order = 50)]
    [Range(0, 30)]
    public int ReplayDelaySeconds
    {
        get => _replayDelaySeconds;
        set => _replayDelaySeconds = value;
    }

    private int _signalLookbackBars = 3;
    [Display(Name = "Signal Lookback Bars", GroupName = "3) Signals", Order = 60,
        Description = "How many bars back to search for a signal at trade entry")]
    [Range(1, 10)]
    public int SignalLookbackBars
    {
        get => _signalLookbackBars;
        set => _signalLookbackBars = value;
    }

    #endregion

    #region Constructor

    public TradeLogger()
    {
        Panel = IndicatorDataProvider.CandlesPanel;

        // Register internal indicator (hidden, for signal reading only)
        Add(_ccLevels);
    }

    #endregion

    #region Calculation

    protected override void OnCalculate(int bar, decimal value)
    {
        // Internal indicators auto-calculate via Add().
        // Fallback position detection: check if CurrentPosition changed.
        // This handles cases where OnNewMyTrade doesn't fire for manual trades.
        if (bar == CurrentBar - 1)
            CheckPositionState(bar);
    }

    #endregion

    #region Trade Events

    protected override void OnNewMyTrade(MyTrade myTrade)
    {
        base.OnNewMyTrade(myTrade);
        CheckPositionState(CurrentBar - 1);
    }

    private void CheckPositionState(int bar)
    {
        decimal pos = CurrentPosition;
        bool nowInPosition = pos != 0;

        if (!_inPosition && nowInPosition)
        {
            // Position opened
            _inPosition = true;
            _entryDirection = pos > 0 ? OrderDirections.Buy : OrderDirections.Sell;
            _entrySize = Math.Abs(pos);
            _entryBar = bar;
            _entryTime = DateTime.UtcNow;

            // Read entry price from the latest fill
            _entryPrice = 0;
            foreach (var t in MyTrades)
            {
                _entryPrice = t.Price;
            }

            // Read the active signal
            ReadSignalAtBar(bar);
        }
        else if (_inPosition && !nowInPosition)
        {
            // Position closed
            _inPosition = false;

            decimal exitPrice = 0;
            foreach (var t in MyTrades)
            {
                exitPrice = t.Price;
            }

            decimal pnl = _entryDirection == OrderDirections.Buy
                ? (exitPrice - _entryPrice) * _entrySize
                : (_entryPrice - exitPrice) * _entrySize;

            var payload = new
            {
                instrument = InstrumentInfo?.Instrument ?? "Unknown",
                direction = _entryDirection == OrderDirections.Buy ? "Long" : "Short",
                entry_price = _entryPrice,
                exit_price = exitPrice,
                size = _entrySize,
                pnl = pnl,
                entry_time = _entryTime.ToString("o"),
                exit_time = DateTime.UtcNow.ToString("o"),
                signal = _entrySignal,
                signal_source = _entrySignalSource,
            };

            _ = SendWebhookAsync(payload);

            if (_enableObs)
                _ = TriggerObsReplayAsync();
        }
    }

    #endregion

    #region Signal Reading

    private void ReadSignalAtBar(int bar)
    {
        _entrySignal = "None";
        _entrySignalSource = "";

        for (int i = 0; i < _signalLookbackBars && bar - i >= 0; i++)
        {
            int checkBar = bar - i;
            int code = GetSignalCode(_ccLevels, checkBar);
            if (code > 0 && code < SignalNames.Length)
            {
                _entrySignal = SignalNames[code];
                _entrySignalSource = "CC Suite Levels";
                return;
            }
        }
    }

    private static int GetSignalCode(Indicator indicator, int bar)
    {
        // The signal series is added to the indicator's DataSeries list.
        // Find it by name "Signal".
        foreach (var ds in indicator.DataSeries)
        {
            if (ds is ValueDataSeries vds && vds.Name == "Signal")
            {
                decimal val = vds[bar];
                return (int)val;
            }
        }
        return 0;
    }

    #endregion

    #region Webhook

    private async Task SendWebhookAsync(object payload)
    {
        try
        {
            string json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Http.PostAsync(_webhookUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"Trade Logger webhook failed: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Trade Logger webhook error: {ex.Message}");
        }
    }

    #endregion

    #region OBS

    private async Task TriggerObsReplayAsync()
    {
        try
        {
            if (_replayDelaySeconds > 0)
                await Task.Delay(TimeSpan.FromSeconds(_replayDelaySeconds));

            using var obs = new TradeLoggerPlugin.ObsClient(_obsUrl, _obsPassword);
            await obs.ConnectAsync();
            await obs.SaveReplayBufferAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Trade Logger OBS error: {ex.Message}");
        }
    }

    #endregion
}
