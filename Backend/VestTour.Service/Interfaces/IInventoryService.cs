using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IInventoryService
    {
        Task<int> AddInventoryAsync(InventoryModel inventory);
        Task DeleteInventoryAsync(int productId);
        Task UpdateInventoryAsync(int productId, InventoryModel inventory);
        Task<InventoryModel?> GetInventoryByIdAsync(int productId);
        Task<List<InventoryModel>> GetAllInventoriesAsync();
    }

}
