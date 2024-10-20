using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Enums;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Services
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

        public async Task<FabricModel> GetFabricByIdAsync(int id)
        {
            var fabric = await _fabricRepo.GetFabricModelByIdAsync(id);
            return fabric ?? throw new KeyNotFoundException("Fabric not found");
        }

        public async Task<int> AddNewFabricAsync(FabricModel model)
        {
            return await _fabricRepo.AddFabricAsync(model);
        }

        public async Task UpdateFabricAsync(int id, FabricModel model)
        {
            if (id != model.FabricId)
            {
                throw new ArgumentException("Fabric ID mismatch");
            }
            await _fabricRepo.UpdateFabricAsync(id, model);
        }

        public async Task DeleteFabricAsync(int id)
        {
            await _fabricRepo.DeleteFabricAsync(id);
        }

        public async Task<List<FabricModel>> GetFabricsByTagAsync(FabricEnums? tag)
        {
            var fabrics = await _fabricRepo.GetFabricByTagAsync(tag);
            return fabrics.Count > 0 ? fabrics : throw new KeyNotFoundException("No fabrics found for the specified tag");
        }
    }
}
