using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Repository.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public InventoryRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddInventoryAsync(InventoryModel inventory)
        {
            // Map InventoryModel to ProductInventory
            var productInventory = _mapper.Map<ProductInventory>(inventory);

            await _context.ProductInventories.AddAsync(productInventory);
            await _context.SaveChangesAsync();

            return productInventory.ProductId;
        }

        public async Task DeleteInventoryAsync(int productId)
        {
            var productInventory = await _context.ProductInventories.FindAsync(productId);
            if (productInventory != null)
            {
                _context.ProductInventories.Remove(productInventory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<InventoryModel>> GetAllInventoriesAsync()
        {
            var inventories = await _context.ProductInventories.ToListAsync();
            return _mapper.Map<List<InventoryModel>>(inventories);
        }

        public async Task<InventoryModel?> GetInventoryByIdAsync(int productId)
        {
            var productInventory = await _context.ProductInventories
                                                 .FirstOrDefaultAsync(pi => pi.ProductId == productId);
            return _mapper.Map<InventoryModel?>(productInventory);
        }

        public async Task UpdateInventoryAsync(int productId, InventoryModel inventory)
        {
            var existingInventory = await _context.ProductInventories.FindAsync(productId);
            if (existingInventory != null)
            {
                // Update properties using the values from the InventoryModel
                existingInventory.Quantity = inventory.Quantity;
                existingInventory.LastUpdate = DateTime.UtcNow;

                _context.ProductInventories.Update(existingInventory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
