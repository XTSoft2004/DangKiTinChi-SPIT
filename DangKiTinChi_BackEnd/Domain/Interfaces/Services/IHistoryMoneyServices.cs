using Domain.Common.Http;
using Domain.Entities;
using Domain.Model.Request.HistoryMoney;
using Domain.Model.Response.HistoryMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IHistoryMoneyServices
    {
        /// <summary>
        /// Tạo mới lịch sử thay đổi tiền
        /// </summary>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> CreateAsync(HistoryMoneyRequest FormData);
        /// <summary>
        /// Cập nhật lịch sử thay đổi tiền
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FormData"></param>
        /// <returns></returns>
        Task<HttpResponse> UpdateAsync(long? id, HistoryMoneyRequest FormData);
        /// <summary>
        /// Xoá lịch sử thay đổi tiền
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(long? id);
        /// <summary>
        /// Lấy danh sách lịch sử thay đổi tiền theo phân trang
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<HistoryMoneyResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
        /// <summary>
        /// Lấy lịch sử thay đổi tiền của người dùng hiện tại theo phân trang
        /// </summary>
        /// <returns></returns>
        Task<(List<HistoryMoneyResponse>?, int TotalRecords)> GetMe(string search, int pageNumber, int pageSize);
    }
}
