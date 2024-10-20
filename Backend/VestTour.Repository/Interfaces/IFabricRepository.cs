﻿using VestTour.Domain.Enums;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IFabricRepository
    {
        public Task<List<FabricModel>> GetAllFabricsAsync();
        public Task<FabricModel> GetFabricByIdAsync(int fabricId);

        public Task<int> AddFabricAsync(FabricModel model);

        public Task UpdateFabricAsync(int id, FabricModel model);

        public Task DeleteFabricAsync(int fabricId);
       // Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag);


    }
}
