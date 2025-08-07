using Domain.Base.Services;
using Domain.Common.API;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Course;
using Domain.Model.Response.Course;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CourseServices : BaseService, ICourseServices
    {
        private readonly IRepositoryBase<Courses>? _course;
        private readonly IRepositoryBase<Department>? _department;
        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        private readonly ISchoolAPI _schoolAPI;
        public CourseServices(IRepositoryBase<Courses>? course, ITokenServices tokenServices, IRepositoryBase<Department>? department, ISchoolAPI schoolAPI = null)
        {
            _course = course;
            _department = department;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
            _schoolAPI = schoolAPI;
            _schoolAPI.SetAccount(-1).GetAwaiter().GetResult();
        }

        public async Task<HttpResponse> CreateAsync(CourseRequest courseRequest)
        {
            CourseResponse courseCheck = await _schoolAPI.GetInfoCourse(courseRequest.Code);
            if (courseCheck == null)
                return HttpResponse.Error("Mã không hợp lệ vui lòng kiểm tra lại!", System.Net.HttpStatusCode.BadRequest);

            var existingCourse = await _course!.FindAsync(f => f.Code == courseRequest.Code);
            if (existingCourse != null)
                return HttpResponse.Error("Môn học đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var newCourse = new Courses
            {
                Name = courseCheck.Name,
                Code = courseCheck.Code,
                Credit = courseCheck.Credit,

                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken.Username
            };
            _course.Insert(newCourse);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: $"Tạo môn học \"{newCourse.Name}\" thành công!", statusCode: System.Net.HttpStatusCode.Created);
        }

        public async Task<HttpResponse> UpdateAsync(long? courseId)
        {
            var existingCourse = await _course!.FindAsync(f => f.Id == courseId);
            if (existingCourse == null)
                return HttpResponse.Error("Mã học phần không tồn tại!", System.Net.HttpStatusCode.NotFound);
        
            CourseResponse courseCheck = await _schoolAPI.GetInfoCourse(existingCourse.Code);
            if (courseCheck == null)
                return HttpResponse.Error("Mã học phần không hợp lệ vui lòng kiểm tra lại!", System.Net.HttpStatusCode.BadRequest);

            existingCourse.Name = courseCheck.Name ?? existingCourse.Name;
            existingCourse.Credit = courseCheck.Credit ?? existingCourse.Credit;

            existingCourse.ModifiedDate = DateTime.Now;
            existingCourse.ModifiedBy = userMeToken.Username;

            _course.Update(existingCourse);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật môn học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponse> DeleteAsync(long? courseId)
        {
            var existingCourse = await _course!.FindAsync(f => f.Id == courseId);
            if (existingCourse == null)
                return HttpResponse.Error("Môn học không tồn tại!", System.Net.HttpStatusCode.NotFound);
            _course.Delete(existingCourse);
     
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Xóa môn học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<(List<CourseResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _course.All()
                //.Include(c => c.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(search) ||
                                        x.Code.ToLower().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var courseSearch = await query.Select(x => new CourseResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Credit = x.Credit,
                //DepartmentName = x.Department.Name,
            }).ToListAsync();

            return (courseSearch, TotalRecords);
        }
    }
}
