using Domain.Base.Services;
using Domain.Common;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.AccountClass;
using Domain.Model.Response.AccountClass;
using Domain.Model.Response.Class;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AccountClassServices : BaseService, IAccountClassServices
    {
        private readonly IRepositoryBase<AccountClasses> _accountClasses;
        private readonly IRepositoryBase<InfoProxy> _infoProxy;
        private readonly IRepositoryBase<Account> _account;
        private readonly IRepositoryBase<Classes> _classes;
        private ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        public AccountClassServices(IRepositoryBase<AccountClasses> accountClasses, IRepositoryBase<InfoProxy> infoProxy, IRepositoryBase<Account> account, IRepositoryBase<Classes> classes, ITokenServices tokenServices)
        {
            _accountClasses = accountClasses;
            _infoProxy = infoProxy;
            _account = account;
            _classes = classes;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> CreateAsync(AccountClassRequest FormData)
        {
            var account = _account.Find(x => x.Id == FormData.AccountId);
            var classes = _classes.Find(x => x.Id == FormData.ClassesId);
            var infoProxy = _infoProxy.Find(x => x.Id == FormData.InfoProxyId);

            if (account == null)
                return HttpResponse.Error(message: "Tài khoản không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            if (classes == null)
                return HttpResponse.Error(message: "Lớp học không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            if (infoProxy == null)
                return HttpResponse.Error(message: "Proxy không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            var accountClass = new AccountClasses()
            {
                AccountId = FormData.AccountId,
                ClassesId = FormData.ClassesId,
                StatusRegister = Register_Enum.NotRegistered,
                CountFailed = FormData.CountFailed,
                __RequestVerificationToken = FormData.__RequestVerificationToken,
                Status = FormData.Status,
                InfoProxyId = FormData.InfoProxyId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userMeToken?.Username
            };

            _accountClasses.Insert(accountClass);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Đăng ký lớp học thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponse> UpdateAsync(long? id, AccountClassRequest FormData)
        {
            var accountClass = _accountClasses.Find(x => x.Id == id);

            if (accountClass == null)
                return HttpResponse.Error(message: "Đăng ký lớp học không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            accountClass.AccountId = FormData.AccountId;
            accountClass.ClassesId = FormData.ClassesId;
            accountClass.StatusRegister = (Register_Enum)EnumExtensions.GetEnumValueFromDisplayName<Register_Enum>(FormData.StatusRegister);
            accountClass.CountFailed = FormData.CountFailed;
            accountClass.__RequestVerificationToken = FormData.__RequestVerificationToken;
            accountClass.Status = FormData.Status;
            accountClass.InfoProxyId = FormData.InfoProxyId;
            accountClass.ModifiedDate = DateTime.UtcNow;
            accountClass.ModifiedBy = userMeToken?.Username;

            _accountClasses.Update(accountClass);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Cập nhật đăng ký lớp học thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponse> DeleteAsync(long? id)
        {
            var accountClass = _accountClasses.Find(x => x.Id == id);

            if (accountClass == null)
                return HttpResponse.Error(message: "Đăng ký lớp học không tồn tại", statusCode: System.Net.HttpStatusCode.NotFound);

            _accountClasses.Delete(accountClass);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Xóa đăng ký lớp học thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<(List<AccountClassResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _accountClasses.All()
                .Include(search => search.InfoProxy)
                .Include(search => search.Account)
                .Include(search => search.Classes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(x => x.AccountId.ToString().Contains(search) ||
                                    x.ClassesId.ToString().Contains(search) ||
                                    x.InfoProxyId.ToString().Contains(search) ||
                                    x.CountFailed.ToString().Contains(search) ||
                                    x.StatusRegister.GetEnumDisplayName().ToLower().Contains(search) ||
                                    x.Status.ToLower().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var courseSearch = await query.Select(x => new AccountClassResponse
            {
                Id = x.Id,
                AccountId = x.AccountId,
                ClassesId = x.ClassesId,
                StatusRegister = x.StatusRegister.GetEnumDisplayName(),
                CountFailed = x.CountFailed,
                __RequestVerificationToken = x.__RequestVerificationToken,
                Status = x.Status,
                InfoProxyId = x.InfoProxyId,
            }).ToListAsync();

            return (courseSearch, TotalRecords);
        }
    }
}
