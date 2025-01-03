using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Repository.Repositories;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;
using VestTour.Services.Interfaces;

namespace VestTour.Services.Implementation
{
    public class LiningService : ILiningService
    {
        private readonly ILiningRepository _liningRepository;

        public LiningService(ILiningRepository liningRepository)
        {
            _liningRepository = liningRepository;
        }

        public async Task<ServiceResponse<List<LiningModel>>> GetAllLiningsAsync()
        {
            var response = new ServiceResponse<List<LiningModel>>();
            try
            {
                response.Data = await _liningRepository.GetAllLiningAsync();
                response.Message = "Linings retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving linings: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<LiningModel>> GetLiningByIdAsync(int id)
        {
            var response = new ServiceResponse<LiningModel>();
            try
            {
                var lining = await _liningRepository.GetLiningByIdAsync(id);
                if (lining == null)
                {
                    response.Success = false;
                    response.Message = $"Lining with id {id} not found.";
                }
                else
                {
                    response.Data = lining;
                    response.Message = "Lining retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error retrieving lining: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<LiningModel>> AddLiningAsync(LiningModel model)
        {
            var response = new ServiceResponse<LiningModel>();
            if (!ItemStatusValidate.IsValidStatus(model.Status))
            {
                response.Success = false;
                response.Message = "Status is not valid. Allowed statuses are: Available, Unavailable.";
                return response;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(model.LiningName))
                    throw new ArgumentException("Lining name cannot be null or empty.");

                var newLiningId = await _liningRepository.AddLiningAsync(model);
                response.Data = await _liningRepository.GetLiningByIdAsync(newLiningId);
                response.Message = "Lining added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error adding lining: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateLiningAsync(int id, LiningModel model)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(model.Status))
            {
                response.Success = false;
                response.Message = "Status is not valid. Allowed statuses are: Available, Unavailable.";
                return response;
            }
            try
            {
                var existingLining = await _liningRepository.GetLiningByIdAsync(id);
                if (existingLining == null)
                    throw new ArgumentException($"Lining with id {id} not found.");

                // Preserve existing values for null fields
                model.LiningName = string.IsNullOrWhiteSpace(model.LiningName) ? existingLining.LiningName : model.LiningName;
                model.ImageUrl = model.ImageUrl ?? existingLining.ImageUrl;

                await _liningRepository.UpdateLiningAsync(id, model);
                response.Message = "Lining updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating lining: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteLiningAsync(int id)
        {
            var response = new ServiceResponse();
            try
            {
                await _liningRepository.DeleteLiningAsync(id);
                response.Message = "Lining deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error deleting lining: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid lining status. Status must be Available or Unavailable";
                return response;
            }
            await _liningRepository.UpdateStatusAsync(itemId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "Lining status updated successfully.";

            return response;
        }
    }
}
