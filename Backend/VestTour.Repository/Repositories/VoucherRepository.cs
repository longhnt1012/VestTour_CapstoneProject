using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Models;
namespace VestTour.Repository.Implementation
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
        public async Task UpdateVoucherAsync(int id, UpdateVoucherModel voucherUpdateModel)
        {
            var existingVoucher = await _context.Vouchers!.FindAsync(id);
            if (existingVoucher == null)
            {
                throw new KeyNotFoundException($"Voucher with ID {id} not found.");
            }

            //// Check for uniqueness of VoucherCode, if it's being updated
            //if (!string.IsNullOrEmpty(voucherUpdateModel.VoucherCode) &&
            //    voucherUpdateModel.VoucherCode != existingVoucher.VoucherCode)
            //{
            //    var duplicate = await _context.Vouchers
            //        .AnyAsync(v => v.VoucherCode == voucherUpdateModel.VoucherCode && v.VoucherId != id);
            //    if (duplicate)
            //    {
            //        throw new ArgumentException("VoucherCode must be unique.");
            //    }
            //}

            // Map properties from UpdateVoucherModel to the existing voucher entity
            existingVoucher.Status = voucherUpdateModel.Status ?? existingVoucher.Status;
            existingVoucher.VoucherCode = voucherUpdateModel.VoucherCode ?? existingVoucher.VoucherCode;
            existingVoucher.Description = voucherUpdateModel.Description ?? existingVoucher.Description;
            existingVoucher.DiscountNumber = voucherUpdateModel.DiscountNumber ?? existingVoucher.DiscountNumber;
            existingVoucher.DateStart = voucherUpdateModel.DateStart ?? existingVoucher.DateStart;
            existingVoucher.DateEnd = voucherUpdateModel.DateEnd ?? existingVoucher.DateEnd;

            await _context.SaveChangesAsync();
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
        // VoucherRepository
        public async Task<List<VoucherModel>> GetVouchersByCodeAsync(string code)
        {
            // Sử dụng Contains để tìm kiếm các voucher có chứa mã nhập vào
            var vouchers = await _context.Vouchers!
                .Where(v => v.VoucherCode!.Contains(code)) // Thay đổi ở đây
                .ToListAsync();

            return _mapper.Map<List<VoucherModel>>(vouchers);
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

        // Get voucher by code
        public async Task<VoucherModel?> GetVoucherByCodeAsync(string code)
        {
            // Sử dụng Contains để tìm kiếm voucher có chứa mã voucher
            var voucher = await _context.Vouchers!
                .Where(v => v.VoucherCode!.Contains(code))
                .FirstOrDefaultAsync();

            return _mapper.Map<VoucherModel>(voucher);
        }




    }
}
