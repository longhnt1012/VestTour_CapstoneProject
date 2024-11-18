using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class ProcessingTailorService : IProcessingTailorService
    {
        private readonly IProcessingTailorRepository _processingTailorRepository;

        public ProcessingTailorService(IProcessingTailorRepository processingTailorRepository)
        {
            _processingTailorRepository = processingTailorRepository;
        }

        public async Task<ServiceResponse<ProcessingTailorModel?>> GetProcessingTailorByIdAsync(int processingId)
        {
            var response = new ServiceResponse<ProcessingTailorModel?>();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            try
            {
                var processingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);
                response.Data = processingTailor;
                response.Success = processingTailor != null;
                response.Message = processingTailor != null ? null : Error.ProcessingTailorNotFound;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProcessingTailorModel>>> GetAllProcessingTailorsAsync()
        {
            var response = new ServiceResponse<List<ProcessingTailorModel>>();

            try
            {
                response.Data = await _processingTailorRepository.GetAllProcessingTailorsAsync();
                response.Success = response.Data.Any();
                response.Message = response.Data.Any() ? null : Error.NoProcessingTailorsFound;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddProcessingTailorAsync(ProcessingTailorModel processingTailor)
        {
            var response = new ServiceResponse<int>();
            if (!StageNameValidate.IsValidStage(processingTailor.StageName))
            {
                response.Success = false;
                response.Message = Error.InvalidStageName; 
                return response;
            }

            try
            {
                response.Data = await _processingTailorRepository.AddProcessingTailorAsync(processingTailor);
                response.Success = true;
                response.Message = Success.ProcessingTailorAdded;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateProcessingTailorAsync(int id, ProcessingTailorModel processingTailor)
        {
            var response = new ServiceResponse();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }
            if (!StageNameValidate.IsValidStage(processingTailor.StageName))
            {
                response.Success = false;
                response.Message = Error.InvalidStageName; // Assume this constant exists in your error messages
                return response;
            }

            try
            {
                await _processingTailorRepository.UpdateProcessingTailorAsync(id, processingTailor);
                response.Success = true;
                response.Message = Success.ProcessingTailorUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteProcessingTailorAsync(int processingId)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            try
            {
                await _processingTailorRepository.DeleteProcessingTailorAsync(processingId);
                response.Success = true;
                response.Message = Success.ProcessingTailorDeleted;
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
