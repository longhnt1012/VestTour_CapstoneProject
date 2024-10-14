using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface IVoucherRepository
    {
        Task<List<VoucherModel>> GetAllVouchersAsync();             
        Task<VoucherModel?> GetVoucherByIdAsync(int voucherId);      
        Task<int> AddVoucherAsync(VoucherModel voucher);            
        Task UpdateVoucherAsync(int id, VoucherModel voucher);       
        Task DeleteVoucherAsync(int voucherId);                     
        Task<VoucherModel?> GetVoucherByCodeAsync(string code);     
        Task<List<VoucherModel>> GetValidVouchersAsync();            // Get valid vouchers (DateStart <= Now <= DateEnd)
        Task<List<VoucherModel>> GetVouchersByPatternAsync();
    }
}
