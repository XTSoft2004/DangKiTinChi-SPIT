using Domain.Base.Services;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TimeServices : BaseService, ITimeServices
    {
        private readonly IRepositoryBase<Time>? _times;

        public TimeServices(IRepositoryBase<Time>? times)
        {
            _times = times;
        }

        public async Task<Time> CreateAsync(TimeRequest timeRequest)
        {
            var existTime = _times.Find(f => f.Room ==  timeRequest.Room && 
                                        f.StartTime == timeRequest.StartTime && 
                                        f.EndTime == timeRequest.EndTime && 
                                        f.Day == timeRequest.Day);

            if(existTime != null)
                return existTime;

            //if (existTime != null)
            //    return HttpResponse.Error("Thời gian này đã tồn tại, vui lòng kiểm tra lại!", statusCode: System.Net.HttpStatusCode.Conflict);

            Time time = new Time()
            {
                Day = timeRequest.Day,
                StartTime = timeRequest.StartTime,
                EndTime = timeRequest.EndTime,
                Room = timeRequest.Room,
            };

            _times.Insert(time);
            await UnitOfWork.CommitAsync();

            //return HttpResponse.OK(message: "Thêm thời gian thành công", data: time);
            return time;
        }
        public async Task<List<Time>> CheckTimeExist(List<TimeRequest> listTimeRequest)
        {
            List<Time> timeList = new List<Time>();
            foreach (var timeRequest in listTimeRequest)
            {
                Time timeResult = null;
                timeResult = _times.Find(f => f.Room == timeRequest.Room &&
                            f.StartTime == timeRequest.StartTime &&
                            f.EndTime == timeRequest.EndTime &&
                            f.Day == timeRequest.Day);
             
                if(timeResult == null)
                    timeResult = await CreateAsync(timeRequest);

                if (timeResult != null)
                    timeList.Add(timeResult);
            }

            return timeList;
        }
    }
}
