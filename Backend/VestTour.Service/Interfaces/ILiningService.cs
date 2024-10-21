using VestTour.Repository.Models;

namespace VestTour.Services.Interfaces
{
    public interface ILiningService
    {
        Task<List<LiningModel>> GetAllLiningsAsync();
        Task<LiningModel> GetLiningByIdAsync(int id);
        Task<LiningModel> AddLiningAsync(LiningModel model);
        Task UpdateLiningAsync(int id, LiningModel model);
        Task DeleteLiningAsync(int id);
    }
}
