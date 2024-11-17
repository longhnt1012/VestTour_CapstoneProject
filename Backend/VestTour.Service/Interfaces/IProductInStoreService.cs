using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IProductInStoreService
    {
        Task<ServiceResponse<ProductInStoreModel?>> GetProductInStoreAsync(int storeId, int productId);
        Task<ServiceResponse<List<ProductInStoreModel>>> GetAllProductsInStoreAsync();
        Task<ServiceResponse<int>> AddProductInStoreAsync(ProductInStoreModel productInStore);
        Task<ServiceResponse> UpdateProductInStoreAsync(int storeId, int productId, ProductInStoreModel productInStore);
        Task<ServiceResponse> DeleteProductInStoreAsync(int storeId, int productId);
        Task<ServiceResponse> UpdateQuantityAsync(int storeId, int productId, int quantity);
    }
}
