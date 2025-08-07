using Domain.Base.Services;
using Domain.Common.HttpRequest;
using Domain.Entities;
using Domain.Interfaces.Common;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Domain.Model.Request.Class;
using Domain.Model.Request.Course;
using Domain.Model.Request.Time;
using Domain.Model.Response.Class;
using Domain.Model.Response.Course;
using Domain.Model.Response.User;
using HelperHttpClient;
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
        private readonly IHttpContextHelper _httpContextHelper;
        private readonly AsyncLocal<Account> accountMe = new();
        private readonly AsyncLocal<string> UrlBase = new();
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

        public SchoolAPI(IHttpRequestHelper request, IRepositoryBase<Account> account, IHttpContextHelper httpContextHelper)
        {
            _request = request;
            _account = account;
            _httpContextHelper = httpContextHelper;
            SetAccountContext();
        }
        public void SetAccountContext()
        {
            var account = _account.Find(f => f.Id == _httpContextHelper.GetAccountId());
            if(account != null)
            {
                accountMe.Value = account;
                UrlBase.Value = AppFunction.GetDomainSchool(account.SchoolEnum);
                _request.SetAccount(account);
            }
        }
        public async Task<bool> LoginAccount(long? AccountId)
        {
            var accountLogin = await _account.FindAsync(f => f.Id == AccountId);
            string UrlBase = AppFunction.GetDomainSchool(accountLogin.SchoolEnum);

            RequestHttpClient _requestLogin = new RequestHttpClient();
            _requestLogin.SetHeader(headersDefault);
            await _requestLogin.GetAsync($"https://{UrlBase}/Account/Login");
            string verificationToken = Regex.Match(_requestLogin.Content, "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"(.*?)\"").Groups[1].Value;
            if (string.IsNullOrEmpty(verificationToken))
                return false;

            Dictionary<string, string> paramData = new()
            {
                {"__RequestVerificationToken", verificationToken},
                {"loginID", accountLogin.UserName},
                {"password", accountLogin.Password},
            };

            var responseMessage = await _requestLogin.PostAsync($"https://{UrlBase}/Account/Login", paramData);
            if (responseMessage.IsSuccessStatusCode && !_requestLogin.Content.Contains("Đăng nhập hệ thống"))
            {
                var response = _requestLogin.Content;
                accountLogin.Cookie = _requestLogin.GetCookies($"https://{UrlBase}");
                _account.Update(accountLogin);
                await UnitOfWork.CommitAsync();
                // Lấy các thông tin cá nhân
                await GetNameStudent(_requestLogin, accountLogin);
                return true;
            }

            return false;
        }
        public async Task<bool> GetNameStudent(RequestHttpClient _requestNew = null, Account account = null)
        {
            RequestHttpClient _requestProfile = null;
            if (_requestNew != null)
                _requestProfile = _requestNew;
            else
                _requestProfile = _request.Client;

            string urlBase = AppFunction.GetDomainSchool(account.SchoolEnum);
            var response = await _requestProfile.GetAsync($"https://{urlBase}/Account/UserProfile");
            if (response.IsSuccessStatusCode && !String.IsNullOrEmpty(_requestProfile.Content))
            {
                string content = _requestProfile.Content;
                content = HtmlEntity.DeEntitize(_requestProfile.Content);

                // Load the HTML content into an HTML document
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                string fullname = doc.DocumentNode.SelectSingleNode("//div[@class=\"hitec-information\"]//h5")?.InnerText;
                if (string.IsNullOrEmpty(fullname))
                    return false;

                string semeterName = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[3]/div/a/text()[4]")?.InnerText;
                account.FullName = fullname.Trim();
                account.SemeterName = semeterName.Trim();
                account.Cookie = _requestProfile.GetCookies($"https://{urlBase}");
                _account.Update(account);
                await UnitOfWork.CommitAsync();
                return true;
            }
            return false;
        }
        public async Task<string> RefreshTokenLogin(string url)
        {
            _request.Client.SetHeader(headersDefault);
            var responseMessageHome = await _request.Client.GetAsync(url);
            if (responseMessageHome.IsSuccessStatusCode)
            {
                string token = Regex.Match(_request.Client.Content, "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"(.*?)\"").Groups[1].Value;
                accountMe.Value.__RequestVerificationToken = token;
                _account.Update(accountMe.Value);
                await UnitOfWork.CommitAsync();
                return token;
            }
            return string.Empty;
        }
        public async Task<CourseResponse> GetInfoCourse(string courseCode)
        {
            var response = await _request.Client.GetAsync($"https://{UrlBase.Value}/Studying/Courses/{courseCode}/");
            if (response.IsSuccessStatusCode)
            {
                string content = _request.Client.Content;
                content = HtmlEntity.DeEntitize(_request.Client.Content);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                string nameCourse = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div/div/div/div[3]/div[1]/div/p")?.InnerText;
                string codeCourse = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div/div/div/div[3]/div[2]/div[1]/p")?.InnerText;
                string credit = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div/div/div/div[3]/div[2]/div[2]/p")?.InnerText;

                CourseResponse courseRequest = new CourseResponse()
                {
                    Name = nameCourse?.Trim(),
                    Code = codeCourse?.Trim(),
                    Credit = Convert.ToInt32(credit)
                };

                return courseRequest;
            }

            return null;
        }
        public async Task<ClassCheckResponse> GetInfoClass(string classCode)
        {
            var response = await _request.Client.GetAsync($"https://{UrlBase.Value}/Course/Details/{classCode}/");
            if (response.IsSuccessStatusCode)
            {
                string content = _request.Client.Content;
                content = HtmlEntity.DeEntitize(_request.Client.Content);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                string nameClass = doc.DocumentNode.SelectSingleNode("//*[@id=\"courseInformation\"]/fieldset[1]/div[1]/div/p")?.InnerText;
                string nameLecturer = doc.DocumentNode.SelectSingleNode("//*[@id=\"courseInformation\"]/fieldset[1]/div[3]/div/p")?.InnerText;
                string thoikhoabieu = doc.DocumentNode.SelectSingleNode("//*[@id=\"courseInformation\"]/fieldset[2]/div[3]/div/p")?.InnerText;
                string maxStudent = doc.DocumentNode.SelectSingleNode("//*[@id=\"courseInformation\"]/fieldset[2]/div[5]/div/p/span[2]")?.InnerText;
                string courseName = nameClass?.Split('-')?[0]?.Trim();
                if(string.IsNullOrEmpty(nameClass) || 
                    string.IsNullOrEmpty(nameLecturer) || 
                    string.IsNullOrEmpty(thoikhoabieu) ||
                    string.IsNullOrEmpty(maxStudent) ||
                    string.IsNullOrEmpty(courseName))
                {
                    return null;
                }
                ClassCheckResponse classCheckResponse = new ClassCheckResponse()
                {
                    Code = classCode,
                    CourseName = courseName,
                    Name = nameClass,
                    MaxStudent = Convert.ToInt32(maxStudent.Replace("Tối đa: ", "")),
                    TimesRequest = AppFunction.ConvertTextToTimeClass(thoikhoabieu),
                };
                return classCheckResponse;
            }

            return null;
        }
    }
}
