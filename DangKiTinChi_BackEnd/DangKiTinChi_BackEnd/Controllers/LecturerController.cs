using Domain.Interfaces.Services;
using Domain.Model.Request.Lecturer;
using Microsoft.AspNetCore.Mvc;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("lecturer")]
    [ApiController]
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
                return BadRequest("Invalid model state.");

            var response = await _lecturerServices.CreateAsync(FormData);
            return response.ToActionResult();
        }
        [HttpPatch("{LecturerId}")]
        public async Task<IActionResult> UpdateAsync(long? LecturerId, LecturerRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            var response = await _lecturerServices.UpdateAsync(LecturerId.Value, FormData);
            return response.ToActionResult();
        }
        [HttpDelete("{LecturerId}")]
        public async Task<IActionResult> DeleteAsync(long? LecturerId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            var response = await _lecturerServices.DeleteAsync(LecturerId.Value);
            return response.ToActionResult();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string? search = null, int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state.");

            var (data, totalRecords) = await _lecturerServices.GetAllAsync(search, pageNumber, pageSize);

            return Ok(new { Data = data, TotalRecords = totalRecords });
        }
    }
}
