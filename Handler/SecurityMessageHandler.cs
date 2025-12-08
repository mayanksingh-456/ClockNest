using ClockNest.Enum;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace ClockNest.Handler
{
    public class SecurityMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private readonly IJSRuntime _js;
        private readonly ILogger<SecurityMessageHandler> _logger;

        public SecurityMessageHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IJSRuntime js, ILogger<SecurityMessageHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = configuration;
            _js = js;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var originalCulture = CultureInfo.CurrentCulture;

            try
            {
                // Set thread culture
                var newCulture = request.RequestUri.ToString().Contains("azurewebsites")
                    ? new CultureInfo("en-US")
                    : new CultureInfo("en-GB");

                Thread.CurrentThread.CurrentCulture = newCulture;

                // Prepare HMAC parts
                var nonce = Guid.NewGuid().ToString("N");

                var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                var deviceType = ((int)DeviceType.Web).ToString();
                var appVersion = "0";
                var hashedContent = string.Empty;
                string userId = "0"; // default value

                if (request.Headers.TryGetValues("X-User-Id", out var values))
                {
                    userId = values.FirstOrDefault() ?? "0";
                }


                _logger.LogInformation("SecurityMessageHandler. UserId={UserId}, Url={Url}, Method={Method}, Nonce={Nonce}, TimeStamp={TimeStamp}",
                     userId, request.RequestUri, request.Method, nonce, timeStamp);

                //await _js.InvokeVoidAsync("logToConsole", "service security"+ userId);
                // Hash the request content (if any)
                if (request.Content != null)
                {
                    var rawContent = await request.Content.ReadAsStringAsync();
                    var contentBytes = Encoding.UTF8.GetBytes(rawContent);
                    using var md5 = MD5.Create();
                    var contentHashedByteArray = md5.ComputeHash(contentBytes);
                    hashedContent = Convert.ToBase64String(contentHashedByteArray);
                }

                // Generate token
                var hmacToken = GenerateHmacToken(nonce, timeStamp, hashedContent, deviceType, appVersion, userId);
                _logger.LogInformation("hmacToken for hmacToken={hmacToken}", hmacToken);
                // Add authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("amx", hmacToken);

                // Proceed with request
                return await base.SendAsync(request, cancellationToken);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }
        private string GenerateHmacToken(string nonce, string timeStamp, string content, string deviceType, string appVersion, string userId)
        {
            var _key = _config["PrivateKey"];

            var rawDataForToken = string.Format("{0}{1}{2}{3}{4}{5}", nonce, timeStamp, content, deviceType, /*applicationType,*/ appVersion, userId);
            var rawDataForTokenByteArray = Encoding.UTF8.GetBytes(rawDataForToken);

            var keyByteArray = Convert.FromBase64String(_key);

            var hmac = new HMACSHA256(keyByteArray);
            var hmacTokenByteArray = hmac.ComputeHash(rawDataForTokenByteArray);

            var hmacTokenString = Convert.ToBase64String(hmacTokenByteArray);
            var hmacToken = string.Join(":", new string[] { hmacTokenString, nonce, timeStamp, deviceType, /*applicationType,*/ appVersion, userId });

            return hmacToken;
        }
    }
}
