using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetCategoryByIdAsync(int categoryId);  
        Task<List<CategoryModel>> GetAllCategoriesAsync();          
        Task<int> AddCategoryAsync(CategoryModel category);         
        Task UpdateCategoryAsync(int id, CategoryModel category);  
        Task DeleteCategoryAsync(int categoryId);
        Task<List<CategoryModel>> GetCategoriesByParentIdAsync(int parentId);
    }
}
