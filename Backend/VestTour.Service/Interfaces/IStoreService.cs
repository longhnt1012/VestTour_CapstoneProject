using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

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
        Task<bool> AddStaffToStoreAsync(int storeId, int staffId);
        Task<ServiceResponse> RemoveStaffFromStoreAsync(int storeId, int staffId);
        Task<StoreModel?> GetStoreByStaffIdAsync(int staffId);
        List<string> GetTimeSlots(TimeOnly openTime, TimeOnly closeTime);
        Task<(TimeOnly? OpenTime, TimeOnly? CloseTime)> GetStoreTimingsAsync(int storeId);
        Task<List<string>> GetStoreTimeSlotsAsync(int storeId);
        Task<bool> UpdateStoreImageAsync(int storeId, string imgUrl);
        Task<StoreModel?> GetStoreByUserId(int userId);
    }
}
