using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IStyleService
    {
        Task<List<StyleModel>> GetAllStylesAsync();
        Task<StyleModel?> GetStyleByIdAsync(int id);
        Task<int> AddStyleAsync(StyleModel styleModel);
        Task UpdateStyleAsync(int id, StyleModel styleModel);
        Task DeleteStyleAsync(int id);
       
    }
}
