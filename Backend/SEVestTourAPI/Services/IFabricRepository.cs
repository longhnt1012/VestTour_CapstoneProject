using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public interface IFabricRepository
    {
        public Task<List<FabricModel>> GetAllFabricsAsync();
        public Task<FabricModel> GetFabricModelByIdAsync(int fabricId);

        public Task<int> AddFabricAsync(FabricModel model);

        public Task UpdateFabricAsync(int id,  FabricModel model);

        public Task DeleteFabricAsync(int fabricId);
        Task<List<FabricModel>> GetFabricByTagAsync(string tag);


    }
}
