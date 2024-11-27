using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IVoucherService
    {
        Task<ServiceResponse<List<VoucherModel>>> GetAllVouchersAsync();
        Task<ServiceResponse<VoucherModel?>> GetVoucherByIdAsync(int voucherId);
        Task<ServiceResponse<int>> AddVoucherAsync(VoucherModel voucherModel);
        Task<ServiceResponse> UpdateVoucherAsync(int voucherId, UpdateVoucherModel updateModel);
        Task<ServiceResponse> DeleteVoucherAsync(int voucherId);
        Task<ServiceResponse<List<VoucherModel>>> GetVouchersByCodeAsync(string code);
        Task<ServiceResponse<List<VoucherModel>>> GetValidVouchersAsync();
        Task<ServiceResponse<VoucherModel?>> GetVoucherByCodeAsync(string code);
        Task<ServiceResponse> ChangeVoucherStatusAsync(int voucherId, string newStatus);
       
    }
}
