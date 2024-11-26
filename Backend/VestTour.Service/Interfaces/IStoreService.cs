using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IStoreService
    {
        Task<List<StoreModel>> GetAllStoresAsync();
        Task<StoreModel?> GetStoreByIdAsync(int id);
        Task<int> CreateStoreAsync(StoreModel storeModel);
        Task UpdateStoreAsync(int id, StoreModel storeModel);
        Task DeleteStoreAsync(int id);
        Task<List<UserModel>> GetStaffByStoreIdAsync(int storeId);
    }
}
