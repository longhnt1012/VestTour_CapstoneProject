using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Enums;
using VestTour.Repository.Models;
using VestTour.Service;

namespace VestTour.Service.Interface
{
    public interface IFabricService
    {
        Task<ServiceResponse<List<FabricModel>>> GetAllFabricsAsync();
        Task<ServiceResponse<FabricModel>> GetFabricByIdAsync(int fabricId);
        Task<ServiceResponse<int>> AddFabricAsync(FabricModel model);
        Task<ServiceResponse> UpdateFabricAsync(int id, FabricModel model);
        Task<ServiceResponse> DeleteFabricAsync(int fabricId);
        Task<ServiceResponse<List<FabricModel>>> GetFabricByTagAsync(FabricEnums? tag);
    }
}
