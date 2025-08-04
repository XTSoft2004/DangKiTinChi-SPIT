using Domain.Base.Services;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Lecturer;
using Domain.Model.Response.Department;
using Domain.Model.Response.Lecturer;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LecturerServices : BaseService, ILecturerServices
    {
        public readonly IRepositoryBase<Lecturer> _lecturer;
        private readonly TokenServices _tokenServices;
        private UserTokenResponse? userMeToken;
        public LecturerServices(IRepositoryBase<Lecturer> lecturer, TokenServices tokenServices)
        {
            _lecturer = lecturer;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> CreateAsync(LecturerRequest FormData)
        {
            var lecturer = new Lecturer
            {
                Name = FormData.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken.Username,
            };

            _lecturer.Insert(lecturer);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Tạo giảng viên thành công", statusCode: System.Net.HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long id, LecturerRequest FormData)
        {
            var lecturer = await _lecturer.FindAsync(x => x.Id == id);
            if (lecturer == null)
            {
                return HttpResponse.Error("Giảng viên không tồn tại", System.Net.HttpStatusCode.NotFound);
            }

            lecturer.Name = FormData.Name;
            lecturer.ModifiedDate = DateTime.Now;   
            lecturer.ModifiedBy = userMeToken.Username;

            _lecturer.Update(lecturer);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Cập nhật giảng viên thành công");
        }
        public async Task<HttpResponse> DeleteAsync(long id)
        {
            var lecturer = await _lecturer.FindAsync(x => x.Id == id);
            if (lecturer == null)
            {
                return HttpResponse.Error("Giảng viên không tồn tại", System.Net.HttpStatusCode.NotFound);
            }

            _lecturer.TotallyDelete(lecturer);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Xoá giảng viên thành công");
        }
        public async Task<(List<LecturerResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _lecturer.All().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var TotalRecords = await query.CountAsync();

            if (pageSize != -1 && pageNumber != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var lecturers = await query.Select(x => new LecturerResponse
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return (lecturers, TotalRecords);
        }
    }
}
