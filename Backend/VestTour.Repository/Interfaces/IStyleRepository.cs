using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IStyleRepository
    {
        Task<List<StyleModel>> GetAllStylesAsync();            // Get all styles
        Task<StyleModel?> GetStyleByIdAsync(int id);          // Get style by ID
        Task<int> AddStyleAsync(StyleModel style);             // Add a new style
        Task UpdateStyleAsync(int id, StyleModel style);       // Update an existing style
        Task DeleteStyleAsync(int id);                          // Delete a style by ID
    }
}
