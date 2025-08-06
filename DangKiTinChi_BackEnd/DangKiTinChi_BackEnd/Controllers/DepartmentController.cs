using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DepartmentRequest formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _departmentServices.CreateAsync(formData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPatch("{DepartmentId}")]
        public async Task<IActionResult> UpdateAsync(long? DepartmentId, DepartmentRequest formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _departmentServices.UpdateAsync(DepartmentId, formData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long? DepartmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _departmentServices.DeleteAsync(DepartmentId);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (departments, totalRecords) = await _departmentServices.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(departments, totalRecords, totalPages, pageNumber, pageSize));
        }
    }
}
