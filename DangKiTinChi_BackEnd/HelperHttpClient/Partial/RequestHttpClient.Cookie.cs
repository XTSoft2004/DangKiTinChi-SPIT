using HelperHttpClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HelperHttpClient
{
    public partial class RequestHttpClient
    {
        // Cookie Management Methods
        public void SetCookie(string cookie, string path, string domain)
        {
            IEnumerable<string[]> list = cookie.Split(';').Select(x => x.Split('=')).Where(x => x.Length == 2);

            foreach (string[] info in list)
            {
                SetCookie(info, path, domain);
            }
        }

        public void SetCookie(string[] cookieInfo, string path, string domain)
        {
            string name = cookieInfo[0].Trim();
            string value = cookieInfo[1].Trim();

            Cookie cookieNew = new Cookie(name, value, path, domain);
            _cookieContainer.Add(cookieNew);
        }

        public void ClearCookie()
        {
            _cookieContainer = new CookieContainer();
        }

        public void SetCookie(CookieModel? cookieModel)
        {
            if (cookieModel == null)
                return;

            IEnumerable<string[]> cookieList = cookieModel.GetCookieList();

            foreach (string[] cookie in cookieList)
            {
                SetCookie(cookie, cookieModel.Path, cookieModel.Domain);
            }
        }

        public async Task<List<Cookie>> ListGetCookies(string address)
        {
            var cookies = new List<Cookie>();

            if (_handler is HttpClientHandler handler)
            {
                var cookieContainer = handler.CookieContainer;
                Uri uri = new Uri(address);
                cookies.AddRange(cookieContainer.GetCookies(uri).Cast<Cookie>());
            }

            return cookies;
        }

        public string GetCookies(string address)
        {
            List<Cookie> cookies = ListGetCookies(address).GetAwaiter().GetResult();
            string cookie = string.Join("; ", cookies.Select(cookie => $"{cookie.Name}={cookie.Value}"));
            return cookie;
        }
    }
}