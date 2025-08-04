using Domain.Common.Http;
using Domain.Model.Request.Department;
using Domain.Model.Response.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IDepartmentServices
    {
        /// <summary>
        /// Tạo mới 1 khoa
        /// </summary>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(DepartmentRequest FormData);
        /// <summary>
        /// Cập nhật thông tin khoa
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? DepartmentId, DepartmentRequest FormData);
        /// <summary>
        /// Xoá khoa qua id khoa
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? DepartmentId);
        /// <summary>
        /// Lấy danh sách khoa với phân trang và tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<DepartmentResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
    }
}
