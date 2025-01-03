using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetCategoryByIdAsync(int categoryId);
        Task<List<CategoryModel>> GetAllCategoriesAsync();
        Task<int> AddCategoryAsync(CategoryModel category);
        Task UpdateCategoryAsync(int id, CategoryModel category);
        Task DeleteCategoryAsync(int categoryId);
        Task<List<CategoryModel>> GetCategoriesByParentIdAsync(int parentId);
        Task UpdateStatusAsync(int itemId, string newStatus);
    }
}
