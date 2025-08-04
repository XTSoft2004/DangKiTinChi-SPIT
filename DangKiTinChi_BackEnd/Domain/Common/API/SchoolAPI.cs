using Domain.Base.Services;
using Domain.Common.HttpRequest;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Domain.Model.Response.User;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Domain.Common.API
{
    public class SchoolAPI : BaseService, ISchoolAPI
    {
        private readonly IHttpRequestHelper _request;
        private readonly IRepositoryBase<Account> _account;
        private Account accountMe;

        public Dictionary<string, string> headersDefault = new Dictionary<string, string>()
        {
            {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"},
            {"Accept-Language","vi-VN,vi;q=0.9"},
            {"Priority","u=0, i"},
            {"Sec-Ch-Ua","\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Google Chrome\";v=\"126\""},
            {"Sec-Ch-Ua-Mobile","?0"},
            {"Sec-Ch-Ua-Platform","\"Windows\""},
            {"Sec-Fetch-Dest","document"},
            {"Sec-Fetch-Mode","navigate"},
            {"Sec-Fetch-Site","none"},
            {"Sec-Fetch-User","?1"},
            {"Upgrade-Insecure-Requests","1"},
            {"User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36"},
        };

        public SchoolAPI(IHttpRequestHelper request, IRepositoryBase<Account> account)
        {
            _request = request;
            _account = account;
        }
        public async Task SetAccount(long? AccountId)
        {
            accountMe = await _account.FindAsync(f => f.Id == AccountId);
            _request.SetAccount(accountMe);
        }
        public async Task<string> RefreshTokenLogin(string url)
        {
            _request.Client.SetHeader(headersDefault);
            var responseMessageHome = await _request.Client.GetAsync(url);
            if (responseMessageHome.IsSuccessStatusCode)
            {
                string token = Regex.Match(_request.Client.Content, "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"(.*?)\"").Groups[1].Value;
                accountMe.__RequestVerificationToken = token;
                _account.Update(accountMe);
                await UnitOfWork.CommitAsync();
                return token;
            }
            return string.Empty;
        }

        public async Task<bool> LoginAccount(long? AccountId)
        {
            await SetAccount(AccountId);

            if(await GetNameStudent())
                return true;

            if (string.IsNullOrEmpty(await RefreshTokenLogin($"https://{accountMe.DomainSchool}/Account/Login")))
                return false;

            Dictionary<string, string> paramData = new()
            {
                {"__RequestVerificationToken", accountMe?.__RequestVerificationToken},
                {"loginID", accountMe?.UserName},
                {"password", accountMe?.Password},
            };

            var responseMessage = await _request.Client.PostAsync($"https://{accountMe.DomainSchool}/Account/Login", paramData);
            if (responseMessage.IsSuccessStatusCode && !_request.Client.Content.Contains("Đăng nhập hệ thống"))
            {
                var response = _request.Client.Content;
                accountMe.Cookie = _request.Client.GetCookies($"https://{accountMe.DomainSchool}");
                _account.Update(accountMe);
                await UnitOfWork.CommitAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> GetNameStudent()
        {
            var response = await _request.Client.GetAsync($"https://{accountMe.DomainSchool}/Account/UserProfile");
            if (response.IsSuccessStatusCode && !String.IsNullOrEmpty(_request.Client.Content))
            {
                string content = _request.Client.Content;
                content = HtmlEntity.DeEntitize(_request.Client.Content);

                // Load the HTML content into an HTML document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                string fullname = doc.DocumentNode.SelectSingleNode("//div[@class=\"hitec-information\"]//h5")?.InnerText;
                string semeterName = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[3]/div/a/text()[4]")?.InnerText;
                accountMe.FullName = fullname.Trim();
                accountMe.SemeterName = semeterName.Trim();
                accountMe.Cookie = _request.Client.GetCookies($"https://{accountMe.DomainSchool}");
                _account.Update(accountMe);
                await UnitOfWork.CommitAsync();
                return true;    
            }
            return false;
        }
    }
}
