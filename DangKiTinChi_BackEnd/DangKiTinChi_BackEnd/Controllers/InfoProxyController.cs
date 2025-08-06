using Domain.Interfaces.Services;
using Domain.Model.Request.InfoProxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Domain.Common.AppConstants;

namespace DangKiTinChi_BackEnd.Controllers
{
    [Route("info-proxy")]
    [ApiController]
    [Authorize]
    public class InfoProxyController : Controller
    {
        private readonly IInfoProxyServices _infoProxyServices;
        public InfoProxyController(IInfoProxyServices infoProxyServices)
        {
            _infoProxyServices = infoProxyServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(InfoProxyRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _infoProxyServices.CreateAsync(FormData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(long? id, InfoProxyRequest FormData)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _infoProxyServices.UpdateAsync(id, FormData);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long? id)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var response = await _infoProxyServices.DeleteAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(string search = "", int pageNumber = 1, int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(DefaultString.INVALID_MODEL);

            var (infoProxies, totalRecords) = await _infoProxyServices.GetAllAsync(search, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return Ok(new
            {
                Data = infoProxies,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }
    }
}
