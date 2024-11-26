using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using VestTour.Repository.Models;
namespace VestTour.Repository.Repositories
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
        public async Task<List<UserModel>> GetStaffByStoreIdAsync(int storeId)
        {
            var store = await _context.Stores!.FindAsync(storeId);

            if (store == null || string.IsNullOrEmpty(store.StaffIDs))
                return new List<UserModel>();

            // Parse StaffIDs and fetch related users
            var staffIds = store.StaffIDs.Split(',').Select(int.Parse).ToList();
            var staff = await _context.Users!
                .Where(u => staffIds.Contains(u.UserId))
                .ToListAsync();

            return _mapper.Map<List<UserModel>>(staff);
        }
    }
}
