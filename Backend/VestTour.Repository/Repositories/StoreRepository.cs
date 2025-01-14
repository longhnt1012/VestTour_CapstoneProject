﻿using AutoMapper;
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
            var store = await _context.Stores!
                .Where(s => s.StoreId == storeId )  // Adding the condition for "Active" status
                .FirstOrDefaultAsync();

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
        public async Task<StoreModel?> GetStoreByUserIdAsync(int userId)
        {
            if (userId <= 0)
                return null;

            // Fetch the store associated with the given UserId
            var store = await _context.Stores!
                .FirstOrDefaultAsync(s => s.UserId == userId);

            // Map the result to StoreModel and return
            return _mapper.Map<StoreModel>(store);
        }
        public async Task<StoreModel?> GetTailorPartnerByManagerID(int userId)
        {
            if (userId <= 0)
                return null;

            // Fetch the store along with its single TailorPartner entity
            var store = await _context.Stores!
                .Include(s => s.TailorPartner) // Eagerly load the TailorPartner
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (store == null)
                return null;

            // Map the store to StoreModel
            var storeModel = new StoreModel
            {
                StoreId = store.StoreId,
                UserId = store.UserId ?? 0,
                Name = store.Name,
                Address = store.Address,
                ContactNumber = store.ContactNumber,
                StoreCode = store.StoreCode,
                OpenTime = store.OpenTime,
                CloseTime = store.CloseTime,
                StaffIDs = store.StaffIds,
                DistrictID = store.DistrictId,
                ImgUrl = store.ImgUrl,
                Status = store.Status,
                TailorPartner = store.TailorPartner // Single TailorPartner
            };


            return _mapper.Map<StoreModel>(storeModel);
            
            
        }
        public async Task<List<UserModel>> GetStaffByStoreIdAsync(int? storeId)
        {
            var store = await _context.Stores!.FindAsync(storeId);

            if (store == null || string.IsNullOrEmpty(store.StaffIds))
                return new List<UserModel>();

            // Parse StaffIDs and fetch related users
            var staffIds = store.StaffIds.Split(',').Select(int.Parse).ToList();
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
                !string.IsNullOrEmpty(s.StaffIds) &&
                s.StaffIds.Split(',').Select(int.Parse).Contains(staffId)
            );

            if (otherStore != null)
                return false;
            var staffIds = string.IsNullOrEmpty(store.StaffIds)
                ? new List<int>()
                : store.StaffIds.Split(',').Select(int.Parse).ToList();

            if (staffIds.Contains(staffId))
                return false; 
            staffIds.Add(staffId);
            store.StaffIds = string.Join(',', staffIds);
            _context.Stores!.Update(store);
            await _context.SaveChangesAsync();

            return true;
        }
    
        public async Task<bool> RemoveStaffFromStoreAsync(int storeId, int staffId)
        {
            var store = await _context.Stores!.FirstOrDefaultAsync(s => s.StoreId == storeId );
            if (store == null || string.IsNullOrEmpty(store.StaffIds))
            {
                return false; 
            }
            var staffIds = store.StaffIds.Split(',')
                                         .Select(int.Parse)
                                         .ToList();

            if (!staffIds.Contains(staffId))
            {
                return false; 
            }

          
            staffIds.Remove(staffId);
            store.StaffIds = string.Join(',', staffIds);
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
                .Where(store => !string.IsNullOrEmpty(store.StaffIds))
                .ToListAsync();

            // Process the StaffIDs in memory
            var store = stores
                .FirstOrDefault(store => store.StaffIds
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
        public async Task UpdateStatusAsync(int storeId, string newStatus)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store != null)
            {
                store.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
