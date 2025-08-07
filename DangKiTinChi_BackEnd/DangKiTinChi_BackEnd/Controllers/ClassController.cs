using DangKiTinChi_BackEnd.Attribute;
using Domain.Common;
using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Common;
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
        private readonly IHttpContextHelper _httpContextHelper;
        public ClassController(IClassServices services, IHttpContextHelper httpContextHelper)
        {
            _services = services;
            _httpContextHelper = httpContextHelper;
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

        /// <summary>
        /// Tạo lớp học mới
        /// </summary>
        /// <remarks>Tạo lớp học mới</remarks>
        [HttpPost]
        [RequireAccountIdHeader]
        public async Task<IActionResult> CreateAsync(ClassRequest classRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var accountId = _httpContextHelper.GetAccountId();
            if (accountId == null)
                return BadRequest("X-Account-Id không tồn tại vui lòng kiểm tra lại!");

            var response = await _services.CreateAsync(classRequest);
            return response.ToActionResult();
        }

        /// <summary>
        /// Cập nhật thông tin của lớp học theo mã lớp học (ClassID).
        /// </summary>
        /// <remarks>Cập nhật thông tin của lớp học theo mã lớp học (ClassID).</remarks>
        [HttpPost("{ClassID}")]
        [RequireAccountIdHeader]
        public async Task<IActionResult> UpdateAsync(long ClassID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var accountId = _httpContextHelper.GetAccountId();
            if (accountId == null)
                return BadRequest("X-Account-Id không tồn tại vui lòng kiểm tra lại!");

            var response = await _services.UpdateAsync(ClassID);
            return response.ToActionResult();
        }

        /// <summary>
        /// Xoá lớp học theo mã lớp học (ClassID).
        /// </summary>
        /// <remarks>Xoá lớp học theo mã lớp học (ClassID).</remarks>
        [HttpDelete("{ClassID}")]
        public async Task<IActionResult> DeleteAsync(long ClassID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.DeleteAsync(ClassID);
            return response.ToActionResult();
        }
    }
}
