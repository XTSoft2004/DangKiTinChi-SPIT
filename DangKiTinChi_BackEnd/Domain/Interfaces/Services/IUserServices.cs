using Domain.Common.Http;
using Domain.Model.Request.Auth;
using Domain.Model.Request.User;
using Domain.Model.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Lấy thông tin người dùng hiện tại
        /// </summary>
        /// <returns></returns>
        Task<HttpResponse> GetMe();
        /// <summary>
        /// Tạo mới tài khoản người dùng
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(RegisterRequest userRequest);
        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="userUpdateRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? UserId, UserUpdateRequest userUpdateRequest);
        /// <summary>
        ///  Xoá thông tin người dùng
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? UserId);
        /// <summary>
        /// Lấy thông tin người dùng
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        Task<(List<UserResponse>?, int TotalRecords)> GetUsers(string search, int pageNumber, int pageSize);
    }
}
