using Domain.Base.Services;
using Domain.Common;
using Domain.Common.API;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Common;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Class;
using Domain.Model.Request.Course;
using Domain.Model.Request.Time;
using Domain.Model.Response.Class;
using Domain.Model.Response.Course;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ClassServices : BaseService, IClassServices
    {
        private readonly IRepositoryBase<Classes> _class;
        private readonly IRepositoryBase<TimeClass> _timeClass;
        private readonly IRepositoryBase<Courses> _course;
        private readonly IRepositoryBase<Time> _time;
        private readonly ITokenServices _tokenServices;
        private readonly ITimeServices _timeServices;
        private readonly ICourseServices _courseServices;
        private UserTokenResponse? userMeToken;
        private readonly ISchoolAPI _schoolAPI;
        private readonly IRepositoryBase<Account> _account;
        private readonly IHttpContextHelper _httpContextHelper;
        public ClassServices(IRepositoryBase<Classes> @class, ITokenServices tokenServices, IRepositoryBase<Courses> course, ITimeServices timeServices, IRepositoryBase<TimeClass> timeClass, ISchoolAPI schoolAPI, IRepositoryBase<Time> time, ICourseServices courseServices, IRepositoryBase<Account> account, IHttpContextHelper httpContextHelper)
        {
            _class = @class;
            _course = course;
            _tokenServices = tokenServices;
            _timeServices = timeServices;
            _timeClass = timeClass;
            _schoolAPI = schoolAPI;
            _time = time;
            _courseServices = courseServices;
            _account = account;
            userMeToken = _tokenServices.GetTokenBrowser();
            _httpContextHelper = httpContextHelper;
        }

        public async Task<HttpResponse> CreateAsync(ClassRequest classRequest)
        {
            var listAccounts = _account.ListBy(l => l.UserId == userMeToken.Id).Select(s => (long?)s.Id).ToList();
            if (!listAccounts.Contains(_httpContextHelper.GetAccountId()))
                return HttpResponse.Error("Account này không tồn tại ở người dùng này!", System.Net.HttpStatusCode.Forbidden);

            var existingClass = await _class.FindAsync(f => f.Code == classRequest.Code);
            if (existingClass != null)
                return HttpResponse.Error("Lớp học đã tồn tại!", System.Net.HttpStatusCode.Conflict);

            var classCheck = await _schoolAPI.GetInfoClass(classRequest.Code);
            if (classCheck == null)
                return HttpResponse.Error("Mã lớp đã xảy ra lỗi, vui lòng kiểm tra lại!");

            ReloadCourse:
            var course = await _course.FindAsync(f => f.Name == classCheck.CourseName);
            if (course == null)
            {
                string pattern = @"\d{4}-\d{4}\.\d\.(.*?)\.\d+";
                Match match = Regex.Match(classRequest.Code, pattern);
                if (match.Success)
                {
                    string courseCode = match.Groups[1].Value;
                    var courseRegister = await _courseServices.CreateAsync(new CourseRequest()
                    {
                        Code = courseCode
                    });
                    if (courseRegister.StatusCode != (int)HttpStatusCode.Created)
                        return HttpResponse.Error("Không thể tạo khóa học mới, vui lòng kiểm tra lại mã lớp học!");
                    goto ReloadCourse;
                }
            }

            List<Time> times = times = await _timeServices.CheckTimeExist(classCheck.TimesRequest);
            if (times.Count == 0)
                return HttpResponse.Error("Kiểm tra các thời gian không hợp lệ, vui lòng kiểm tra lại");

            var newClass = new Classes()
            {
                Code = classCheck.Code,
                Name = classCheck.Name,
                MaxStudent = classCheck.MaxStudent,

                Course = course,
                CourseId = course.Id,

                CreatedBy = userMeToken?.Username,
                CreatedDate = DateTime.Now
            };
            _class.Insert(newClass);
            await UnitOfWork.CommitAsync();

            foreach(var time in times)
            {
                _timeClass.Insert(new TimeClass()
                {
                    ClassId = newClass.Id,
                    Classes = newClass,
                    TimeId = time.Id,
                    Times = time
                });
            }
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: $"Tạo lớp học {classCheck.Name} thành công!", statusCode: System.Net.HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long classId)
        {
            var listAccounts = _account.ListBy(l => l.UserId == userMeToken.Id).Select(s => (long?)s.Id).ToList();
            if (!listAccounts.Contains(_httpContextHelper.GetAccountId()))
                return HttpResponse.Error("Account này không tồn tại ở người dùng này!", System.Net.HttpStatusCode.Forbidden);

            var existingClass = await _class.FindAsync(f => f.Id == classId);
            if (existingClass == null)
                return HttpResponse.Error("Lớp học không tồn tại!", System.Net.HttpStatusCode.NotFound);
           

            var classCheck = await _schoolAPI.GetInfoClass(existingClass.Code);
            if (classCheck == null)
                return HttpResponse.Error("Mã lớp đã xảy ra lỗi, vui lòng kiểm tra lại!");

            ReloadCourse:
            var course = await _course.FindAsync(f => f.Name == classCheck.CourseName);
            if (course == null)
            {
                string pattern = @"\d{4}-\d{4}\.\d\.(.*?)\.\d+";
                Match match = Regex.Match(existingClass.Code, pattern);
                if (match.Success)
                {
                    string courseCode = match.Groups[1].Value;
                    var courseRegister = await _courseServices.CreateAsync(new CourseRequest()
                    {
                        Code = courseCode
                    });
                    if (courseRegister.StatusCode != (int)HttpStatusCode.Created)
                        return HttpResponse.Error("Không thể tạo khóa học mới, vui lòng kiểm tra lại mã lớp học!");
                    goto ReloadCourse;
                }
            }

            List<Time> times = times = await _timeServices.CheckTimeExist(classCheck.TimesRequest);
            if (times.Count == 0)
                return HttpResponse.Error("Kiểm tra các thời gian không hợp lệ, vui lòng kiểm tra lại");

            #region Xử lý thời gian lớp học
            var listTimeClass = _timeClass.ListBy(l => l.ClassId == classId).Select(s => s.TimeId).ToList();
            var timeIdRemove = listTimeClass.Except(times.Select(s => s.Id)).ToList();
            var timeClassRemove = _timeClass.ListBy(l => timeIdRemove.Contains(l.TimeId));
            _timeClass.DeleteRange(timeClassRemove);

            var timeIdAdd = times.Select(s => s.Id).Except(listTimeClass).ToList();
            _timeClass.InsertRange(new List<TimeClass>(timeIdAdd.Select(s => new TimeClass()
            {
                TimeId = s,
                ClassId = classId
            })));
            await UnitOfWork.CommitAsync();
            #endregion


            existingClass.Name = classCheck.Name ?? existingClass.Name;
            existingClass.MaxStudent = classCheck.MaxStudent ?? existingClass.MaxStudent;
            
            if(existingClass.CourseId != course.Id)
            {
                existingClass.CourseId = course.Id;
                existingClass.Course = course;
            }

            _class.Update(existingClass);
            await UnitOfWork.CommitAsync();
            return HttpResponse.OK(message: "Cập nhật lớp học thành công!", statusCode: System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpResponse> DeleteAsync(long classId)
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
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(w => w.Name.ToLower().Contains(search) ||
                                        w.MaxStudent.ToString().Contains(search) ||
                                        w.Course.Name.ToString().ToLower().Contains(search));
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
                MaxStudent = x.MaxStudent,
                CourseName = x.Course.Name
            }).ToListAsync();

            return (courseSearch, TotalRecords);
        }
    }
}
