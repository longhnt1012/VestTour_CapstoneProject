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

            // Kiểm tra StageName
            if (!StageNameValidate.IsValidStage(processingTailor.StageName))
            {
                response.Success = false;
                response.Message = Error.InvalidStageName;
                return response;
            }

           
            if (!StageStatusValidate.IsValidStageStatus(processingTailor.Status))
            {
                response.Success = false;
                response.Message = Error.InvalidProcessStatus;
                return response;
            }

           
            if (!StageStatusValidate.IsValidStageStatus(processingTailor.SampleStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(processingTailor.FixStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(processingTailor.DeliveryStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
;
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
                response.Message = Error.InvalidStageName; 
                return response;
            }


            if (!StageStatusValidate.IsValidStageStatus(processingTailor.SampleStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(processingTailor.FixStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                ;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(processingTailor.DeliveryStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                ;
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
        public async Task<ServiceResponse<List<ProcessingTailorModel>>> GetProcessAssignedByTailorPartnerIdAsync(int tailorPartnerId)
        {
            var response = new ServiceResponse<List<ProcessingTailorModel>>();

            if (tailorPartnerId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidTailorPartnerId; 
                return response;
            }

            try
            {
                var processes = await _processingTailorRepository.GetProcessAssignedByTailorPartnerIdAsync(tailorPartnerId);
                response.Data = processes;
                response.Success = processes.Any();
                response.Message = processes.Any() ? null : Error.NoProcessesAssigned;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ChangeStatusAsync(int processingId, string newStatus)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            if (!TailorProcessStatusValidate.IsValidProcessStatus(newStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidProcessStatus;
                return response;
            }

            try
            {
                var existingProcessingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);

                if (existingProcessingTailor == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProcessingTailorNotFound}: {processingId}";
                    return response;
                }
                await _processingTailorRepository.ChangeStatusAsync(processingId, newStatus);

                response.Success = true;
                response.Message = Success.ProcessingTailorStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ChangeSampleStatusAsync(int processingId, string newStatus)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(newStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                return response;
            }

            try
            {
                var existingProcessingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);

                if (existingProcessingTailor == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProcessingTailorNotFound}: {processingId}";
                    return response;
                }
                await _processingTailorRepository.ChangeSampleStatusAsync(processingId, newStatus);

                response.Success = true;
                response.Message = Success.SampleStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ChangeFixStatusAsync(int processingId, string newStatus)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(newStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                return response;
            }

            try
            {
                var existingProcessingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);

                if (existingProcessingTailor == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProcessingTailorNotFound}: {processingId}";
                    return response;
                }
                await _processingTailorRepository.ChangeFixStatusAsync(processingId, newStatus);

                response.Success = true;
                response.Message = Success.FixStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ChangeDeliveryStatusAsync(int processingId, string newStatus)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            if (!StageStatusValidate.IsValidStageStatus(newStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidStageStatus;
                return response;
            }

            try
            {
                var existingProcessingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);

                if (existingProcessingTailor == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProcessingTailorNotFound}: {processingId}";
                    return response;
                }
                await _processingTailorRepository.ChangeDeliveryStatusAsync(processingId, newStatus);

                response.Success = true;
                response.Message = Success.DeliveryStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ChangeStageNameAsync(int processingId, string newStage)
        {
            var response = new ServiceResponse();

            if (processingId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidProcessingTailorId;
                return response;
            }

            if (!StageNameValidate.IsValidStage(newStage))
            {
                response.Success = false;
                response.Message = Error.InvalidStageName;
                return response;
            }

            try
            {
                var existingProcessingTailor = await _processingTailorRepository.GetProcessingTailorByIdAsync(processingId);

                if (existingProcessingTailor == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProcessingTailorNotFound}: {processingId}";
                    return response;
                }
                await _processingTailorRepository.ChangeStageNameAsync(processingId, newStage);

                response.Success = true;
                response.Message = Success.StageNameUpdated;
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
