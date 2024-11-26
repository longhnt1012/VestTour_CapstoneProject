using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<List<StoreModel>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllStoresAsync();
        }

        public async Task<StoreModel?> GetStoreByIdAsync(int id)
        {
            return await _storeRepository.GetStoreByIdAsync(id);
        }

        public async Task<int> CreateStoreAsync(StoreModel storeModel)
        {
            return await _storeRepository.AddStoreAsync(storeModel);
        }

        public async Task UpdateStoreAsync(int id, StoreModel storeModel)
        {
            await _storeRepository.UpdateStoreAsync(id, storeModel);
        }

        public async Task DeleteStoreAsync(int id)
        {
            await _storeRepository.DeleteStoreAsync(id);
        }
        public async Task<List<UserModel>> GetStaffByStoreIdAsync(int storeId)
        {
            return await _storeRepository.GetStaffByStoreIdAsync(storeId);
        }
       
    }
}
