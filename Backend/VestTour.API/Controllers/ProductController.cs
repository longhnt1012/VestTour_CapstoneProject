using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
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
        [HttpGet("category-iscustomfalse/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryIdIsCustomFalse(int categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryIdIsCustomFalseAsync(categoryId);
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
            if (products == null)
            {
                return NotFound(Error.ProductNotFound);
            }

            return Ok(products);
        }

        [HttpGet("products/custom-false/instoreid")]
        public async Task<IActionResult> GetProductsWithIsCustomFalseInStoreAsync(int storeId)
        {
            var products = await _productService.GetProductsWithIsCustomFalseInStoreAsync(storeId);
            if (products == null)
            {
                return NotFound(Error.ProductNotFound);
            }

            return Ok(products);
        }
        [HttpPut("{productId}/update-status")]
        public async Task<IActionResult> UpdateProductStatus(int productId, [FromBody] string status)
        {
            var result = await _productService.UpdateStatusAsync(productId, status);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(new { Message = result.Message });
        }

    }
}
