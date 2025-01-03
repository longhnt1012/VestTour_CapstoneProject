using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<ProductModel?> GetProductByCodeAsync(string productCode);
        Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
        Task<ServiceResponse> UpdateProductAsync(int id, ProductModel product);
        Task<ServiceResponse> UpdateStatusAsync(int productId, string newStatus);
        Task<ServiceResponse<int>> AddProductAsync(ProductModel product);
        Task DeleteProductAsync(int id);
        Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync();
        Task<List<ProductQuantityModel>> GetProductsWithIsCustomFalseInStoreAsync(int storeId);
        Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId);
        Task<List<ProductModel>> GetProductsByCategoryIdIsCustomFalseAsync(int categoryId);
    }

}
