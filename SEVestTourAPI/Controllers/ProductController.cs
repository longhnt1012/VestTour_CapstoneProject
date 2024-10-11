using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
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
        
    }
}
