using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IInventoryService
    {
        Task<ServiceResponse<int>> AddInventoryAsync(InventoryModel inventory);
        Task<ServiceResponse> DeleteInventoryAsync(int productId);
        Task<ServiceResponse> UpdateInventoryAsync(int productId, InventoryModel inventory);
        Task<ServiceResponse<InventoryModel?>> GetInventoryByIdAsync(int productId);
        Task<ServiceResponse<List<InventoryModel>>> GetAllInventoriesAsync();
    }
}
