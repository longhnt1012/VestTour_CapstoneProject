using VestTour.Domain.Enums;
using VestTour.Repository.Models;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/IFabricRepository.cs
namespace SEVestTourAPI.Services
========
namespace VestTour.Repository.Interface
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Interfaces/IFabricRepository.cs
{
    public interface IFabricRepository
    {
        public Task<List<FabricModel>> GetAllFabricsAsync();
        public Task<FabricModel> GetFabricModelByIdAsync(int fabricId);

        public Task<int> AddFabricAsync(FabricModel model);

        public Task UpdateFabricAsync(int id,  FabricModel model);

        public Task DeleteFabricAsync(int fabricId);
        Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag);


    }
}
