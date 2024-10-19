using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCartController : ControllerBase
    {
        private readonly IAddCartService _addCartService;

        public AddCartController(IAddCartService addCartService)
        {
            _addCartService = addCartService;
        }

        [HttpGet("mycart")]
        public async Task<ActionResult<List<CartItemModel>>> ViewUserCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var cartItems = await _addCartService.GetUserCartAsync(userId);
            return Ok(cartItems);
        }

        [HttpPost("addtocart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.AddToCartAsync(userId, productId);
            return Ok("Product added to cart.");
        }

        [HttpDelete("removefromcart/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.RemoveFromCartAsync(userId, productId);
            return Ok("Product removed from cart.");
        }

        [HttpPost("increase/{productId}")]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.IncreaseQuantityAsync(userId, productId);
            return Ok("Product quantity increased.");
        }

        [HttpPost("decrease/{productId}")]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.DecreaseQuantityAsync(userId, productId);
            return Ok("Product quantity decreased.");
        }
    }
}
