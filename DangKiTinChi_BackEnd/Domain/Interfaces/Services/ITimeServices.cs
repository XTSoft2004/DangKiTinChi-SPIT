using Domain.Common.Http;
using Domain.Entities;
using Domain.Model.Request.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ITimeServices
    {
        /// <summary>
        /// Tạo thời gian của lớp học
        /// </summary>
        /// <param name="timeRequest"></param>
        /// <returns></returns>
        Task<Time> CreateAsync(TimeRequest timeRequest);
        /// <summary>
        /// Lấy danh sách time tồn tại
        /// </summary>
        /// <param name="listTimeRequest"></param>
        /// <returns></returns>
        Task<List<Time>> CheckTimeExist(List<TimeRequest> listTimeRequest);
    }
}
