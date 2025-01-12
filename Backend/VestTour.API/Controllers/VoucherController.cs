using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interface;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        // Get all vouchers
        [HttpGet]
        public async Task<IActionResult> GetAllVouchers()
        {
            var response = await _voucherService.GetAllVouchersAsync();
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok(response.Data);
        }

        // Get voucher by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherById(int id)
        {
            var response = await _voucherService.GetVoucherByIdAsync(id);
            if (!response.Success)
                return NotFound(response.Message);
            return Ok(response.Data);
        }

        // Add a new voucher
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddVoucher([FromBody] VoucherModel voucherModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _voucherService.AddVoucherAsync(voucherModel);
            if (!response.Success)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetVoucherById), new { id = response.Data }, response.Data);
        }

        // Update an existing voucher
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVoucher(int id, [FromBody] UpdateVoucherModel updateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _voucherService.UpdateVoucherAsync(id, updateModel);
            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }

        // Delete a voucher by ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var response = await _voucherService.DeleteVoucherAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }

        // Get vouchers by code
        [HttpGet("search")]
        public async Task<IActionResult> GetVouchersByCode([FromQuery] string code)
        {
            var response = await _voucherService.GetVouchersByCodeAsync(code);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        // Get valid vouchers
        [HttpGet("valid")]
        public async Task<IActionResult> GetValidVouchers()
        {
            var response = await _voucherService.GetValidVouchersAsync();
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        // Get voucher by code
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetVoucherByCode(string code)
        {
            var response = await _voucherService.GetVoucherByCodeAsync(code);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }
        [HttpPatch("status/{voucherId}")]
        public async Task<IActionResult> ChangeVoucherStatus(int voucherId, [FromBody] string newStatus)
        {
            var result = await _voucherService.ChangeVoucherStatusAsync(voucherId, newStatus);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

    }
}
