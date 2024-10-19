using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IMeasurementService
    {
        Task<List<MeasurementModel>> GetAllMeasurementsAsync();
        Task<MeasurementModel?> GetMeasurementByIdAsync(int measurementId);
        Task<int> AddMeasurementAsync(MeasurementModel measurementModel);
        Task UpdateMeasurementAsync(int id, MeasurementModel measurementModel);
        Task DeleteMeasurementAsync(int measurementId);
    }
}
