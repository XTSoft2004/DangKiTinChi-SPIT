using Domain.Common.Http;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountServices _account;

        public AccountController(IAccountServices account)
        {
            _account = account;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (accounts, totalRecords) = await _account.GetAccounts(search, pageNumber, pageSize);

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(accounts, totalRecords, totalPages, pageNumber, pageSize));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AccountRequest accountRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _account.CreateAsync(accountRequest);
            return response.ToActionResult();
        }
        [HttpPatch("{AccountID}")]
        public async Task<IActionResult> UpdateAsync(long? AccountID, AccountUpdateRequest accountUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _account.UpdateAsync(AccountID, accountUpdateRequest);
            return response.ToActionResult();
        }

        [HttpDelete("{AccountID}")]
        public async Task<IActionResult> DeleteAsync(long? AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _account.DeleteAsync(AccountID);
            return response.ToActionResult();
        }
        [HttpGet("login-account/{AccountID}")]
        public async Task<IActionResult> LoginAccount(long? AccountID)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);
            var response = await _account.LoginAccount(AccountID);
            return response.ToActionResult();
        }
    }
}
