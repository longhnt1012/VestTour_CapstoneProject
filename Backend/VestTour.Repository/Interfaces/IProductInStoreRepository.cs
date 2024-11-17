using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IProductInStoreRepository
    {
        Task<ProductInStoreModel?> GetProductInStoreAsync(int storeId, int productId);
        Task<List<ProductInStoreModel>> GetAllProductsInStoreAsync();
        Task<int> AddProductInStoreAsync(ProductInStoreModel productInStore);
        Task UpdateProductInStoreAsync(int storeId, int productId, ProductInStoreModel productInStore);
        Task DeleteProductInStoreAsync(int storeId, int productId);
        Task UpdateQuantityAsync(int storeId, int productId, int quantity);
    }
}
