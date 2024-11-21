using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using VestTour.Repository.Constants;
using VestTour.Service.Interface;

namespace VestTour.Service.Implementation
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementService(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public async Task<ServiceResponse<List<MeasurementModel>>> GetAllMeasurementsAsync()
        {
            var response = new ServiceResponse<List<MeasurementModel>>();

            try
            {
                var measurements = await _measurementRepository.GetAllMeasurementsAsync();

                if (measurements.Count == 0)
                {
                    response.Success = false;
                    response.Message = Error.MeasurementNotFound;
                }
                else
                {
                    response.Data = measurements;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<MeasurementModel?>> GetMeasurementByIdAsync(int id)
        {
            var response = new ServiceResponse<MeasurementModel?>();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidMeasurementId;
                return response;
            }

            try
            {
                var measurement = await _measurementRepository.GetMeasurementByIdAsync(id);

                if (measurement == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.MeasurementNotFound}: {id}";
                }
                else
                {
                    response.Data = measurement;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddMeasurementAsync(MeasurementModel measurementModel)
        {
            var response = new ServiceResponse<int>();

            if (measurementModel == null)
            {
                response.Success = false;
                response.Message = Error.InvalidModelState;
                return response;
            }

            try
            {
                // Kiểm tra Measurement theo UserId
                var existingMeasurement = await _measurementRepository.GetMeasurementByUserIdAsync(measurementModel.UserId);

                if (existingMeasurement != null)
                {
                    // Nếu tồn tại, thực hiện cập nhật
                    await _measurementRepository.UpdateMeasurementAsync(existingMeasurement.MeasurementId, measurementModel);
                    response.Data = existingMeasurement.MeasurementId;
                    response.Message = Success.MeasurementUpdated;
                }
                else
                {
                    // Nếu không tồn tại, thêm mới
                    var newMeasurementId = await _measurementRepository.AddMeasurementAsync(measurementModel);
                    response.Data = newMeasurementId;
                    response.Message = Success.MeasurementAdded;
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse> UpdateMeasurementAsync(int id, MeasurementModel measurementModel)
        {
            var response = new ServiceResponse();

            if (id <= 0 || measurementModel == null)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                var existingMeasurement = await _measurementRepository.GetMeasurementByIdAsync(id);
                if (existingMeasurement == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.MeasurementNotFound}: {id}";
                }
                else
                {
                    await _measurementRepository.UpdateMeasurementAsync(id, measurementModel);
                    response.Message = Success.MeasurementUpdated;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteMeasurementAsync(int id)
        {
            var response = new ServiceResponse();

            if (id <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidMeasurementId;
                return response;
            }

            try
            {
                var existingMeasurement = await _measurementRepository.GetMeasurementByIdAsync(id);
                if (existingMeasurement == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.MeasurementNotFound}: {id}";
                }
                else
                {
                    await _measurementRepository.DeleteMeasurementAsync(id);
                    response.Message = Success.MeasurementDeleted;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<MeasurementModel?>> GetMeasurementByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<MeasurementModel?>();

            if (userId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidUserId;
                return response;
            }

            try
            {
                var measurement = await _measurementRepository.GetMeasurementByUserIdAsync(userId);

                if (measurement == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.MeasurementNotFound}: {userId}";
                }
                else
                {
                    response.Data = measurement;
                    response.Success = true;
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
