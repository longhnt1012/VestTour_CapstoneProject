using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
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
            // Logic: nếu không tìm thấy sản phẩm, có thể ném ngoại lệ hoặc trả về một giá trị mặc định
            return product;
        }

        public async Task<ProductModel?> GetProductByCodeAsync(string productCode)
        {
            var product = await _productRepository.GetProductByCodeAsync(productCode);
            return product ?? throw new KeyNotFoundException("Product not found."); // Ném ngoại lệ nếu không tìm thấy
        }

        public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            // Logic: có thể ném ngoại lệ nếu không có sản phẩm nào được tìm thấy
            if (products == null || products.Count == 0)
                throw new KeyNotFoundException("No products found for this category.");

            return products;
        }

        public async Task<int> AddProductAsync(ProductModel product)
        {
            // Logic: kiểm tra hợp lệ hoặc xử lý bổ sung trước khi thêm sản phẩm
            if (string.IsNullOrEmpty(product.ProductCode))
                throw new ArgumentException("Product code cannot be empty.");

            return await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(int id, ProductModel product)
        {
            // Logic: kiểm tra xem sản phẩm có tồn tại không trước khi cập nhật
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found.");

            await _productRepository.UpdateProductAsync(id, product);
        }

        public async Task DeleteProductAsync(int id)
        {
            // Logic: kiểm tra xem sản phẩm có tồn tại không trước khi xóa
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found.");

            await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync()
        {
            return await _productRepository.GetProductsWithIsCustomFalseAsync();
        }

        public async Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId)
        {
            var productDetails = await _productRepository.GetProductWithDetailsAsync(productId);
            if (productDetails == null)
                throw new KeyNotFoundException("Product details not found.");

            return productDetails;
        }
        //public async Task AddStyleOptionToProductAsync(int productId, int styleOptionId)
        //{
        //    await _productRepository.AddStyleOptionToProductAsync(productId, styleOptionId);
        //}
    }
}
