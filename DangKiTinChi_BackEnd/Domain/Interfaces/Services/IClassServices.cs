using Domain.Common.Http;
using Domain.Model.Request.Class;
using Domain.Model.Response.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClassServices
    {
        /// <summary>
        /// Tạo lớp học mới
        /// </summary>
        /// <param name="classRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(ClassRequest classRequest);
        /// <summary>
        /// Cập nhật thông tin lớp học
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="classUpdateRequest"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? classId, ClassUpdateRequest classUpdateRequest);
        /// <summary>
        /// Xoá lớp học theo Id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? classId);
        /// <summary>
        /// Lấy thông tin lớp học
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<ClassResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
    }
}
