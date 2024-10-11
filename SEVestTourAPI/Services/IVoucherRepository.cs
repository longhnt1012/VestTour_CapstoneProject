using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface IVoucherRepository
    {
        Task<List<VoucherModel>> GetAllVouchersAsync();             // Get all vouchers
        Task<VoucherModel?> GetVoucherByIdAsync(int voucherId);      // Get a voucher by ID
        Task<int> AddVoucherAsync(VoucherModel voucher);             // Add a new voucher
        Task UpdateVoucherAsync(int id, VoucherModel voucher);       // Update a voucher
        Task DeleteVoucherAsync(int voucherId);                      // Delete a voucher
        Task<VoucherModel?> GetVoucherByCodeAsync(string code);      // Get a voucher by code
    }
}
