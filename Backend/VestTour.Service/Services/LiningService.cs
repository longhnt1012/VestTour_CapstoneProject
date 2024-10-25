using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Services.Interfaces;

namespace VestTour.Services.Implementation
{
    public class LiningService : ILiningService
    {
        private readonly ILiningRepository _liningRepository;

        public LiningService(ILiningRepository liningRepository)
        {
            _liningRepository = liningRepository;
        }

        public async Task<List<LiningModel>> GetAllLiningsAsync()
        {
            return await _liningRepository.GetAllLiningAsync();
        }

        public async Task<LiningModel> GetLiningByIdAsync(int id)
        {
            return await _liningRepository.GetLiningByIdAsync(id);
        }

        public async Task<LiningModel> AddLiningAsync(LiningModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LiningName))
                throw new ArgumentException("Lining name cannot be null or empty.");

            var newLiningId = await _liningRepository.AddLiningAsync(model);
            return await _liningRepository.GetLiningByIdAsync(newLiningId);
        }

        public async Task UpdateLiningAsync(int id, LiningModel model)
        {
            var existingLining = await _liningRepository.GetLiningByIdAsync(id);
            if (existingLining == null)
                throw new ArgumentException($"Lining with id {id} not found.");

            // Preserve existing values for null fields
            model.LiningName = string.IsNullOrWhiteSpace(model.LiningName) ? existingLining.LiningName : model.LiningName;
            model.ImageUrl = model.ImageUrl ?? existingLining.ImageUrl;

            await _liningRepository.UpdateLiningAsync(id, model);
        }

        public async Task DeleteLiningAsync(int id)
        {
            await _liningRepository.DeleteLiningAsync(id);
        }
    }
}
