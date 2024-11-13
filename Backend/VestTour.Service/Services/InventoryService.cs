using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ServiceResponse<int>> AddInventoryAsync(InventoryModel inventory)
        {
            var response = new ServiceResponse<int>();

            try
            {
                var newInventoryId = await _inventoryRepository.AddInventoryAsync(inventory);
                response.Data = newInventoryId;
                response.Success = true;
                response.Message = "Inventory added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteInventoryAsync(int productId)
        {
            var response = new ServiceResponse();

            try
            {
                await _inventoryRepository.DeleteInventoryAsync(productId);
                response.Success = true;
                response.Message = "Inventory deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<InventoryModel>>> GetAllInventoriesAsync()
        {
            var response = new ServiceResponse<List<InventoryModel>>();

            try
            {
                var inventories = await _inventoryRepository.GetAllInventoriesAsync();
                response.Data = inventories;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<InventoryModel?>> GetInventoryByIdAsync(int productId)
        {
            var response = new ServiceResponse<InventoryModel?>();

            try
            {
                var inventory = await _inventoryRepository.GetInventoryByIdAsync(productId);
                if (inventory == null)
                {
                    response.Success = false;
                    response.Message = "Inventory not found.";
                }
                else
                {
                    response.Data = inventory;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateInventoryAsync(int productId, InventoryModel inventory)
        {
            var response = new ServiceResponse();

            try
            {
                if (inventory == null)
                {
                    response.Success = false;
                    response.Message = "Inventory cannot be null.";
                    return response;
                }

                await _inventoryRepository.UpdateInventoryAsync(productId, inventory);
                response.Success = true;
                response.Message = "Inventory updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
