using Domain.Common.Http;
using Domain.Interfaces.Services;
using Domain.Model.Request.Account;
using Domain.Model.Request.AccountClass;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("account-class")]
    [ApiController]
    public class AccountClassController : Controller
    {
        private readonly IAccountClassServices _accountClassServices;
        public AccountClassController(IAccountClassServices accountClassServices)
        {
            this._accountClassServices = accountClassServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AccountClassRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _accountClassServices.CreateAsync(FormData);

            return response.ToActionResult();
        }
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateAsync(long? Id, AccountClassUpdateRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _accountClassServices.UpdateAsync(Id, FormData);

            return response.ToActionResult();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(long? Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _accountClassServices.DeleteAsync(Id);

            return response.ToActionResult();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (accountClasses, totalRecords) = await _accountClassServices.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(accountClasses, totalRecords, totalPages, pageNumber, pageSize));
        }
    }
}
