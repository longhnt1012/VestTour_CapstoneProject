using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IStyleService
    {
        Task<List<StyleModel>> GetAllStylesAsync();
        Task<StyleModel?> GetStyleByIdAsync(int id);
        Task<ServiceResponse<int>> AddStyleAsync(StyleModel styleModel);
        Task<ServiceResponse> UpdateStyleAsync(int id, StyleModel styleModel);
        Task DeleteStyleAsync(int id);
        Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus);


    }
}
