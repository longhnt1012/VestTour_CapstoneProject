﻿using VestTour.Models;

namespace VestTour.Services
{
    public interface ILiningRepository
    {
        public Task<List<LiningModel>> GetAllLiningAsync();

        public Task<LiningModel> GetLiningByIdAsync(int id);

        public Task<int> AddLiningAsync(LiningModel lining);

        public Task UpdateLiningAsync(int id,LiningModel lining);

        public Task DeleteLiningAsync(int id);
    }
}