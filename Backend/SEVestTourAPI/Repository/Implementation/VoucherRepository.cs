using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEVestTourAPI.Repository.Implementation
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public VoucherRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all vouchers
        public async Task<List<VoucherModel>> GetAllVouchersAsync()
        {
            var vouchers = await _context.Vouchers!.ToListAsync();
            return _mapper.Map<List<VoucherModel>>(vouchers);
        }

        // Get voucher by ID
        public async Task<VoucherModel?> GetVoucherByIdAsync(int voucherId)
        {
            var voucher = await _context.Vouchers!.FindAsync(voucherId);
            return _mapper.Map<VoucherModel>(voucher);
        }

        // Add a new voucher
        public async Task<int> AddVoucherAsync(VoucherModel voucherModel)
        {
            var newVoucher = _mapper.Map<Voucher>(voucherModel);
            _context.Vouchers!.Add(newVoucher);
            await _context.SaveChangesAsync();
            return newVoucher.VoucherId;
        }

        // Update an existing voucher
        public async Task UpdateVoucherAsync(int id, VoucherModel voucherModel)
        {
            if (id == voucherModel.VoucherId)
            {
                var updateVoucher = _mapper.Map<Voucher>(voucherModel);
                _context.Vouchers!.Update(updateVoucher);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a voucher by ID
        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await _context.Vouchers!.FindAsync(voucherId);
            if (voucher != null)
            {
                _context.Vouchers!.Remove(voucher);
                await _context.SaveChangesAsync();
            }
        }

        // Get voucher by code
        public async Task<VoucherModel?> GetVoucherByCodeAsync(string code)
        {
            var voucher = await _context.Vouchers!.FirstOrDefaultAsync(v => v.VoucherCode == code);
            return _mapper.Map<VoucherModel>(voucher);
        }
        // Get valid vouchers (DateStart <= Now <= DateEnd)
        public async Task<List<VoucherModel>> GetValidVouchersAsync()
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            var validVouchers = await _context.Vouchers!
                .Where(v => v.DateStart <= now && v.DateEnd >= now)
                .ToListAsync();

            return _mapper.Map<List<VoucherModel>>(validVouchers);
        }

        // Get vouchers matching the pattern FREESHIPxx or BIGSALEXx
        public async Task<List<VoucherModel>> GetVouchersByPatternAsync()
        {
            var voucherPattern = @"^(FREESHIP\d{2}|BIGSALE\d{2})$";
            var vouchers = await _context.Vouchers!
                .Where(v => Regex.IsMatch(v.VoucherCode!, voucherPattern))
                .ToListAsync();

            return _mapper.Map<List<VoucherModel>>(vouchers);
        }
    }
}
