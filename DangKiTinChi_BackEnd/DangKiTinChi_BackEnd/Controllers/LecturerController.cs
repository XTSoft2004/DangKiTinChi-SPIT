using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Lecturer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("lecturer")]
    [ApiController]
    [Authorize]
    public class LecturerController : Controller
    {
        private readonly ILecturerServices _lecturerServices;
        public LecturerController(ILecturerServices lecturerServices)
        {
            _lecturerServices = lecturerServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(LecturerRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _lecturerServices.CreateAsync(FormData);
            return response.ToActionResult();
        }
        [HttpPatch("{LecturerId}")]
        public async Task<IActionResult> UpdateAsync(long? LecturerId, LecturerRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _lecturerServices.UpdateAsync(LecturerId.Value, FormData);
            return response.ToActionResult();
        }
        [HttpDelete("{LecturerId}")]
        public async Task<IActionResult> DeleteAsync(long? LecturerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _lecturerServices.DeleteAsync(LecturerId.Value);
            return response.ToActionResult();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (data, totalRecords) = await _lecturerServices.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(data, totalRecords, totalPages, pageNumber, pageSize));
        }
    }
}
