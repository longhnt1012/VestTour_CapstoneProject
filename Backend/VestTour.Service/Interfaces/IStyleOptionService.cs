using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IStyleOptionService
    {
        Task<List<StyleOptionModel>> GetAllStyleOptionsAsync();
        Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id);
        Task<ServiceResponse<int>> AddStyleOptionAsync(StyleOptionModel styleOptionModel);
        Task<ServiceResponse> UpdateStyleOptionAsync(int id, StyleOptionModel styleOptionModel);
        Task DeleteStyleOptionAsync(int id);
        Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus);
    }
}
