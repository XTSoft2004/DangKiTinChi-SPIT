using Domain.Common.Http;
using Domain.Model.Request;
using Domain.Model.Request.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAuthServices
    {
        /// <summary>
        /// Tạo mới tài khoản người dùng
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> RegisterAsync(RegisterRequest registerRequest);
        /// <summary>
        /// Đăng nhâp người dùng
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> LoginAsync(LoginRequest loginRequest);
        /// <summary>
        /// Đăng xuất tài khoản
        /// </summary>
        /// <returns></returns>
        Task<HttpResponse> LogoutUser();
        /// <summary>
        /// Lấy refresh token mới của người dùng
        /// </summary>
        /// <returns></returns>
        Task<HttpResponse> RefreshTokenAccount();
    }
}
