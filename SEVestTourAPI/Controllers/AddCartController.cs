using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCartController : ControllerBase
    {
        private readonly IAddCartRepository _addCartRepository;

        public AddCartController(IAddCartRepository addCartRepository)
        {
            _addCartRepository = addCartRepository;
        }

        [HttpGet("mycart")]
        public async Task<ActionResult<List<CartItemModel>>> ViewUserCart()
        {
            
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));

            var cartItems = await _addCartRepository.GetUserCartAsync(userId);

            return Ok(cartItems);
        }

       [HttpPost("addtocart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartRepository.AddToCartAsync(userId, productId);
            return Ok("Product added to cart.");
        }

        
        [HttpDelete("removefromcart/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartRepository.RemoveFromCartAsync(userId, productId);
            return Ok("Product removed from cart.");
        }
    }
}
