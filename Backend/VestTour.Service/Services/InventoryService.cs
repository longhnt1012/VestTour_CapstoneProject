using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Domain.Entities;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
      //  private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
          
        }

        public async Task<int> AddInventoryAsync(InventoryModel inventory)
        {
            // Delegate to the repository layer
            return await _inventoryRepository.AddInventoryAsync(inventory);
        }

        public async Task DeleteInventoryAsync(int productId)
        {
            // Delegate to the repository layer
            await _inventoryRepository.DeleteInventoryAsync(productId);
        }

        public async Task<List<InventoryModel>> GetAllInventoriesAsync()
        {
            // Delegate to the repository layer
            return await _inventoryRepository.GetAllInventoriesAsync();
        }

        public async Task<InventoryModel?> GetInventoryByIdAsync(int productId)
        {
            // Delegate to the repository layer
            return await _inventoryRepository.GetInventoryByIdAsync(productId);
        }

        public async Task UpdateInventoryAsync(int productId, InventoryModel inventory)
        {
            // Validate that the inventory object exists
            if (inventory == null)
            {
                throw new ArgumentNullException(nameof(inventory), "Inventory cannot be null");
            }

            // Delegate to the repository layer
            await _inventoryRepository.UpdateInventoryAsync(productId, inventory);
        }
    }
}
