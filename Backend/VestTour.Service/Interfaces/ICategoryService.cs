using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interface
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryModel?>> GetCategoryByIdAsync(int categoryId);
        Task<ServiceResponse<List<CategoryModel>>> GetAllCategoriesAsync();
        Task<ServiceResponse<int>> AddCategoryAsync(CategoryModel category);
        Task<ServiceResponse> UpdateCategoryAsync(int id, CategoryModel category);
        Task<ServiceResponse> DeleteCategoryAsync(int categoryId);
        Task<ServiceResponse<List<CategoryModel>>> GetCategoriesByParentIdAsync(int parentId);
        Task<ServiceResponse> UpdateStatusAsync(int productId, string newStatus);
    }
}
