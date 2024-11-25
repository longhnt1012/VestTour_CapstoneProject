using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IVoucherRepository
    {
        Task<List<VoucherModel>> GetAllVouchersAsync();
        Task<VoucherModel?> GetVoucherByIdAsync(int voucherId);
        Task<int> AddVoucherAsync(VoucherModel voucher);
        Task UpdateVoucherAsync(int id, UpdateVoucherModel voucherUpdateModel);
        Task DeleteVoucherAsync(int voucherId);
        Task<VoucherModel?> GetVoucherByCodeAsync(string code);
        Task<List<VoucherModel>> GetValidVouchersAsync();
        Task ChangeVoucherStatusAsync(int voucherId, string newStatus);

        Task<List<VoucherModel>> GetVouchersByCodeAsync(string code);
    }
}
