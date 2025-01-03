using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
     
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
          
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
        public async Task<List<ProductModel>> GetProductsByCategoryIdIsCustomFalseAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdIsCustomFalseAsync(categoryId);

            if (products == null || products.Count == 0)
                throw new KeyNotFoundException("No products found for this category.");

            return products;
        }
        public async Task<ServiceResponse<int>> AddProductAsync(ProductModel product)
        {
            var response = new ServiceResponse<int>();

            // Validate product code
            if (string.IsNullOrEmpty(product.ProductCode))
            {
                response.Success = false;
                response.Message = "Product code cannot be empty.";
                return response;
            }
            if (!ItemStatusValidate.IsValidStatus(product.Status))
            {
                response.Success = false;
                response.Message = "Invalid product status.";
                return response;
            }
            // Add the product
            var productId = await _productRepository.AddProductAsync(product);

            response.Success = true;
            response.Message = "Product added successfully.";
            response.Data = productId; // Return the product ID

            return response;
        }


        public async Task<ServiceResponse> UpdateProductAsync(int id, ProductModel product)
        {
            var response = new ServiceResponse();

            // Validate product status
            if (!ItemStatusValidate.IsValidStatus(product.Status))
            {
                response.Success = false;
                response.Message = "Invalid product status.";
                return response;
            }

            // Check if the product exists
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                response.Success = false;
                response.Message = "Product not found.";
                return response;
            }

            // Update the product
            await _productRepository.UpdateProductAsync(id, product);

            response.Success = true;
            response.Message = "Product updated successfully.";
            return response;
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

        public async Task<ServiceResponse> UpdateStatusAsync(int productId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid product status.";
                return response;
            }
            await _productRepository.UpdateStatusAsync(productId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "Product status updated successfully.";

            return response;
        }

    }
}
