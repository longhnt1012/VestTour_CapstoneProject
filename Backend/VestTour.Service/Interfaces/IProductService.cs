using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<ProductModel?> GetProductByCodeAsync(string productCode);
        Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
        Task<int> AddProductAsync(ProductModel product);
        Task UpdateProductAsync(int id, ProductModel product);
        Task DeleteProductAsync(int id);
        Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync();
        Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId);
        Task AddStyleOptionToProductAsync(int productId, int styleOptionId);
    }

}
