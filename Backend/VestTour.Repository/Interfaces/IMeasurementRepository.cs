using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IMeasurementRepository
    {
        Task<MeasurementModel?> GetMeasurementByUserIdAsync(int userId);
        Task<MeasurementModel?> GetMeasurementByIdAsync(int measurementId);   // Get measurement by ID
        Task<List<MeasurementModel>> GetAllMeasurementsAsync();                // Get all measurements
        Task<int> AddMeasurementAsync(MeasurementModel measurement);           // Add a new measurement
        Task UpdateMeasurementAsync(int id, MeasurementModel measurement);     // Update a measurement
        Task DeleteMeasurementAsync(int measurementId);                        // Delete a measurement
    }
}
