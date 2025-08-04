using Domain.Base.Services;
using Domain.Common;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Database;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Auth;
using Domain.Model.Request.User;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserServices : BaseService, IUserServices
    {
        private readonly IRepositoryBase<User>? _user;
        private readonly IRepositoryBase<Role>? _role;

        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        public UserServices(IRepositoryBase<User>? user, IRepositoryBase<Role>? role, ITokenServices tokenServices)
        {
            _user = user;
            _role = role;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> GetMe()
        {
            long? userIdToken = userMeToken?.Id;
            if (userIdToken == null)
                return HttpResponse.Error("Người dùng không hợp lệ", HttpStatusCode.Unauthorized);

            // Specify parameter to resolve ambiguity
            var user = await _user!.FindAsync(f => f.Id == userIdToken);
            if (user == null)
                return HttpResponse.Error("Người dùng không tồn tại!", HttpStatusCode.NotFound);

            return HttpResponse.OK(new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Money = user.Money,
                RoleName = AppFunction.GetRoleName(user.RoleId)?.DisplayName,
            }, "Lấy thông tin người dùng thành công!");
        }
        public async Task<HttpResponse> CreateAsync(RegisterRequest userRequest)
        {
            var user = await _user.FindAsync(f => f.UserName == userRequest.UserName);
            if(user != null)
                return HttpResponse.Error("Tài khoản đã tồn tại!", HttpStatusCode.NotFound);

            var role = await _role!.FindAsync(f => f.DisplayName == "User");

            var newUser = new User()
            {
                UserName = userRequest.UserName,
                Password = userRequest.Password,
                FullName = userRequest.FullName,
                Money = 0,
                Role = role,
                RoleId = role?.Id
            };
            _user.Insert(newUser);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Tạo tài khoản thành công!", statusCode: HttpStatusCode.Created);
        }

        public async Task<HttpResponse> UpdateAsync(long? UserId, UserUpdateRequest userUpdateRequest)
        {
            var user =  await _user.FindAsync(f => f.Id == UserId);
            if (user == null)
                return HttpResponse.Error("Tài khoản không tồn tại!", HttpStatusCode.NotFound);

            user.Password = userUpdateRequest.Password ?? user.Password;
            user.FullName = userUpdateRequest.FullName ?? user.FullName;
            _user.Update(user);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật tài khoản thành công!");
        }

        public async Task<HttpResponse> DeleteAsync(long? UserId)
        {
            var user = await _user.FindAsync(f => f.Id == UserId);
            if (user == null)
                return HttpResponse.Error("Tài khoản không tồn tại!", HttpStatusCode.NotFound);
            _user.Delete(user);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Xoá người dùng thành công.");
        }

        public async Task<(List<UserResponse>?, int TotalRecords)> GetUsers(string search, int pageNumber, int pageSize)
        {
            var query = _user!.All()
                .Include(d => d.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string searchLower = search.ToLower();

                query = query.Where(d =>
                    (!string.IsNullOrEmpty(d.FullName) && d.FullName.ToLower().Contains(searchLower)) ||
                    (!string.IsNullOrEmpty(d.UserName) && d.UserName.ToLower().Contains(searchLower)) ||
                    (!string.IsNullOrEmpty(d.Role.DisplayName) && d.Role.DisplayName.ToLower().Contains(searchLower))
                );
            }

            int TotalRecords = await query.CountAsync();

            if (pageNumber != -1 && pageSize != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var userSearch = await query.Select(d => new UserResponse
            {
                Id = d.Id,
                UserName = d.UserName,
                FullName = d.FullName,
                Money = d.Money,
                RoleName = d.Role.DisplayName
            }).ToListAsync();

            return (userSearch, TotalRecords);
        }
    }
}
