using System;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TradeLoggerPlugin
{
    /// <summary>
    /// Lightweight OBS WebSocket v5 client.
    /// Supports connect, authenticate, and SaveReplayBuffer.
    /// </summary>
    public class ObsClient : IDisposable
    {
        private ClientWebSocket _ws;
        private readonly string _url;
        private readonly string _password;
        private int _requestId;
        private bool _identified;

        public ObsClient(string url, string password)
        {
            _url = url;
            _password = password ?? "";
        }

        public async Task ConnectAsync(CancellationToken ct = default)
        {
            _ws = new ClientWebSocket();
            _identified = false;
            await _ws.ConnectAsync(new Uri(_url), ct);

            // Receive Hello (op 0)
            var hello = await ReceiveMessageAsync(ct);
            var helloData = JObject.Parse(hello);
            int opCode = helloData["op"]?.Value<int>() ?? -1;

            if (opCode != 0)
                throw new Exception($"Expected Hello (op 0), got op {opCode}");

            var d = helloData["d"];
            var auth = d?["authentication"];

            // Build Identify message (op 1)
            var identify = new JObject
            {
                ["op"] = 1,
                ["d"] = new JObject
                {
                    ["rpcVersion"] = 1,
                }
            };

            if (auth != null && !string.IsNullOrEmpty(_password))
            {
                string challenge = auth["challenge"]?.Value<string>() ?? "";
                string salt = auth["salt"]?.Value<string>() ?? "";
                string authResponse = GenerateAuthResponse(_password, salt, challenge);
                ((JObject)identify["d"])["authentication"] = authResponse;
            }

            await SendMessageAsync(identify.ToString(Formatting.None), ct);

            // Receive Identified (op 2)
            var identifiedMsg = await ReceiveMessageAsync(ct);
            var identifiedData = JObject.Parse(identifiedMsg);
            int identifiedOp = identifiedData["op"]?.Value<int>() ?? -1;

            if (identifiedOp != 2)
                throw new Exception($"Authentication failed, got op {identifiedOp}");

            _identified = true;
        }

        public async Task SaveReplayBufferAsync(CancellationToken ct = default)
        {
            if (!_identified || _ws?.State != WebSocketState.Open)
                throw new InvalidOperationException("Not connected to OBS");

            var requestId = Interlocked.Increment(ref _requestId).ToString();
            var request = new JObject
            {
                ["op"] = 6,
                ["d"] = new JObject
                {
                    ["requestType"] = "SaveReplayBuffer",
                    ["requestId"] = requestId,
                }
            };

            await SendMessageAsync(request.ToString(Formatting.None), ct);

            // Read the response (op 7)
            var response = await ReceiveMessageAsync(ct);
            var responseData = JObject.Parse(response);
            var status = responseData["d"]?["requestStatus"];
            bool success = status?["result"]?.Value<bool>() ?? false;

            if (!success)
            {
                string code = status?["code"]?.Value<string>() ?? "unknown";
                throw new Exception($"SaveReplayBuffer failed: {code}");
            }
        }

        private static string GenerateAuthResponse(string password, string salt, string challenge)
        {
            using var sha256 = SHA256.Create();

            // secret = base64(sha256(password + salt))
            byte[] secretHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string secret = Convert.ToBase64String(secretHash);

            // auth = base64(sha256(secret + challenge))
            byte[] authHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(secret + challenge));
            return Convert.ToBase64String(authHash);
        }

        private async Task SendMessageAsync(string message, CancellationToken ct)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await _ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, ct);
        }

        private async Task<string> ReceiveMessageAsync(CancellationToken ct)
        {
            var buffer = new byte[8192];
            var sb = new StringBuilder();

            WebSocketReceiveResult result;
            do
            {
                result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
            }
            while (!result.EndOfMessage);

            return sb.ToString();
        }

        public void Dispose()
        {
            if (_ws != null)
            {
                if (_ws.State == WebSocketState.Open)
                {
                    try
                    {
                        _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None)
                            .GetAwaiter().GetResult();
                    }
                    catch { }
                }
                _ws.Dispose();
                _ws = null;
            }
        }
    }
}
