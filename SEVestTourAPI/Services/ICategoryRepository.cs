using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetCategoryByIdAsync(int categoryId);  // Get category by ID
        Task<List<CategoryModel>> GetAllCategoriesAsync();          // Get all categories
        Task<int> AddCategoryAsync(CategoryModel category);         // Add a new category
        Task UpdateCategoryAsync(int id, CategoryModel category);   // Update a category
        Task DeleteCategoryAsync(int categoryId);                   // Delete a category
        

        public Task<List<CategoryModel>> GetAllParentID(int categoryId);
    }
}
