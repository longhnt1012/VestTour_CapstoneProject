using VestTour.Repository.Interface;
using VestTour.Service.Interface;
using VestTour.Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        // Get all vouchers
        public async Task<ServiceResponse<List<VoucherModel>>> GetAllVouchersAsync()
        {
            var response = new ServiceResponse<List<VoucherModel>>();
            try
            {
                var vouchers = await _voucherRepository.GetAllVouchersAsync();
                response.Data = vouchers;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving vouchers: {ex.Message}";
            }
            return response;
        }

        // Get voucher by ID
        public async Task<ServiceResponse<VoucherModel?>> GetVoucherByIdAsync(int voucherId)
        {
            var response = new ServiceResponse<VoucherModel?>();
            try
            {
                var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
                if (voucher == null)
                {
                    response.Success = false;
                    response.Message = "Voucher not found.";
                }
                else
                {
                    response.Data = voucher;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving the voucher: {ex.Message}";
            }
            return response;
        }

        // Add a new voucher
        public async Task<ServiceResponse<int>> AddVoucherAsync(VoucherModel voucherModel)
        {
            var response = new ServiceResponse<int>();
            try
            {
                var voucherId = await _voucherRepository.AddVoucherAsync(voucherModel);
                response.Data = voucherId;
                response.Message = "Voucher added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while adding the voucher: {ex.Message}";
            }
            return response;
        }

        // Update an existing voucher
        public async Task<ServiceResponse> UpdateVoucherAsync(int voucherId, UpdateVoucherModel updateModel)
        {
            var response = new ServiceResponse();
            try
            {
                await _voucherRepository.UpdateVoucherAsync(voucherId, updateModel);
                response.Message = "Voucher updated successfully.";
            }
            catch (KeyNotFoundException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while updating the voucher: {ex.Message}";
            }
            return response;
        }

        // Delete a voucher
        public async Task<ServiceResponse> DeleteVoucherAsync(int voucherId)
        {
            var response = new ServiceResponse();
            try
            {
                await _voucherRepository.DeleteVoucherAsync(voucherId);
                response.Message = "Voucher deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while deleting the voucher: {ex.Message}";
            }
            return response;
        }

        // Get vouchers by code
        public async Task<ServiceResponse<List<VoucherModel>>> GetVouchersByCodeAsync(string code)
        {
            var response = new ServiceResponse<List<VoucherModel>>();
            try
            {
                var vouchers = await _voucherRepository.GetVouchersByCodeAsync(code);
                response.Data = vouchers;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving vouchers by code: {ex.Message}";
            }
            return response;
        }

        // Get valid vouchers
        public async Task<ServiceResponse<List<VoucherModel>>> GetValidVouchersAsync()
        {
            var response = new ServiceResponse<List<VoucherModel>>();
            try
            {
                var validVouchers = await _voucherRepository.GetValidVouchersAsync();
                response.Data = validVouchers;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving valid vouchers: {ex.Message}";
            }
            return response;
        }

        // Get voucher by code
        public async Task<ServiceResponse<VoucherModel?>> GetVoucherByCodeAsync(string code)
        {
            var response = new ServiceResponse<VoucherModel?>();
            try
            {
                var voucher = await _voucherRepository.GetVoucherByCodeAsync(code);
                if (voucher == null)
                {
                    response.Success = false;
                    response.Message = "Voucher not found.";
                }
                else
                {
                    response.Data = voucher;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving the voucher by code: {ex.Message}";
            }
            return response;
        }
    }
}
