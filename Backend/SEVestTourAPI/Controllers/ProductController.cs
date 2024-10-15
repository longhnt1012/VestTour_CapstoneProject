using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("basic/{id}")]
        public async Task<ActionResult<ProductModel>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductWithDetails(int id)
        {
            var product = await _productRepository.GetProductWithDetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("code/{productCode}")]
        public async Task<IActionResult> GetProductByCode(string productCode)
        {
            var product = await _productRepository.GetProductByCodeAsync(productCode);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found for this category.");
            }
            return Ok(products);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> AddProduct(ProductModel product)
        {
            var productId = await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel product)
        {
            await _productRepository.UpdateProductAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
        [HttpGet("products/custom-false")]
        public async Task<IActionResult> GetProductsWithIsCustomFalse()
        {
            var products = await _productRepository.GetProductsWithIsCustomFalseAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found with IsCustom = false.");
            }

            return Ok(products);
        }

    }
}
