using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Service.Services;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCartController : ControllerBase
    {
        private readonly IAddCartService _addCartService;
        private readonly PaypalClient _paypalClient;
        private readonly IOrderService _orderService;

        public AddCartController(IAddCartService addCartService, PaypalClient paypalClient,IOrderService orderService)
        {
            _addCartService = addCartService;
            _paypalClient=paypalClient;
            _orderService=orderService;
        }

        private int? GetUserId()
        {
            return User.Identity.IsAuthenticated
                ? int.Parse(User.FindFirstValue(ClaimTypes.Name))
                : null;
        }

        [HttpGet("mycart")]
        public async Task<ActionResult<CartModel>> ViewUserCart()
        {
            var userId = GetUserId();
            var cart = await _addCartService.GetUserCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("addtocart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestModel request)
        {
            var userId = GetUserId();

            if (request.IsCustom)
            {
                await _addCartService.AddToCartAsync(userId, true, customProduct: request.CustomProduct);
            }
            else if (request.ProductId.HasValue)
            {
                await _addCartService.AddToCartAsync(userId, false, productId: request.ProductId.Value);
            }
            else
            {
                return BadRequest("Invalid product information.");
            }

            return Ok("Product added to cart.");
        }

        [HttpDelete("removefromcart/{productCode}")]
        public async Task<IActionResult> RemoveFromCart(string productCode)
        {
            var userId = GetUserId();
            await _addCartService.RemoveFromCartAsync(userId, productCode);
            return Ok("Product removed from cart.");
        }

        [HttpPost("increase/{productCode}")]
        public async Task<IActionResult> IncreaseQuantity(string productCode)
        {
            var userId = GetUserId();
            await _addCartService.IncreaseQuantityAsync(userId, productCode);
            return Ok("Product quantity increased.");
        }

        [HttpPost("decrease/{productCode}")]
        public async Task<IActionResult> DecreaseQuantity(string productCode)
        {
            var userId = GetUserId();
            await _addCartService.DecreaseQuantityAsync(userId, productCode);
            return Ok("Product quantity decreased.");
        }

        [HttpPost("confirmorder")]
        public async Task<IActionResult> ConfirmOrder(string? guestName, string? guestEmail, string? guestAddress, decimal? deposit, decimal? shippingfee)
        {
            var userId = GetUserId();
            try
            {
                await _addCartService.ConfirmOrderAsync(userId, guestName, guestEmail, guestAddress,deposit, shippingfee);
                return Ok("Order confirmed successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [Authorize]
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(int userID, CancellationToken cancellationToken)
        {
            var totalPrice = await _addCartService.GetTotalPriceAsync(userID);
            if (totalPrice <= 0)
            {
                return BadRequest("The cart is empty or has invalid items.");
            }
            var tongtien = totalPrice.ToString("F2");
            var donViTienTe = "USD";
            var maDonHangThamChieu = "OR" + DateTime.Now.Ticks.ToString();
            try
            {
                var response = await _paypalClient.CreateOrder(tongtien, donViTienTe, maDonHangThamChieu);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }

        }
        [Authorize]
        [HttpPost("Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);

                //Lưu database thì xài create order
                
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
