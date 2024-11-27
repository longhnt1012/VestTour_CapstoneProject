using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
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
        public async Task<bool> AddStaffToStoreAsync(int storeId, int staffId)
        {
            return await _storeRepository.AddStaffToStoreAsync(storeId, staffId);
        }
        public async Task<ServiceResponse> RemoveStaffFromStoreAsync(int storeId, int staffId)
        {
            var result = await _storeRepository.RemoveStaffFromStoreAsync(storeId, staffId);
            if (!result)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Failed to remove staff. Either the store or the staff does not exist, or the staff is not assigned to this store."
                };
            }

            return new ServiceResponse
            {
                Success = true,
                Message = "Staff removed successfully from the store."
            };
        }
        public async Task<StoreModel?> GetStoreByStaffIdAsync(int staffId)
        {
            return await _storeRepository.GetStoreByStaffIdAsync(staffId);
        }
    }
}
