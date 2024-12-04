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
        public async Task<StoreModel?> GetStoreByIdAsync(int? storeId)
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
        public async Task<List<UserModel>> GetStaffByStoreIdAsync(int? storeId)
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
        public async Task<bool> AddStaffToStoreAsync(int storeId, int staffId)
        {
            var store = await _context.Stores!.FindAsync(storeId);

            if (store == null)
                return false; 
            var user = await _context.Users!.FindAsync(staffId);
            if (user == null || user.RoleId != 2) 
                return false;

            var allStores = await _context.Stores!.ToListAsync();

            var otherStore = allStores.FirstOrDefault(s =>
                !string.IsNullOrEmpty(s.StaffIDs) &&
                s.StaffIDs.Split(',').Select(int.Parse).Contains(staffId)
            );

            if (otherStore != null)
                return false;
            var staffIds = string.IsNullOrEmpty(store.StaffIDs)
                ? new List<int>()
                : store.StaffIDs.Split(',').Select(int.Parse).ToList();

            if (staffIds.Contains(staffId))
                return false; 
            staffIds.Add(staffId);
            store.StaffIDs = string.Join(',', staffIds);
            _context.Stores!.Update(store);
            await _context.SaveChangesAsync();

            return true;
        }
    
        public async Task<bool> RemoveStaffFromStoreAsync(int storeId, int staffId)
        {
            var store = await _context.Stores!.FirstOrDefaultAsync(s => s.StoreId == storeId);
            if (store == null || string.IsNullOrEmpty(store.StaffIDs))
            {
                return false; 
            }
            var staffIds = store.StaffIDs.Split(',')
                                         .Select(int.Parse)
                                         .ToList();

            if (!staffIds.Contains(staffId))
            {
                return false; 
            }

          
            staffIds.Remove(staffId);
            store.StaffIDs = string.Join(',', staffIds);
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StoreModel?> GetStoreByStaffIdAsync(int staffId)
        {
            if (staffId <= 0)
            {
                return null;
            }

            // Fetch all stores with non-null StaffIDs from the database
            var stores = await _context.Stores
                .Where(store => !string.IsNullOrEmpty(store.StaffIDs))
                .ToListAsync();

            // Process the StaffIDs in memory
            var store = stores
                .FirstOrDefault(store => store.StaffIDs
                    .Split(',')
                    .Select(id => int.Parse(id.Trim()))
                    .Contains(staffId));

            // Map to StoreModel and return
            return _mapper.Map<StoreModel>(store);
        }

        public async Task<(TimeOnly? OpenTime, TimeOnly? CloseTime)> GetStoreTimingsAsync(int storeId)
        {
            var store = await _context.Stores!.FindAsync(storeId);
            if (store == null)
                return (null, null);

            return (store.OpenTime, store.CloseTime);
        }
        public async Task<bool> UpdateStoreImageAsync(int storeId, string imgUrl)
        {
            var store = await _context.Stores!.FindAsync(storeId);

            if (store == null)
                return false;

            store.ImgUrl = imgUrl;
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
