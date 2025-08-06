using Domain.Common.Http;
using Domain.Interfaces.Services;
using Domain.Model.Request.HistoryMoney;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("history-money")]
    [ApiController]
    [Authorize]
    public class HistoryMoneyController : Controller
    {
        private readonly IHistoryMoneyServices _historyMoneyServices;
        public HistoryMoneyController(IHistoryMoneyServices historyMoneyServices)
        {
            _historyMoneyServices = historyMoneyServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(HistoryMoneyRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _historyMoneyServices.CreateAsync(FormData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(long? id, HistoryMoneyRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _historyMoneyServices.UpdateAsync(id, FormData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long? id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _historyMoneyServices.DeleteAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (historyMoney, totalRecords) = await _historyMoneyServices.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(historyMoney, totalRecords, totalPages, pageNumber, pageSize));
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMe(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(DefaultString.INVALID_MODEL);
            }

            var (historyMoney, totalRecords) = await _historyMoneyServices.GetMe(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(ResponseArray.ResponseList(historyMoney, totalRecords, totalPages, pageNumber, pageSize));
        }
    } 
}
