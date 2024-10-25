using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<List<VoucherModel>> GetAllVouchersAsync()
        {
            return await _voucherRepository.GetAllVouchersAsync();
        }

        public async Task<VoucherModel?> GetVoucherByIdAsync(int id)
        {
            return await _voucherRepository.GetVoucherByIdAsync(id);
        }

        public async Task<List<VoucherModel>> GetValidVouchersAsync()
        {
            return await _voucherRepository.GetValidVouchersAsync();
        }

       

        public async Task<int> CreateVoucherAsync(VoucherModel voucherModel)
        {
            return await _voucherRepository.AddVoucherAsync(voucherModel);
        }

        public async Task UpdateVoucherAsync(int id, UpdateVoucherModel updateVoucherModel)
        {
            await _voucherRepository.UpdateVoucherAsync(id, updateVoucherModel);
        }


        public async Task DeleteVoucherAsync(int id)
        {
            await _voucherRepository.DeleteVoucherAsync(id);
        }

        public async Task<VoucherModel?> GetVoucherByCodeAsync(string code)
        {
            return await _voucherRepository.GetVoucherByCodeAsync(code);
        }
    }
}
