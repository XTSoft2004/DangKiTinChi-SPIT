using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("class")]
    [ApiController]
    [Authorize]
    public class ClassController : Controller
    {
        private readonly IClassServices _services;

        public ClassController(IClassServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetClasses(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (classes, totalRecords) = await _services.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(classes, totalRecords, totalPages, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ClassRequest classRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.CreateAsync(classRequest);
            return response.ToActionResult();
        }

        [HttpPatch("{ClassID}")]
        public async Task<IActionResult> UpdateAsync(long? ClassID, ClassUpdateRequest classUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.UpdateAsync(ClassID, classUpdateRequest);
            return response.ToActionResult();
        }

        [HttpDelete("{ClassID}")]
        public async Task<IActionResult> DeleteAsync(long? ClassID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.DeleteAsync(ClassID);
            return response.ToActionResult();
        }
    }
}
