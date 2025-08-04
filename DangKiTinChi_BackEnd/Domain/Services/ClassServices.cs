using Domain.Base.Services;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Class;
using Domain.Model.Response.Class;
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
    public class ClassServices : BaseService, IClassServices
    {
        private readonly IRepositoryBase<Classes> _class;
        private readonly IRepositoryBase<Lecturer> _lecturer;
        private readonly IRepositoryBase<Courses> _course;
        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;

        public ClassServices(IRepositoryBase<Classes> @class, ITokenServices tokenServices, IRepositoryBase<Lecturer> lecturer, IRepositoryBase<Courses> course)
        {
            _class = @class;
            _lecturer = lecturer;
            _course = course;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }

        public async Task<HttpResponse> CreateAsync(ClassRequest classRequest)
        {
            var existingClass = await _class.FindAsync(f => f.Code == classRequest.Code);
            if (existingClass != null)
                return HttpResponse.Error("Lớp học đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var lecturer = await _lecturer.FindAsync(f => f.Id == classRequest.LecturerId);
            if (lecturer == null)
                return HttpResponse.Error("Giảng viên không tồn tại!", System.Net.HttpStatusCode.NotFound);

            var course = await _course.FindAsync(f => f.Id == classRequest.CourseId);
            if (course == null)
                return HttpResponse.Error("Khóa học không tồn tại!", System.Net.HttpStatusCode.NotFound);

            var newClass = new Classes()
            {
                Code = classRequest.Code,
                Name = classRequest.Name,
                Day = classRequest.Day,
                StartTime = classRequest.StartTime,
                EndTime = classRequest.EndTime,
                Room = classRequest.Room,
                MaxStudent = classRequest.MaxStudent,

                LecturerId = lecturer.Id,
                Lecturer = lecturer,
                Course = course,
                CourseId = course.Id,

                CreatedBy = userMeToken?.Username,
                CreatedDate = DateTime.Now
            };

            _class.Insert(newClass);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Tạo lớp học thành công!", statusCode: System.Net.HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long? classId, ClassUpdateRequest classUpdateRequest)
        {
            var existingClass = await _class.FindAsync(f => f.Id == classId);
            if (existingClass == null)
                return HttpResponse.Error("Lớp học không tồn tại!", System.Net.HttpStatusCode.NotFound);
            existingClass.Name = classUpdateRequest.Name ?? existingClass.Name;
            existingClass.Day = classUpdateRequest.Day ?? existingClass.Day;
            existingClass.StartTime = classUpdateRequest.StartTime ?? existingClass.StartTime;
            existingClass.EndTime = classUpdateRequest.EndTime ?? existingClass.EndTime;
            existingClass.Room = classUpdateRequest.Room ?? existingClass.Room;
            existingClass.MaxStudent = classUpdateRequest.MaxStudent ?? existingClass.MaxStudent;

            var lecturer = await _lecturer.FindAsync(f => f.Id == classUpdateRequest.LecturerId);
            if (lecturer != null)
            {
                existingClass.LecturerId = lecturer.Id;
                existingClass.Lecturer = lecturer;
            }
            var course = await _course.FindAsync(f => f.Id == classUpdateRequest.CourseId);
            if (course != null)
            {
                existingClass.CourseId = course.Id;
                existingClass.Course = course;
            }

            _class.Update(existingClass);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật lớp học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponse> DeleteAsync(long? classId)
        {
            var existingClass = await _class.FindAsync(f => f.Id == classId);
            if (existingClass == null)
                return HttpResponse.Error("Lớp học không tồn tại!", System.Net.HttpStatusCode.NotFound);
            _class.Delete(existingClass);

            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Xóa lớp học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<(List<ClassResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _class.All()
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(w => w.Name.ToLower().Contains(search) ||
                                        w.Day.ToString().Contains(search) ||
                                        w.StartTime.ToString().Contains(search) ||
                                        w.EndTime.ToString().Contains(search) ||
                                        w.Room.Contains(search) ||
                                        w.MaxStudent.ToString().Contains(search) ||
                                        w.Lecturer.Name.ToString().Contains(search) ||
                                        w.Course.Name.ToString().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var courseSearch = await query.Select(x => new ClassResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Day = x.Day,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Room = x.Room,
                MaxStudent = x.MaxStudent,
                LecturerName = x.Lecturer.Name,
                CourseName = x.Course.Name
            }).ToListAsync();

            return (courseSearch, TotalRecords);
        }

    }
}
