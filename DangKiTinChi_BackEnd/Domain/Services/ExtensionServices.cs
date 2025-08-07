using Domain.Base.Services;
using Domain.Common.API;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ExtensionServices : BaseService
    {
        private readonly IRepositoryBase<Courses> _course;
        private readonly IRepositoryBase<Classes> _classes;
        private readonly IRepositoryBase<Account> _account;
        private readonly ICourseServices _courseServices;
        private readonly IClassServices _classServices;
        private readonly ISchoolAPI _schoolAPI;
        public ExtensionServices(IRepositoryBase<Courses> course, IRepositoryBase<Classes> classes, IRepositoryBase<Account> account, ICourseServices courseServices, IClassServices classServices, ISchoolAPI schoolAPI)
        {
            _course = course;
            _classes = classes;
            _account = account;
            _courseServices = courseServices;
            _classServices = classServices;
            _schoolAPI = schoolAPI;
        }
        //public async Task<HttpResponse> CheckCourseAndClass(string code)
        //{
        //    bool isCheckCourse = await _course.FindAsync(f => f.Code == code) != null;
        //    bool isCheckClass = await _classes.FindAsync(f => f.Code == code) != null;
        //    if (!isCheckClass && !isCheckCourse)
        //    {
        //        var codeSplit = code.Split('.');
        //        if (codeSplit.Length == 0)
        //        {
        //            var courseInfo = await _schoolAPI.GetInfoCourse(code);
        //            if (courseInfo == null)
        //                return HttpResponse.Error("Mã môn học này không tồn tại, vui lòng kiểm tra lại!", HttpStatusCode.NotFound);
               
        //            var courseResponse = await _courseServices.CreateAsync(courseInfo);
        //            if (courseResponse.StatusCode == 200)
        //                return HttpResponse.OK(message: $"Đã thêm môn học {courseInfo.Name} vào hệ thống!");
        //            else
        //                return HttpResponse.Error("Thêm môn học thất bại, vui lòng kiểm tra lại!", HttpStatusCode.BadRequest);
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
    }
}
