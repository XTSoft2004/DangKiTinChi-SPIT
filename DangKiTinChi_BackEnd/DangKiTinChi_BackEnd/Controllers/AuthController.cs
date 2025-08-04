using Domain.Interfaces.Services;
using Domain.Model.Request;
using Domain.Model.Request.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthServices _services;
        public AuthController(IAuthServices services)
        {
            _services = services;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.RegisterAsync(registerRequest);
            return response.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.LoginAsync(loginRequest);
            return response.ToActionResult();
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.LogoutUser();
            return response.ToActionResult();
        }
        [Authorize]
        [HttpGet("refesh-token")]
        public async Task<IActionResult> RefreshTokenAccount()
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _services.RefreshTokenAccount();
            return response.ToActionResult();
        }
    }
}
