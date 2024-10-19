using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/IStyleOptionRepository.cs
namespace SEVestTourAPI.Services
========
namespace VestTour.Repository.Interface
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Interfaces/IStyleOptionRepository.cs
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
