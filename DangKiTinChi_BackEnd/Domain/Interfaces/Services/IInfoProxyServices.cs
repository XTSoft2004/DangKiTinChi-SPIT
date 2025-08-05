using Domain.Common.Http;
using Domain.Model.Request.InfoProxy;
using Domain.Model.Response.InfoProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IInfoProxyServices
    {
        /// <summary>
        /// Tạo 1 proxy mới
        /// </summary>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(InfoProxyRequest FormData);
        /// <summary>
        /// Cập nhật thông tin proxy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? id, InfoProxyRequest FormData);
        /// <summary>
        /// Xoá 1 proxy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? id);
        /// <summary>
        /// Lấy tất cả proxy theo phân trang và tìm kiếm
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<InfoProxyResponse>, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
    }
}
