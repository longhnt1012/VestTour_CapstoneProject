using VestTour.Models;

namespace VestTour.Services
{
    public interface IFabricRepository
    {
       public Task<FabricModel?> GetFabricByIdAsync(int fabricId);      // Get fabric by ID
       public Task<List<FabricModel>> GetAllFabricsAsync();      // Get all fabrics
       public Task<int> AddFabricAsync(FabricModel fabric);                  // Add new fabric
       public Task UpdateFabricAsync(int id, FabricModel fabric);               // Update fabric
       public Task DeleteFabricAsync(int fabricId);

    }
}
