using VestTour.Models;

namespace VestTour.Services
{
    public interface IMeasurementRepository
    {
        Task<MeasurementModel> GetMeasurementByIdAsync(int measurementId);

        // Retrieve all measurements for a specific user
        Task<IEnumerable<MeasurementModel>> GetMeasurementsByUserIdAsync(int userId);

        // Add a new measurement
        Task<MeasurementModel> AddMeasurementAsync(MeasurementModel measurement);

        // Update an existing measurement
        Task<bool> UpdateMeasurementAsync(MeasurementModel measurement);

        // Delete a measurement by its ID
        Task<bool> DeleteMeasurementAsync(int measurementId);


    }
}
