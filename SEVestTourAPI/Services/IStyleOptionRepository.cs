using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface IStyleOptionRepository
    {
        Task<List<StyleOptionModel>> GetAllStyleOptionsAsync();      // Get all style options
        Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id);    // Get style option by ID
        Task<int> AddStyleOptionAsync(StyleOptionModel styleOption); // Add a new style option
        Task UpdateStyleOptionAsync(int id, StyleOptionModel styleOption); // Update an existing style option
        Task DeleteStyleOptionAsync(int id);                          // Delete a style option by ID
    }
}
