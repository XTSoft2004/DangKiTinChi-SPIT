using Domain.Base.Services;
using Domain.Common;
using Domain.Common.API;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Domain.Model.Response.Account;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AccountServices : BaseService, IAccountServices
    {
        private readonly IRepositoryBase<Account>? _account;
        private readonly IRepositoryBase<User>? _user;
        private readonly ITokenServices _tokenServices;
        private readonly ISchoolAPI _schoolAPI;
        private UserTokenResponse? userMeToken;
        public AccountServices(IRepositoryBase<Account>? account, ITokenServices tokenServices, ISchoolAPI schoolAPI, IRepositoryBase<User>? user)
        {
            _account = account;
            _user = user;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
            _schoolAPI = schoolAPI;
        }

        public async Task<HttpResponse> CreateAsync(AccountRequest accountRequest)
        {
            var existingAccount = await _account!.FindAsync(f => f.UserName == accountRequest.Username && f.UserId == userMeToken.Id);
            if (existingAccount != null)
                return HttpResponse.Error("Tài khoản đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var user = await _user.FindAsync(f => f.Id == userMeToken.Id);
            if(user == null)
                return HttpResponse.Error("Người dùng không tồn tại!", HttpStatusCode.NotFound);

            var newAccount = new Account
            {
                UserName = accountRequest.Username,
                Password = accountRequest.Password,
                DomainSchool = accountRequest.DomainSchool,
                User = user,
                UserId = user?.Id,
                CreatedDate = DateTime.Now
            };

            _account.Insert(newAccount);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Tạo tài khoản thành công!", statusCode: HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long? AccountId, AccountUpdateRequest accountUpdateRequest)
        {
            var existingAccount = await _account!.FindAsync(f => f.Id == AccountId && f.UserId == userMeToken.Id);
            if(existingAccount == null)
                return HttpResponse.Error("Tài khoản không tồn tại!", HttpStatusCode.NotFound);

            existingAccount.UserName = accountUpdateRequest.Username ?? existingAccount.UserName;
            existingAccount.Password = accountUpdateRequest.Password ?? existingAccount.Password;
            existingAccount.FullName = accountUpdateRequest.FullName ?? existingAccount.FullName;
            existingAccount.SemeterName = accountUpdateRequest.SemeterName ?? existingAccount.SemeterName;
            existingAccount.Cookie = accountUpdateRequest.Cookie ?? existingAccount.Cookie;
            existingAccount.__RequestVerificationToken = accountUpdateRequest.__RequestVerificationToken ?? existingAccount.__RequestVerificationToken;
            existingAccount.DomainSchool = accountUpdateRequest.DomainSchool ?? existingAccount.DomainSchool;

            _account.Update(existingAccount);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Cập nhật tài khoản thành công!", statusCode: HttpStatusCode.OK);
        }
        public async Task<HttpResponse> DeleteAsync(long? AccountId)
        {
            var existingAccount = await _account!.FindAsync(f => f.Id == AccountId && f.UserId == userMeToken.Id);
            if(existingAccount == null)
                return HttpResponse.Error("Tài khoản không tồn tại!", HttpStatusCode.NotFound);
            _account.Delete(existingAccount);

            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Xoá tài khoản thành công!", statusCode: HttpStatusCode.OK);
        }
        public async Task<(List<AccountResponse>?, int TotalRecords)> GetAccounts(string search, int pageNumber, int pageSize)
        {
            var query = _account!.All()
                .Include(d => d.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string searchLower = search.ToLower();

                query = query.Where(d =>
                    (!string.IsNullOrEmpty(d.FullName) && d.FullName.ToLower().Contains(searchLower)) ||
                    (!string.IsNullOrEmpty(d.SemeterName) && d.SemeterName.ToLower().Contains(searchLower)) ||
                    (!string.IsNullOrEmpty(d.UserName) && d.UserName.ToLower().Contains(searchLower))
                );
            }

            int TotalRecords = await query.CountAsync();

            if (pageNumber != -1 && pageSize != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var accountSearch = await query.Select(d => new AccountResponse
            {
                Id = d.Id,
                Username = d.UserName,
                FullName = d.FullName,
                SemeterName = d.SemeterName,
                Cookie = d.Cookie,
                __RequestVerificationToken = d.__RequestVerificationToken,
                DomainSchool = d.DomainSchool
            }).ToListAsync();

            return (accountSearch, TotalRecords);
        }
        public async Task<HttpResponse> LoginAccount(long? AccountId)
        {
            var account = _account.Find(f => f.Id == AccountId && f.UserId == userMeToken.Id);
            if (account == null)
                return HttpResponse.Error("Tài khoản không tồn tại trong hệ thống!", HttpStatusCode.NotFound);

            var loginAccount = await _schoolAPI.LoginAccount(AccountId);
            return loginAccount
                ? HttpResponse.OK(message: "Đăng nhập tài khoản thành công!")
                : HttpResponse.Error("Đăng nhập tài khoản thất bại, có thể do hệ thống đang quá tải!");
        }
    }
}
