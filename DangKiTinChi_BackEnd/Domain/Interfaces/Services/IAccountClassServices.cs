using Domain.Common.Http;
using Domain.Model.Request.AccountClass;
using Domain.Model.Response.AccountClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAccountClassServices
    {
        /// <summary>
        /// Tạo 1 tài khoản lớp học mới
        /// </summary>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(AccountClassRequest FormData);
        /// <summary>
        /// Cập nhật thông tin tài khoản lớp học
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? id, AccountClassRequest FormData);
        /// <summary>
        /// Xoá 1 tài khoản lớp học
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? id);
        /// <summary>
        /// Lấy danh sách tất cả tài khoản lớp học theo phân trang và tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<AccountClassResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
    }
}
