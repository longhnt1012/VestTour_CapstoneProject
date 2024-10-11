using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProductsAsync();             
        Task<ProductModel?> GetProductByIdAsync(int productId);      
        Task<int> AddProductAsync(ProductModel product);            
        Task UpdateProductAsync(int id, ProductModel product);       
        Task DeleteProductAsync(int productId);
        Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId);
    }
}
