using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Response.User;
using HelperHttpClient;
using ModelsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.HttpRequest
{
    public class HttpRequestHelper : IHttpRequestHelper
    {
        public RequestHttpClient _request { get; private set; }
        private readonly ITokenServices _tokenServices;
        private readonly IRepositoryBase<Account> _account;
        private readonly IRepositoryBase<AccountClasses> _accountClasses;
        private Account accountMe;
        public HttpRequestHelper(ITokenServices tokenServices, IRepositoryBase<Account> account, IRepositoryBase<AccountClasses> accountClasses)
        {
            _request = new RequestHttpClient();
            _tokenServices = tokenServices;
            _account = account;
            _accountClasses = accountClasses;
            // Load .env
            var manualPath = Environment.GetEnvironmentVariable("DOTNET_ENV_PATH");
            if (!string.IsNullOrEmpty(manualPath) && File.Exists(manualPath))
            {
                DotNetEnv.Env.Load(manualPath);
            }
            else
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var parent = Directory.GetParent(baseDir);
                for (int i = 0; i < 4 && parent != null; i++)
                {
                    parent = parent.Parent;
                }
                var envPath = parent != null
                    ? Path.Combine(parent.FullName, ".env")
                    : ".env";
                DotNetEnv.Env.Load(envPath);
            }


            //// Setup request
            //if (!string.IsNullOrEmpty(accountMe.DomainSchool))
            //    _request.SetAddress($"https://{accountMe.DomainSchool}/");

            //if (!string.IsNullOrEmpty(accountMe.Cookie))
            //    _request.SetCookie(accountMe.Cookie, "/", accountMe.DomainSchool ?? string.Empty);
        }
        public void SetAccount(Account account)
        {
            accountMe = account;
            // Setup request
            if (!string.IsNullOrEmpty(accountMe.DomainSchool))
                _request.SetAddress($"https://{accountMe.DomainSchool}/");

            if (!string.IsNullOrEmpty(accountMe.Cookie))
                _request.SetCookie(accountMe.Cookie, "/", accountMe.DomainSchool ?? string.Empty);

            var accountClass = _accountClasses.Find(f => f.AccountId == accountMe.UserId);
            if (accountClass != null && accountClass.InfoProxy != null && !string.IsNullOrEmpty(accountClass.InfoProxy.Proxy))
                _request.SetProxy(new ProxyModel(accountClass.InfoProxy.Proxy, accountClass.InfoProxy.TypeProxy));
        }

        public string GetResponse(HttpResponseMessage httpResponseMessage)
        {
            return RequestHttpClient.GetTextContent(httpResponseMessage).GetAwaiter().GetResult();
        }

        public RequestHttpClient Client => _request;
    }
}
