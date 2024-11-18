using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
       // private readonly IProductStyleOptionRepository _productStyleOptionRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
           // _productStyleOptionRepository = productStyleOptionRepository;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product;
        }

        public async Task<ProductModel?> GetProductByCodeAsync(string productCode)
        {
            var product = await _productRepository.GetProductByCodeAsync(productCode);
            return product ?? throw new KeyNotFoundException("Product not found."); 
        }

        public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
           
            if (products == null || products.Count == 0)
                throw new KeyNotFoundException("No products found for this category.");

            return products;
        }

        public async Task<int> AddProductAsync(ProductModel product)
        {
            if (string.IsNullOrEmpty(product.ProductCode))
                throw new ArgumentException("Product code cannot be empty.");

            return await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(int id, ProductModel product)
        {
           
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found.");

            await _productRepository.UpdateProductAsync(id, product);
        }

        public async Task DeleteProductAsync(int id)
        {
            
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found.");

            await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync()
        {
            return await _productRepository.GetProductsWithIsCustomFalseAsync();
        }
        public async Task<List<ProductQuantityModel>> GetProductsWithIsCustomFalseInStoreAsync(int storeId)
        {
            return await _productRepository.GetProductsWithIsCustomFalseInStoreAsync(storeId);
        }
        public async Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId)
        {
            var productDetails = await _productRepository.GetProductWithDetailsAsync(productId);
            if (productDetails == null)
                throw new KeyNotFoundException("Product details not found.");

            return productDetails;
        }
       

    }
}
