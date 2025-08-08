using Domain.Entities;
using Domain.Model.Request.Account;
using Domain.Model.Request.Class;
using Domain.Model.Request.Course;
using Domain.Model.Response.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.API
{
    public interface ISchoolAPI
    {
        /// <summary>
        /// Lấy token đăng nhập từ trang web trường học
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> RefreshTokenLogin(string url);
        /// <summary>
        /// Đăng nhập tài khoản vào hệ thống trường học
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        Task<bool> LoginAccount(Account account);
        /// <summary>
        /// Lấy thông tin của môn học đó
        /// </summary>
        /// <param name="courseCode"></param>
        /// <returns></returns>
        Task<CourseResponse> GetInfoCourse(string courseCode);
        /// <summary>
        /// Lấy thông tin của lớp học
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        Task<ClassCheckResponse> GetInfoClass(string classCode);
        /// <summary>
        /// Đặt tài khoản account
        /// </summary>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        void SetAccountContext();
    }
}
