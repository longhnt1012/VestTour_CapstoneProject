using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementService(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public async Task<List<MeasurementModel>> GetAllMeasurementsAsync()
        {
            return await _measurementRepository.GetAllMeasurementsAsync();
        }

        public async Task<MeasurementModel?> GetMeasurementByIdAsync(int id)
        {
            var measurement = await _measurementRepository.GetMeasurementByIdAsync(id);
            if (measurement == null)
                throw new KeyNotFoundException("Measurement not found.");

            return measurement;
        }

        public async Task<int> AddMeasurementAsync(MeasurementModel measurementModel)
        {
            if (measurementModel == null)
                throw new ArgumentNullException(nameof(measurementModel), "Measurement cannot be null.");

            return await _measurementRepository.AddMeasurementAsync(measurementModel);
        }

        public async Task UpdateMeasurementAsync(int id, MeasurementModel measurementModel)
        {
            var existingMeasurement = await _measurementRepository.GetMeasurementByIdAsync(id);
            if (existingMeasurement == null)
                throw new KeyNotFoundException("Measurement not found.");

            await _measurementRepository.UpdateMeasurementAsync(id, measurementModel);
        }

        public async Task DeleteMeasurementAsync(int id)
        {
            var existingMeasurement = await _measurementRepository.GetMeasurementByIdAsync(id);
            if (existingMeasurement == null)
                throw new KeyNotFoundException("Measurement not found.");

            await _measurementRepository.DeleteMeasurementAsync(id);
        }
        public async Task<MeasurementModel?> GetMeasurementByUserIdAsync(int userId)
        {
            var measurement = await _measurementRepository.GetMeasurementByUserIdAsync(userId);
            if (measurement == null)
                throw new KeyNotFoundException("Measurement not found for this user.");

            return measurement;
        }

    }
}
