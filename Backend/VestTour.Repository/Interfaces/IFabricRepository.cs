using VestTour.Domain.Enums;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IFabricRepository
    {
        public Task<List<FabricModel>> GetAllFabricsAsync();
        public Task<FabricModel> GetFabricByIdAsync(int fabricId);
        Task<string?> GetFabricNameByIdAsync(int fabricId);
        public Task<int> AddFabricAsync(FabricModel model);

        public Task UpdateFabricAsync(int id, FabricModel model);
        Task<decimal?> GetFabricPriceByIdAsync(int fabricId);
        public Task DeleteFabricAsync(int fabricId);
        Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag);
        Task UpdateStatusAsync(int itemId, string newStatus);


    }
}
