using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IStyleOptionRepository
    {
        Task<decimal?> GetStyleOptionPriceByIdAsync(int styleOptionId);
        Task<List<StyleOptionModel>> GetAllStyleOptionsAsync();      // Get all style options
        Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id);    // Get style option by ID
        Task<int> AddStyleOptionAsync(StyleOptionModel styleOption); // Add a new style option
        Task UpdateStyleOptionAsync(int id, StyleOptionModel styleOption); // Update an existing style option
        Task DeleteStyleOptionAsync(int id);                          // Delete a style option by ID
    }
}
