using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
using VestTour.Repository.ValidationHelper;
using VestTour.Repository.Repositories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResponse<CategoryModel?>> GetCategoryByIdAsync(int categoryId)
    {
        var response = new ServiceResponse<CategoryModel?>();

        // Example validation
        if (categoryId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidCategoryId;
            return response;
        }

        try
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                response.Success = false;
                response.Message = $"{Error.CategoryNotFound}: {categoryId}"; 
            }
            else
            {
                response.Data = category;
                response.Success = true; 
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<ServiceResponse<List<CategoryModel>>> GetAllCategoriesAsync()
    {
        var response = new ServiceResponse<List<CategoryModel>>();

        try
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            if (!categories.Any())
            {
                response.Success = false;
                response.Message = Error.NoCategoriesFound;
            }
            else
            {
                response.Data = categories;
                response.Success = true;
                
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}"; 
        }

        return response;
    }

    public async Task<ServiceResponse<int>> AddCategoryAsync(CategoryModel category)
    {
        var response = new ServiceResponse<int>();

        
        if (string.IsNullOrEmpty(category.Name))
        {
            response.Success = false;
            response.Message = Error.InvalidCategoryName; 
            return response;
        }
        if (!ItemStatusValidate.IsValidStatus(category.Status))
        {
            response.Success = false;
            response.Message = "Invalid category status. Allowed status: Available / Unavailable";
            return response;
        }
        try
        {
            var newCategoryId = await _categoryRepository.AddCategoryAsync(category);
            response.Data = newCategoryId;
            response.Message = Success.CategoryAdded;
            response.Success = true; 
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<ServiceResponse> UpdateCategoryAsync(int id, CategoryModel category)
    {
        var response = new ServiceResponse();

       
        if (id <= 0 || string.IsNullOrEmpty(category.Name))
        {
            response.Success = false;
            response.Message = Error.InvalidInputData;
            return response;
        }
        if (!ItemStatusValidate.IsValidStatus(category.Status))
        {
            response.Success = false;
            response.Message = "Invalid category status. Allowed status: Available / Unavailable";
            return response;
        }
        try
        {
            await _categoryRepository.UpdateCategoryAsync(id, category);
            response.Message = Success.CategoryUpdated;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<ServiceResponse> DeleteCategoryAsync(int categoryId)
    {
        var response = new ServiceResponse();

        if (categoryId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidCategoryId;
            return response;
        }

        try
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
            response.Message = Success.CategoryDeleted;
            response.Success = true; 
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<ServiceResponse<List<CategoryModel>>> GetCategoriesByParentIdAsync(int parentId)
    {
        var response = new ServiceResponse<List<CategoryModel>>();

        if (parentId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidParentCategoryId;
            return response;
        }

        try
        {
            var categories = await _categoryRepository.GetCategoriesByParentIdAsync(parentId);
            if (!categories.Any())
            {
                response.Success = false;
                response.Message = Error.NoCategoriesFound; 
            }
            else
            {
                response.Data = categories;
                response.Success = true; 
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"An error occurred: {ex.Message}"; 
        }

        return response;
    }
    public async Task<ServiceResponse> UpdateStatusAsync(int productId, string newStatus)
    {
        var response = new ServiceResponse();
        if (!ItemStatusValidate.IsValidStatus(newStatus))
        {
            response.Success = false;
            response.Message = "Invalid category status. Allowed status: Available / Unavailable";
            return response;
        }
        await _categoryRepository.UpdateStatusAsync(productId, newStatus);

        // Return success response
        response.Success = true;
        response.Message = "Category status updated successfully.";

        return response;
    }
}
