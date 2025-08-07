using Domain.Base.Services;
using Domain.Common.HttpRequest;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Domain.Model.Request.Class;
using Domain.Model.Request.Course;
using Domain.Model.Request.Time;
using Domain.Model.Response.Class;
using Domain.Model.Response.Course;
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
                // Lấy các thông tin cá nhân
                await GetNameStudent();
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
                if (string.IsNullOrEmpty(fullname))
                    return false;

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
        public async Task<CourseResponse> GetInfoCourse(string courseCode)
        {
            var response = await _request.Client.GetAsync($"https://{accountMe.DomainSchool}/Studying/Courses/{courseCode}/");
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
            var response = await _request.Client.GetAsync($"https://{accountMe.DomainSchool}/Course/Details/{classCode}/");
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
