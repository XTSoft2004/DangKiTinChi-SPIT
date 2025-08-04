using Domain.Model.Request.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.API
{
    public interface ISchoolAPI
    {
        /// <summary>
        /// Lấy token đăng nhập từ trang web trường học
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> RefreshTokenLogin(string url);
        /// <summary>
        /// Đăng nhập tài khoản vào hệ thống trường học
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Task<bool> LoginAccount(long? AccountId);
    }
}
