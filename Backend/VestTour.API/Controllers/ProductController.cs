using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using Microsoft.AspNetCore.Authorization;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("basic/{id}")]
        public async Task<ActionResult<ProductModel>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductWithDetails(int id)
        {
            try
            {
                var product = await _productService.GetProductWithDetailsAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product details not found.");
            }
        }

        [HttpGet("code/{productCode}")]
        public async Task<IActionResult> GetProductByCode(string productCode)
        {
            try
            {
                var product = await _productService.GetProductByCodeAsync(productCode);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
                return Ok(products);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("No products found for this category.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct(ProductModel product)
        {
            var productId = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel product)
        {
            try
            {
                await _productService.UpdateProductAsync(id, product);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
        }

        [HttpGet("products/custom-false")]
        public async Task<IActionResult> GetProductsWithIsCustomFalse()
        {
            var products = await _productService.GetProductsWithIsCustomFalseAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound(Error.ProductNotFound);
            }

            return Ok(products);
        }
    }
}
