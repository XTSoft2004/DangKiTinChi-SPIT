using Domain.Common.Http;
using Domain.Model.Request.Account;
using Domain.Model.Response.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAccountServices
    {
        /// <summary>
        /// Tạo tài khoản mới của user đang đăng nhập
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(AccountRequest accountRequest);
        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="accountUpdateRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? AccountId, AccountUpdateRequest accountUpdateRequest);
        /// <summary>
        /// Xoá tài khoản theo Id
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? AccountId);
        /// <summary>
        /// Lấy danh sách tài khoản của người dùng đang đăng nhập
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<AccountResponse>?, int TotalRecords)> GetAccounts(string search, int pageNumber, int pageSize);
        /// <summary>
        /// Login tài khoản vào hệ thống trường học
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Task<HttpResponse> LoginAccount(long? AccountId);
    }
}
