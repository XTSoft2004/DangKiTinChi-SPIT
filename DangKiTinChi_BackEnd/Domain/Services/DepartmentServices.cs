using Domain.Base.Services;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Model.Request.Department;
using Domain.Model.Response.Department;
using Domain.Model.Response.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class DepartmentServices : BaseService, IDepartmentServices
    {
        private readonly IRepositoryBase<Department> _department;
        private readonly ITokenServices _tokenServices;
        private UserTokenResponse? userMeToken;

        public DepartmentServices(IRepositoryBase<Department> department, ITokenServices tokenServices)
        {
            _department = department;
            _tokenServices = tokenServices;
            userMeToken = _tokenServices.GetTokenBrowser();
        }
        public async Task<HttpResponse> CreateAsync(DepartmentRequest FormData)
        {
            var department = new Department
            {
                Code = FormData.Code,
                Name = FormData.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = userMeToken.Username,
            };

            _department.Insert(department);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Tạo khoa thành công", statusCode: System.Net.HttpStatusCode.Created);
        }
        public async Task<HttpResponse> UpdateAsync(long? DepartmentId, DepartmentRequest FormData)
        {
            var existingDepartment = await _department.FindAsync(f => f.Id == DepartmentId);

            if (existingDepartment == null)
                return HttpResponse.Error("Khoa không tồn tại!", System.Net.HttpStatusCode.NotFound);

            existingDepartment.Code = FormData.Code ?? existingDepartment.Code;
            existingDepartment.Name = FormData.Name ?? existingDepartment.Name;
            existingDepartment.ModifiedDate = DateTime.Now;
            existingDepartment.ModifiedBy = userMeToken.Username;

            _department.Update(existingDepartment);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Cập nhật khoa thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<HttpResponse> DeleteAsync(long? DepartmentId)
        {
            var existingDepartment = await _department.FindAsync(f => f.Id == DepartmentId);

            if (existingDepartment == null)
                return HttpResponse.Error("Khoa không tồn tại!", System.Net.HttpStatusCode.NotFound);

            _department.Delete(existingDepartment);
            await UnitOfWork.CommitAsync();

            return HttpResponse.OK(message: "Xóa khoa thành công", statusCode: System.Net.HttpStatusCode.OK);
        }
        public async Task<(List<DepartmentResponse>?, int TotalRecords)> GetAllAsync(string search, int pageNumber, int pageSize)
        {
            var query = _department.All().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(search) || d.Code.ToLower().Contains(search));
            }

            var TotalRecords = await query.CountAsync();

            if (pageNumber != -1 && pageSize != -1)
            {
                query = query.OrderByDescending(d => d.ModifiedDate)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize);
            }

            var departmentSearch = await query
                .Select(d => new DepartmentResponse
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name
                })
                .ToListAsync();

            return (departmentSearch, TotalRecords);
        }
    }
}
