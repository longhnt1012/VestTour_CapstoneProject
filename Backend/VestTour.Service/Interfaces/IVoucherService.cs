using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IVoucherService
    {
        Task<List<VoucherModel>> GetAllVouchersAsync();
        Task<VoucherModel?> GetVoucherByIdAsync(int id);
        Task<List<VoucherModel>> GetValidVouchersAsync();
       // Task<List<VoucherModel>> GetVouchersByPatternAsync();
        Task<int> CreateVoucherAsync(VoucherModel voucherModel);
        Task UpdateVoucherAsync(int id, VoucherModel voucherModel);
        Task DeleteVoucherAsync(int id);
        Task<VoucherModel?> GetVoucherByCodeAsync(string code);
    }
}
