using HelperHttpClient.Models;
using ModelsHelper;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HelperHttpClient.Interfaces
{
    public interface IRequestHttpClient
    {
        // Properties
        HttpResponseMessage Response { get; set; }
        string Content { get; set; }

        // Configuration Methods
        void SetTimeout(TimeSpan timeout);
        void SetAddress(string address);
        void SetCancellationToken(CancellationToken cancellationToken);

        // Cookie Management
        void SetCookie(string cookie, string path, string domain);
        void SetCookie(CookieModel? cookieModel);
        void ClearCookie();
        Task<List<Cookie>> ListGetCookies(string address);
        string GetCookies(string address);

        // Header Management
        void SetHeader(string key, string value);
        void SetHeader(Dictionary<string, string> headers, bool isClear = false);

        // Proxy Management
        void SetProxy(ProxyModel proxy);
        void SetProxy(string? ip, string? port, string? username, string? password);

        // Authentication
        void SetAuthentication(string access_token);

        // HTTP Methods
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, string json);
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> dataPost);
        Task<HttpResponseMessage> PostAsync(string url, MultipartFormData multipartForm);
        Task<HttpResponseMessage> PostAsync(string url, MultipartFormDataContent multipartFormData);
        Task<HttpResponseMessage> PostAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, dynamic? DataPost);
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<HttpResponseMessage> PatchAsync(string url, dynamic? DataPost);

        // Utility Methods
        string Address();
    }
}
