using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Course;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("course")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseServices _services;

        public CourseController(ICourseServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (courses, totalRecords) = await _services.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(courses, totalRecords, totalPages, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourseRequest courseRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.CreateAsync(courseRequest);
            return response.ToActionResult();
        }

        [HttpPatch("{CourseID}")]
        public async Task<IActionResult> UpdateAsync(long? CourseID, CourseUpdateRequest courseUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.UpdateAsync(CourseID, courseUpdateRequest);
            return response.ToActionResult();
        }

        [HttpDelete("{CourseID}")]
        public async Task<IActionResult> DeleteAsync(long? CourseID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.DeleteAsync(CourseID);
            return response.ToActionResult();
        }
    }
}
