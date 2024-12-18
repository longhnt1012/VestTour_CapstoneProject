using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Services.Interfaces
{
    public interface ILiningService
    {
        Task<ServiceResponse<List<LiningModel>>> GetAllLiningsAsync();
        Task<ServiceResponse<LiningModel>> GetLiningByIdAsync(int id);
        Task<ServiceResponse<LiningModel>> AddLiningAsync(LiningModel model);
        Task<ServiceResponse> UpdateLiningAsync(int id, LiningModel model);
        Task<ServiceResponse> DeleteLiningAsync(int id);
    }
}
