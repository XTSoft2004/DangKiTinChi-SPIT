using Domain.Base.Services;
using Domain.Common;
using Domain.Common.BackgroudServices;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Database;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request;
using Domain.Model.Request.Auth;
using Domain.Model.Request.TokenUser;
using Domain.Model.Response.Auth;
using Domain.Model.Response.Token;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AuthServices : BaseService, IAuthServices
    {
        private readonly IRepositoryBase<User> _user;
        private readonly IRepositoryBase<TokenUser> _token;
        private readonly IUserServices _userServices;
        private readonly ITokenServices _tokenServices;

        private UserTokenResponse? userMeToken;
        public AuthServices(IRepositoryBase<User> user, IUserServices userServices, ITokenServices tokenServices, IRepositoryBase<TokenUser> token)
        {
            _user = user;
            _userServices = userServices;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
            _token = token;
        }

        public async Task<HttpResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = await _user.FindAsync(f => f.UserName == registerRequest.UserName);
            if (user != null)
                return HttpResponse.Error("Tài khoản đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var newUser = await _userServices.CreateAsync(registerRequest);
            return newUser;
        }
        public async Task<HttpResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _user.FindAsync(f => f.UserName == loginRequest.UserName.Trim() && f.Password == loginRequest.Password.Trim());
            if (user == null)
                return HttpResponse.Error("Tài khoản hoặc mật khẩu không đúng!", System.Net.HttpStatusCode.Unauthorized);

            #region Xử lý token của người dùng
            // Tạo JWT token và Refresh Token cho người dùng
            TokenResponse tokenResponse = _tokenServices.GenerateToken(new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                RoleName = AppFunction.GetRoleName(user.RoleId)?.DisplayName,
            }, loginRequest.DeviceId!);

            // Nếu người dùng mới thì tạo mới Refresh Token, ngược lại thì cập nhật Refresh Token
            await _tokenServices.UpdateRefreshToken(new TokenRequest()
            {
                UserId = user.Id,
                Token = tokenResponse.RefreshToken,
                ExpiryDate = tokenResponse.RefreshExpiresAt,
                DeviceId = loginRequest.DeviceId!
            });
            #endregion

            var loginResponse = new LoginResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                RoleName = AppFunction.GetRoleName(user.RoleId)?.DisplayName,
                AvatarUrl = user.AvatarUrl,
                isLocked = user.IsLocker,
                AccessToken = tokenResponse.AccessToken,
                ExpiresAt = tokenResponse.ExpiresAt,
                RefreshToken = tokenResponse.RefreshToken,
                RefreshExpiresAt = tokenResponse.RefreshExpiresAt
            };

            return HttpResponse.OK(message: "Đăng nhập thành công!", data: loginResponse);
        }
        public async Task<HttpResponse> LogoutUser()
        {
            var user = await _user!.FindAsync(f => f.Id == userMeToken!.Id);
            if (user == null)
                return HttpResponse.OK(message: "Người dùng không tồn tại.");
            var tokenAll = await _token.FindAsync(f => f.UserId == userMeToken!.Id && f.DeviceId == userMeToken!.DeviceId);
            // Xóa token đăng nhập của người dùng theo deviceId
            if (tokenAll == null)
                return HttpResponse.OK(message: "Người dùng không có token đăng nhập.");

            _token.Delete(tokenAll);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Đăng xuất thành công.");
        }
        public async Task<HttpResponse> RefreshTokenAccount()
        {
            if (userMeToken == null)
                return HttpResponse.Error(message: "Không tìm thấy thông tin người dùng.", HttpStatusCode.Unauthorized);

            var user = await _user!.FindAsync(f => f.Id == userMeToken.Id);
            // Tạo JWT token và Refresh Token cho người dùng
            TokenResponse tokenResponse = _tokenServices.GenerateToken(new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                RoleName = AppFunction.GetRoleName(user.RoleId)?.DisplayName,
            }, userMeToken.DeviceId!);

            // Nếu người dùng mới thì tạo mới Refresh Token, ngược lại thì cập nhật Refresh Token
            await _tokenServices.UpdateRefreshToken(new TokenRequest()
            {
                UserId = user.Id,
                Token = tokenResponse.RefreshToken,
                ExpiryDate = tokenResponse.RefreshExpiresAt,
                DeviceId = userMeToken.DeviceId!
            });

            return HttpResponse.OK(message: "Làm mới token thành công.", data: new TokenInfoResponse()
            {
                UserId = user.Id,
                AccessToken = tokenResponse.AccessToken,
                ExpiresAt = tokenResponse.ExpiresAt,
                RefreshToken = tokenResponse.RefreshToken,
                RefreshExpiresAt = tokenResponse.RefreshExpiresAt,
                DeviceId = userMeToken?.DeviceId!,
                RoleName = user.Role?.DisplayName ?? string.Empty
            });
        }

    }
}
