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
        Task<List<InventoryModel>> GetAllInventoriesAsync();            // Get all styles
        Task<InventoryModel?> GetInventoryByIdAsync(int productId);          // Get style by ID
        Task<int> AddInventoryAsync(InventoryModel inventory);             // Add a new style
        Task UpdateInventoryAsync(int productId, InventoryModel inventory);       // Update an existing style
        Task DeleteInventoryAsync(int productId);
    }

}
