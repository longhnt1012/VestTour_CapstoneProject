using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Enums;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IFabricService
    {
        Task<List<FabricModel>> GetAllFabricsAsync();
        Task<FabricModel> GetFabricByIdAsync(int id);
        Task<int> AddNewFabricAsync(FabricModel model);
        Task UpdateFabricAsync(int id, FabricModel model);
        Task DeleteFabricAsync(int id);
        Task<List<FabricModel>> GetFabricsByTagAsync(FabricEnums tag);
        Task<List<FabricModel>> GetFabricsByDescriptionAsync(string description);
    }
}
