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
        public async Task<ActionResult<CartModel>> ViewUserCart()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var cart = await _addCartService.GetUserCartAsync(userId);
            return Ok(cart);
        }


        [HttpPost("addtocart")]
        public async Task<IActionResult> AddToCart([FromBody] CustomProductModel customProduct)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.AddToCartAsync(userId, customProduct);
            return Ok("Product added to cart.");
        }


        [HttpDelete("removefromcart/{productCode}")]
        public async Task<IActionResult> RemoveFromCart(string productCode)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.RemoveFromCartAsync(userId, productCode);
            return Ok("Product removed from cart.");
        }

        [HttpPost("increase/{productCode}")]
        public async Task<IActionResult> IncreaseQuantity(string productCode)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.IncreaseQuantityAsync(userId, productCode);
            return Ok("Product quantity increased.");
        }

        [HttpPost("decrease/{productCode}")]
        public async Task<IActionResult> DecreaseQuantity(string productCode)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _addCartService.DecreaseQuantityAsync(userId, productCode);
            return Ok("Product quantity decreased.");
        }
        [HttpPost("confirmorder")]
        public async Task<IActionResult> ConfirmOrder()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name)); // Lấy ID người dùng từ token
            try
            {
                await _addCartService.ConfirmOrderAsync(userId);
                return Ok("Order confirmed successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Trả về thông báo không tìm thấy nếu không có sản phẩm
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); // Trả về thông báo lỗi nếu giỏ hàng trống
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // Trả về lỗi máy chủ nếu có lỗi khác
            }
        }

    }
}
