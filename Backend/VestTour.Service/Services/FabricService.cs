using VestTour.Domain.Enums;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Implementation
{
    public class FabricService : IFabricService
    {
        private readonly IFabricRepository _fabricRepo;

        public FabricService(IFabricRepository fabricRepo)
        {
            _fabricRepo = fabricRepo;
        }

        public async Task<List<FabricModel>> GetAllFabricsAsync()
        {
            return await _fabricRepo.GetAllFabricsAsync();
        }

        public async Task<FabricModel> GetFabricByIdAsync(int fabricId)
        {
            return await _fabricRepo.GetFabricByIdAsync(fabricId);
        }

        public async Task<int> AddFabricAsync(FabricModel model)
        {
            return await _fabricRepo.AddFabricAsync(model);
        }

        public async Task UpdateFabricAsync(int id, FabricModel model)
        {
            await _fabricRepo.UpdateFabricAsync(id, model);
        }

        public async Task DeleteFabricAsync(int fabricId)
        {
            await _fabricRepo.DeleteFabricAsync(fabricId);
        }

        public async Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag)
        {
            return await _fabricRepo.GetFabricByTagAsync(tag);
        }

    }
}
