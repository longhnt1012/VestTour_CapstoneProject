using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Repository.Implementation
{
    public class StoreRepository : IStoreRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public StoreRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all stores
        public async Task<List<StoreModel>> GetAllStoresAsync()
        {
            var stores = await _context.Stores!.ToListAsync();
            return _mapper.Map<List<StoreModel>>(stores);
        }

        // Get store by ID
        public async Task<StoreModel?> GetStoreByIdAsync(int storeId)
        {
            var store = await _context.Stores!.FindAsync(storeId);
            return _mapper.Map<StoreModel>(store);
        }

        // Add a new store
        public async Task<int> AddStoreAsync(StoreModel storeModel)
        {
            var newStore = _mapper.Map<Store>(storeModel);
            _context.Stores!.Add(newStore);
            await _context.SaveChangesAsync();
            return newStore.StoreId;
        }

        // Update an existing store
        public async Task UpdateStoreAsync(int id, StoreModel storeModel)
        {
            if (id == storeModel.StoreId)
            {
                var updatedStore = _mapper.Map<Store>(storeModel);
                _context.Stores!.Update(updatedStore);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a store by ID
        public async Task DeleteStoreAsync(int storeId)
        {
            var store = await _context.Stores!.FindAsync(storeId);
            if (store != null)
            {
                _context.Stores!.Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}
