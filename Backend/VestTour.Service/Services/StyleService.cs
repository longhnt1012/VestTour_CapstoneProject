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
    public class StyleService : IStyleService
    {
        private readonly IStyleRepository _styleRepository;

        public StyleService(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        public async Task<List<StyleModel>> GetAllStylesAsync()
        {
            return await _styleRepository.GetAllStylesAsync();
        }

        public async Task<StyleModel?> GetStyleByIdAsync(int id)
        {
            return await _styleRepository.GetStyleByIdAsync(id);
        }
        public async Task<ServiceResponse<int>> AddStyleAsync(StyleModel styleModel)
        {
            var response = new ServiceResponse<int>();

            // Validate style status
            if (!ItemStatusValidate.IsValidStatus(styleModel.Status))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable.";
                return response;
            }

            try
            {
                // Add the style
                var styleId = await _styleRepository.AddStyleAsync(styleModel);

                response.Success = true;
                response.Message = "Style added successfully.";
                response.Data = styleId; // Return the style ID
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while adding the style: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateStyleAsync(int id, StyleModel styleModel)
        {
            var response = new ServiceResponse();

            // Validate style status
            if (!ItemStatusValidate.IsValidStatus(styleModel.Status))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable.";
                return response;
            }

            try
            {
                // Update the style
                await _styleRepository.UpdateStyleAsync(id, styleModel);

                response.Success = true;
                response.Message = "Style updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while updating the style: {ex.Message}";
            }

            return response;
        }


        public async Task DeleteStyleAsync(int id)
        {
            await _styleRepository.DeleteStyleAsync(id);
        }
        public async Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable";
                return response;
            }
            await _styleRepository.UpdateStatusAsync(itemId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "Style status updated successfully.";

            return response;
        }
    }
}
