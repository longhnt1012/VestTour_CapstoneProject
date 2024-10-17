using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IStoreRepository
    {
        Task<List<StoreModel>> GetAllStoresAsync();            // Get all stores
        Task<StoreModel?> GetStoreByIdAsync(int storeId);       // Get store by ID
        Task<int> AddStoreAsync(StoreModel store);             // Add a new store
        Task UpdateStoreAsync(int id, StoreModel store);       // Update an existing store
        Task DeleteStoreAsync(int storeId);                    // Delete a store by ID
    }
}
