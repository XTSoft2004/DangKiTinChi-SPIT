using Domain.Base.Services;
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

        public CourseServices(IRepositoryBase<Courses>? course, ITokenServices tokenServices, IRepositoryBase<Department>? department)
        {
            _course = course;
            _department = department;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }

        public async Task<HttpResponse> CreateAsync(CourseRequest courseRequest)
        {
            var department = await _department!.FindAsync(f => f.Id == courseRequest.DepartmentId);
            if(department == null)
                return HttpResponse.Error("Khoa không tồn tại!", System.Net.HttpStatusCode.NotFound);

            var existingCourse = await _course!.FindAsync(f => f.Code == courseRequest.Code);
            if (existingCourse != null)
                return HttpResponse.Error("Khóa học đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var newCourse = new Courses
            {
                Name = courseRequest.Name,
                Code = courseRequest.Code,
                Credit = courseRequest.Credit,
                Department = department,
                DepartmentId = department.Id,

                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken.Username
            };
            _course.Insert(newCourse);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Tạo khóa học thành công!", statusCode: System.Net.HttpStatusCode.Created);
        }

        public async Task<HttpResponse> UpdateAsync(long? courseId, CourseUpdateRequest courseRequest)
        {
            var existingCourse = await _course!.FindAsync(f => f.Id == courseId);
            if (existingCourse == null)
                return HttpResponse.Error("Khóa học không tồn tại!", System.Net.HttpStatusCode.NotFound);

            existingCourse.Name = courseRequest.Name ?? existingCourse.Name;
            existingCourse.Credit = courseRequest.Credit ?? existingCourse.Credit;

            if(existingCourse.DepartmentId != courseRequest.DepartmentId)
            {
                var department = await _department!.FindAsync(f => f.Id == courseRequest.DepartmentId);
                if (department == null)
                    return HttpResponse.Error("Khoa không tồn tại!", System.Net.HttpStatusCode.NotFound);

                existingCourse.Department = department;
                existingCourse.DepartmentId = department.Id;
            }

            existingCourse.ModifiedDate = DateTime.Now;
            existingCourse.ModifiedBy = userMeToken.Username;

            _course.Update(existingCourse);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật khóa học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponse> DeleteAsync(long? courseId)
        {
            var existingCourse = await _course!.FindAsync(f => f.Id == courseId);
            if (existingCourse == null)
                return HttpResponse.Error("Khóa học không tồn tại!", System.Net.HttpStatusCode.NotFound);
            _course.Delete(existingCourse);
     
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Xóa khóa học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<(List<CourseResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _course.All()
                .Include(c => c.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
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
                DepartmentName = x.Department.Name,
            }).ToListAsync();

            return (courseSearch, TotalRecords);
        }
    }
}
