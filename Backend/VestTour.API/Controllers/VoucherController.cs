using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Repository.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;

namespace VestTour.Controllers
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
                return NotFound(new { Message = Error.VoucherNotFound });
            }

            return Ok(voucher);
        }

        // GET: api/Voucher/valid
        [HttpGet("valid")]
        public async Task<ActionResult<IEnumerable<VoucherModel>>> GetValidVouchers()
        {
            var validVouchers = await _voucherRepository.GetValidVouchersAsync();
            return Ok(validVouchers);
        }

        

        // POST: api/Voucher
        [HttpPost]
        public async Task<ActionResult<int>> CreateVoucher(VoucherModel voucherModel)
        {
            var newVoucherId = await _voucherRepository.AddVoucherAsync(voucherModel);
            if (newVoucherId > 0)
            {
                return CreatedAtAction(nameof(GetVoucher), new { id = newVoucherId }, new { Message = Success.VoucherAdded, VoucherId = newVoucherId });
            }
            return BadRequest(new { Message = Error.VoucherAddFailed });
        }

        // PUT: api/Voucher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(int id, VoucherModel voucherModel)
        {
            if (id != voucherModel.VoucherId)
            {
                return BadRequest(new { Message = Error.VoucherUpdateFailed });
            }

            var existingVoucher = await _voucherRepository.GetVoucherByIdAsync(id);
            if (existingVoucher == null)
            {
                return NotFound(new { Message = Error.VoucherNotFound });
            }

            await _voucherRepository.UpdateVoucherAsync(id, voucherModel);
            return Ok(new { Message = Success.VoucherUpdated });
        }

        // DELETE: api/Voucher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound(new { Message = Error.VoucherNotFound });
            }

            await _voucherRepository.DeleteVoucherAsync(id);
            return Ok(new { Message = Success.VoucherDeleted });
        }

        // GET: api/Voucher/code/ABCD1234
        [HttpGet("code/{code}")]
        public async Task<ActionResult<VoucherModel>> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCodeAsync(code);

            if (voucher == null)
            {
                return NotFound(new { Message = Error.VoucherNotFound });
            }

            return Ok(voucher);
        }
    }
}
