using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/IStoreRepository.cs
namespace SEVestTourAPI.Services
========
namespace VestTour.Repository.Interface
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Interfaces/IStoreRepository.cs
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
