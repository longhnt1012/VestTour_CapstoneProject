using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class TailorPartnerService : ITailorPartnerService
    {
        private readonly ITailorPartnerRepository _tailorPartnerRepository;

        public TailorPartnerService(ITailorPartnerRepository tailorPartnerRepository)
        {
            _tailorPartnerRepository = tailorPartnerRepository;
        }

        public async Task<ServiceResponse<TailorPartnerModel?>> GetTailorPartnerByIdAsync(int tailorPartnerId)
        {
            var response = new ServiceResponse<TailorPartnerModel?>();

            if (tailorPartnerId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidTailorPartnerId;
                return response;
            }

            try
            {
                var tailorPartner = await _tailorPartnerRepository.GetTailorPartnerByIdAsync(tailorPartnerId);
                response.Data = tailorPartner;
                response.Success = tailorPartner != null;
                response.Message = tailorPartner != null ? null : Error.TailorPartnerNotFound;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<TailorPartnerModel>>> GetAllTailorPartnersAsync()
        {
            var response = new ServiceResponse<List<TailorPartnerModel>>();

            try
            {
                response.Data = await _tailorPartnerRepository.GetAllTailorPartnersAsync();
                response.Success = response.Data.Any();
                response.Message = response.Data.Any() ? null : Error.NoTailorPartnersFound;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddTailorPartnerAsync(TailorPartnerModel tailorPartner)
        {
            var response = new ServiceResponse<int>();

            try
            {
                response.Data = await _tailorPartnerRepository.AddTailorPartnerAsync(tailorPartner);
                response.Success = true;
                response.Message = Success.TailorPartnerAdded;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateTailorPartnerAsync(int id, TailorPartnerModel tailorPartner)
        {
            var response = new ServiceResponse();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidTailorPartnerId;
                return response;
            }

            try
            {
                await _tailorPartnerRepository.UpdateTailorPartnerAsync(id, tailorPartner);
                response.Success = true;
                response.Message = Success.TailorPartnerUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteTailorPartnerAsync(int tailorPartnerId)
        {
            var response = new ServiceResponse();

            if (tailorPartnerId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidTailorPartnerId;
                return response;
            }

            try
            {
                await _tailorPartnerRepository.DeleteTailorPartnerAsync(tailorPartnerId);
                response.Success = true;
                response.Message = Success.TailorPartnerDeleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<TailorPartnerModel>>> GetTailorPartnersByStoreIdAsync(int storeId)
        {
            var response = new ServiceResponse<List<TailorPartnerModel>>();

            if (storeId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidStoreId;
                return response;
            }

            try
            {
                var tailorPartners = await _tailorPartnerRepository.GetTailorPartnersByStoreIdAsync(storeId);

                response.Data = tailorPartners;
                response.Success = tailorPartners.Any();
                response.Message = tailorPartners.Any() ? null : Error.TailorPartnerNotFound;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse<TailorPartnerModel>> GetTailorPartnersByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<TailorPartnerModel>();

            if (userId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidUserId;
                return response;
            }

            try
            {
                var tailorPartners = await _tailorPartnerRepository.GetTailorPartnerByUserIdAsync(userId);

                if (tailorPartners != null)
                {
                    response.Data = tailorPartners;
                    response.Success = true;
                    response.Message = "Tailor partner retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No tailor partner found for the specified user.";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
