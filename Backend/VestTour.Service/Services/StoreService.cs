using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Repository.Repositories;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<List<StoreModel>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllStoresAsync();
        }

        public async Task<StoreModel?> GetStoreByIdAsync(int id)
        {
            return await _storeRepository.GetStoreByIdAsync(id);
        }

        public async Task<ServiceResponse<int>> CreateStoreAsync(StoreModel storeModel)
        {
            var response = new ServiceResponse<int>();

            try
            {
                // Validate store status
                if (!StatusValidate.IsValidStatus(storeModel.Status))
                {
                    response.Success = false;
                    response.Message = "Invalid status. Status must be Active or Deactive.";
                    return response;
                }

                // Add the store
                var storeId = await _storeRepository.AddStoreAsync(storeModel);

                response.Success = true;
                response.Message = "Store created successfully.";
                response.Data = storeId; // Return the store ID
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while creating the store: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateStoreAsync(int id, StoreModel storeModel)
        {
            var response = new ServiceResponse();

            try
            {
                // Validate store status
                if (!StatusValidate.IsValidStatus(storeModel.Status))
                {
                    response.Success = false;
                    response.Message = "Invalid status. Status must be Active or Deactive.";
                    return response;
                }

                // Update the store
                await _storeRepository.UpdateStoreAsync(id, storeModel);

                response.Success = true;
                response.Message = "Store updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while updating the store: {ex.Message}";
            }

            return response;
        }


        public async Task DeleteStoreAsync(int id)
        {
            await _storeRepository.DeleteStoreAsync(id);
        }
        public async Task<List<UserModel>> GetStaffByStoreIdAsync(int storeId)
        {
            return await _storeRepository.GetStaffByStoreIdAsync(storeId);
        }
        public async Task<bool> AddStaffToStoreAsync(int storeId, int staffId)
        {
            return await _storeRepository.AddStaffToStoreAsync(storeId, staffId);
        }
        public async Task<ServiceResponse> RemoveStaffFromStoreAsync(int storeId, int staffId)
        {
            var result = await _storeRepository.RemoveStaffFromStoreAsync(storeId, staffId);
            if (!result)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to remove staff. Either the store or the staff does not exist, or the staff is not assigned to this store."
                };
            }

            return new ServiceResponse
            {
                Success = true,
                Message = "Staff removed successfully from the store."
            };
        }
        public async Task<StoreModel?> GetStoreByStaffIdAsync(int staffId)
        {
            return await _storeRepository.GetStoreByStaffIdAsync(staffId);
        }
        public async Task<StoreModel?> GetStoreByUserId(int userId)
        {
            return await _storeRepository.GetStoreByUserIdAsync(userId);
        }
        public async Task<(TimeOnly? OpenTime, TimeOnly? CloseTime)> GetStoreTimingsAsync(int storeId)
        {
            return await _storeRepository.GetStoreTimingsAsync(storeId);
        }

        public List<string> GetTimeSlots(TimeOnly openTime, TimeOnly closeTime)
        {
            var timeSlots = new List<string>();
            var currentTime = openTime;

            while (currentTime.AddMinutes(30) <= closeTime)
            {
                var endTime = currentTime.AddMinutes(30);
                timeSlots.Add($"{currentTime:HH:mm} - {endTime:HH:mm}");
                currentTime = endTime;
            }

            return timeSlots;
        }
        public async Task<List<string>> GetStoreTimeSlotsAsync(int storeId)
        {
            var (openTime, closeTime) = await GetStoreTimingsAsync(storeId);

            if (openTime == null || closeTime == null)
                return new List<string> { "Store timings are not available." };

            return GetTimeSlots(openTime.Value, closeTime.Value);
        }
        public async Task<bool> UpdateStoreImageAsync(int storeId, string imgUrl)
        {
            return await _storeRepository.UpdateStoreImageAsync(storeId, imgUrl);
        }
        public async Task<ServiceResponse> UpdateStatusAsync(int storeId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!StatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid status. Status must be Active or Deactive";
                return response;
            }
            await _storeRepository.UpdateStatusAsync(storeId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "store status updated successfully.";

            return response;
        }
    }
}
