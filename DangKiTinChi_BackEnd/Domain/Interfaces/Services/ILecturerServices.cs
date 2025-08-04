using Domain.Common.Http;
using Domain.Model.Request.Lecturer;
using Domain.Model.Response.Lecturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ILecturerServices
    {
        /// <summary>
        /// Tạo mới giảng viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(LecturerRequest FormData);
        /// <summary>
        /// Cập nhật thông tin giảng viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long id, LecturerRequest FormData);
        /// <summary>
        /// Xoá giảng viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long id);
        /// <summary>
        /// Lấy danh sách giảng viên với phân trang và tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<LecturerResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);

    }
}
