using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IStoreRepository
    {
        Task<List<StoreModel>> GetAllStoresAsync();           
        Task<StoreModel?> GetStoreByIdAsync(int storeId);      
        Task<int> AddStoreAsync(StoreModel store);             
        Task UpdateStoreAsync(int id, StoreModel store);      
        Task DeleteStoreAsync(int storeId);
        Task<List<UserModel>> GetStaffByStoreIdAsync(int storeId);
    }
}
