using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<List<InventoryModel>> GetAllInventoriesAsync();           
        Task<InventoryModel?> GetInventoryByIdAsync(int productId);          
        Task<int> AddInventoryAsync(InventoryModel inventory);             
        Task UpdateInventoryAsync(int productId, InventoryModel inventory);    
        Task DeleteInventoryAsync(int productId);
    }

}
