using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Auth;
using Domain.Model.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserServices? _services;

        public UserController(IUserServices? services)
        {
            _services = services;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services?.GetMe();
            return response.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (users, totalRecords) = await _services?.GetUsers(search, pageNumber, pageSize);

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(users, totalRecords, totalPages, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services?.CreateAsync(userRequest);
            if (userRequest == null)
                return BadRequest(new { Message = "Thông tin người dùng không hợp lệ." });

            return response.ToActionResult();
        }

        [HttpPatch("{UserId}")]
        public async Task<IActionResult> UpdateAsync(long? UserId, UserUpdateRequest userUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services?.UpdateAsync(UserId, userUpdateRequest);
            if (response == null)
                return NotFound(new { Message = "Người dùng không tồn tại." });

            return response.ToActionResult();
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteAsync(long? UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);
         
            var response = await _services?.DeleteAsync(UserId);
            if (response == null)
                return NotFound(new { Message = "Người dùng không tồn tại." });
       
            return response.ToActionResult();
        }
    }
}
