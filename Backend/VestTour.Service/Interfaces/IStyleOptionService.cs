using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IStyleOptionService
    {
        Task<List<StyleOptionModel>> GetAllStyleOptionsAsync();
        Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id);
        Task<int> AddStyleOptionAsync(StyleOptionModel styleOptionModel);
        Task UpdateStyleOptionAsync(int id, StyleOptionModel styleOptionModel);
        Task DeleteStyleOptionAsync(int id);
    }
}
