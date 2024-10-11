using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        // GET: api/Voucher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherModel>>> GetVouchers()
        {
            var vouchers = await _voucherRepository.GetAllVouchersAsync();
            return Ok(vouchers);
        }

        // GET: api/Voucher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherModel>> GetVoucher(int id)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(id);

            if (voucher == null)
            {
                return NotFound();
            }

            return Ok(voucher);
        }

        // POST: api/Voucher
        [HttpPost]
        public async Task<ActionResult<int>> CreateVoucher(VoucherModel voucherModel)
        {
            var newVoucherId = await _voucherRepository.AddVoucherAsync(voucherModel);
            return CreatedAtAction(nameof(GetVoucher), new { id = newVoucherId }, newVoucherId);
        }

        // PUT: api/Voucher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(int id, VoucherModel voucherModel)
        {
            if (id != voucherModel.VoucherId)
            {
                return BadRequest("Voucher ID mismatch.");
            }

            var existingVoucher = await _voucherRepository.GetVoucherByIdAsync(id);
            if (existingVoucher == null)
            {
                return NotFound();
            }

            await _voucherRepository.UpdateVoucherAsync(id, voucherModel);
            return NoContent();
        }

        // DELETE: api/Voucher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            await _voucherRepository.DeleteVoucherAsync(id);
            return NoContent();
        }

        // GET: api/Voucher/code/ABCD1234
        [HttpGet("code/{code}")]
        public async Task<ActionResult<VoucherModel>> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCodeAsync(code);

            if (voucher == null)
            {
                return NotFound();
            }

            return Ok(voucher);
        }
    }
}
