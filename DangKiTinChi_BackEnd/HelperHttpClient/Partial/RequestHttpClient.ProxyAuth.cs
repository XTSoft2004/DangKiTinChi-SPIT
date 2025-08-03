using ModelsHelper;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HelperHttpClient
{
    public partial class RequestHttpClient
    {
        // Proxy Management Methods
        public void SetProxy(string? ip, string? port, string? username, string? password)
        {
            _handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | System.Net.DecompressionMethods.Brotli,
                CookieContainer = _cookieContainer
            };
            var _client_backup = _client;
            _client = new HttpClient(_handler);

            foreach (var header in _client_backup.DefaultRequestHeaders)
            {
                foreach (var value in header.Value)
                {
                    SetHeader(header.Key, value);
                }
            }

            WebProxy proxy = new WebProxy();

            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(port))
            {
                proxy = new WebProxy
                {
                    Address = new Uri($"http://{ip}:{port}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                };
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                proxy.UseDefaultCredentials = true;
                proxy.Credentials = new NetworkCredential(username, password);
            }

            if (proxy != null)
            {
                _handler.Proxy = proxy;
            }
        }

        public void SetProxy(ProxyModel proxy)
        {
            if (proxy == null || string.IsNullOrEmpty(proxy.IP) || string.IsNullOrEmpty(proxy.Port))
                return;

            SetProxy(proxy.IP, proxy.Port, proxy.Username, proxy.Password);
        }

        // Authentication Methods
        public void SetAuthentication(string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        }
    }
}