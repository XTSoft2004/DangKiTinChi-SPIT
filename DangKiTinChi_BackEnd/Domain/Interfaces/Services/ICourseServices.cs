using Domain.Common.Http;
using Domain.Model.Request.Course;
using Domain.Model.Response.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICourseServices
    {
        Task<HttpResponse> CreateAsync(CourseRequest courseRequest);

        Task<HttpResponse> UpdateAsync(long? courseId, CourseUpdateRequest courseRequest);

        Task<HttpResponse> DeleteAsync(long? courseId);

        Task<(List<CourseResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize);
    }
}
