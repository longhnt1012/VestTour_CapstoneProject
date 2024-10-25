using VestTour.Domain.Enums;
using VestTour.Repository.Models;

namespace VestTour.Service.Interface
{
    public interface IFabricService
    {
        Task<List<FabricModel>> GetAllFabricsAsync();
        Task<FabricModel> GetFabricByIdAsync(int fabricId);
        Task<int> AddFabricAsync(FabricModel model);
        Task UpdateFabricAsync(int id, FabricModel model);
        Task DeleteFabricAsync(int fabricId);
        //Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag);
    }
}
