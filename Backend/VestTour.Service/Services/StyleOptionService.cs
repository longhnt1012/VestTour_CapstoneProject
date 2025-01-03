using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Repositories;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;

namespace VestTour.Service.Implementation
{
    public class StyleOptionService : IStyleOptionService
    {
        private readonly IStyleOptionRepository _styleOptionRepository;

        public StyleOptionService(IStyleOptionRepository styleOptionRepository)
        {
            _styleOptionRepository = styleOptionRepository;
        }

        public async Task<List<StyleOptionModel>> GetAllStyleOptionsAsync()
        {
            return await _styleOptionRepository.GetAllStyleOptionsAsync();
        }

        public async Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id)
        {
            return await _styleOptionRepository.GetStyleOptionByIdAsync(id);
        }

        public async Task<ServiceResponse<int>> AddStyleOptionAsync(StyleOptionModel styleOptionModel)
        {
            var response = new ServiceResponse<int>();

            // Validate style option status
            if (!ItemStatusValidate.IsValidStatus(styleOptionModel.Status))
            {
                response.Success = false;
                response.Message = "Invalid lining status. Status must be Available or Unavailable.";
                return response;
            }

            try
            {
                // Add the style option
                var styleOptionId = await _styleOptionRepository.AddStyleOptionAsync(styleOptionModel);

                response.Success = true;
                response.Message = "Style option added successfully.";
                response.Data = styleOptionId; // Return the style option ID
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while adding the style option: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> UpdateStyleOptionAsync(int id, StyleOptionModel styleOptionModel)
        {
            var response = new ServiceResponse();

            // Validate style option status
            if (!ItemStatusValidate.IsValidStatus(styleOptionModel.Status))
            {
                response.Success = false;
                response.Message = "Invalid lining status. Status must be Available or Unavailable.";
                return response;
            }

            try
            {
                // Update the style option
                await _styleOptionRepository.UpdateStyleOptionAsync(id, styleOptionModel);

                response.Success = true;
                response.Message = "Style option updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while updating the style option: {ex.Message}";
            }

            return response;
        }

        public async Task DeleteStyleOptionAsync(int id)
        {
            await _styleOptionRepository.DeleteStyleOptionAsync(id);
        }
        public async Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid styleOption status. Status must be Available or Unavailable";
                return response;
            }
            await _styleOptionRepository.UpdateStatusAsync(itemId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "StyleOption status updated successfully.";

            return response;
        }
    }
}
