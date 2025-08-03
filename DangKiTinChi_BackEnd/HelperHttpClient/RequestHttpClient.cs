using HelperHttpClient.Interfaces;
using HelperHttpClient.Models;
using ModelsHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelperHttpClient
{
    public partial class RequestHttpClient : IRequestHttpClient
    {
        private CancellationToken _cancellationToken;
        public HttpClient _client = new HttpClient();
        private HttpClientHandler _handler = new HttpClientHandler();
        private CookieContainer _cookieContainer = new CookieContainer();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private TimeSpan? _timeout = new TimeSpan();

        public HttpResponseMessage Response
        {
            get => _response;
            set => _response = value;
        }

        public string Content { get; set; } = string.Empty;

        // Constructors
        public RequestHttpClient()
        {
            Initialize();
        }

        public RequestHttpClient(TimeSpan timeout = default(TimeSpan))
        {
            Initialize();
            if (timeout != default(TimeSpan))
            {
                SetTimeout(timeout);
            }
        }

        public RequestHttpClient(string cookie, string path, string domain, Dictionary<string, string> headers, TimeSpan timeout = default(TimeSpan))
        {
            Initialize();
            SetCookie(cookie, path, domain);
            SetHeader(headers);
            if (timeout != default(TimeSpan))
            {
                SetTimeout(timeout);
            }
        }

        public RequestHttpClient(string? cookie = null, string? path = null, string? domain = null, Dictionary<string, string>? headers = null, ProxyModel? _proxy = null, TimeSpan timeout = default(TimeSpan))
        {
            Initialize();
            if (cookie != null && path != null && domain != null)
                SetCookie(cookie, path, domain);
            if (headers != null)
                SetHeader(headers);
            if (_proxy != null)
                SetProxy(_proxy);
            if (timeout != default(TimeSpan))
                SetTimeout(timeout);
        }

        private void Initialize()
        {
            _cookieContainer = new CookieContainer();
            _handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.All,
                CookieContainer = _cookieContainer,
                ServerCertificateCustomValidationCallback = (HttpRequestMessage, cert, chain, sslPolicyErrors) => true
            };

            _client = new HttpClient(_handler);
            _client.BaseAddress = new Uri("https://www.facebook.com/");
        }

        // Configuration Methods
        public void SetTimeout(TimeSpan timeout = default(TimeSpan))
        {
            if (timeout != default(TimeSpan))
            {
                _timeout = timeout;
                _client.Timeout = timeout;
            }
        }

        public void SetAddress(string address)
        {
            _client.BaseAddress = new Uri(address);
        }

        public void SetCancellationToken(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            _cookieContainer = cookieContainer;
        }
    }
}
