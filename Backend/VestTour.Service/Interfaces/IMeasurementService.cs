using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IMeasurementService
    {
        Task<ServiceResponse<MeasurementModel?>> GetMeasurementByUserIdAsync(int userId);
        Task<ServiceResponse<List<MeasurementModel>>> GetAllMeasurementsAsync();
        Task<ServiceResponse<MeasurementModel?>> GetMeasurementByIdAsync(int measurementId);
        Task<ServiceResponse<int>> AddMeasurementAsync(MeasurementModel measurementModel);
        Task<ServiceResponse> UpdateMeasurementAsync(int id, MeasurementModel measurementModel);
        Task<ServiceResponse> DeleteMeasurementAsync(int measurementId);
        decimal CalculateMeasurementSurcharge(MeasurementModel measurement);
    }
}
