using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Service.Services;
using VestTour.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Numerics;
using Microsoft.AspNetCore.Cors;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class AddCartController : ControllerBase
    {
        private readonly IAddCartService _addCartService;
        private readonly PaypalClient _paypalClient;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddCartController(IHttpContextAccessor httpContextAccessor, IAddCartService addCartService, PaypalClient paypalClient, IOrderService orderService, IPaymentService paymentService)
        {
            _addCartService = addCartService;
            _paypalClient = paypalClient;
            _orderService = orderService;
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> ConfirmOrder(string? guestName, string? guestEmail, string? guestAddress, string? guestPhone, decimal deposit, decimal shippingfee, string deliverymethod, int storeId, int? voucherId)
        {
            var userId = GetUserId();
            try
            {
                var newOrderResponse =  await _addCartService.ConfirmOrderAsync(userId, guestName, guestEmail, guestAddress,guestPhone , deposit, shippingfee, deliverymethod, storeId, voucherId);
                return Ok(new { Message = "Order confirmed successfully.", OrderId = newOrderResponse });
          
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

        [HttpDelete("remove/{productCode}")]
        public async Task<IActionResult> RemoveFromCart(int? userId, string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return BadRequest("Product code cannot be empty.");

            await _addCartService.RemoveFromCartAsync(userId, productCode);
            return Ok(new { message = "Item removed from cart successfully." });
        }

        // Clear the entire cart
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart(int? userId)
        {
            await _addCartService.ClearCartAsync(userId);
            return Ok(new { message = "Cart cleared successfully." });
        }
        //[Authorize]
       
        [HttpPost("/Cart/createpayment")]
        public async Task<IActionResult> CreatePayment( bool isDeposit,decimal payAmount )
        {
            try
            {
                var userId = GetUserId();
               // var totalPrice = await _addCartService.GetTotalPriceAsync(userId);
                string paymentDetails = "";
                paymentDetails = isDeposit ? "Make deposit 50%" : "Paid full";


                // Create a PaymentModel object
                var payment = new PaymentModel
                {
                    UserId = userId,
                    OrderId = null, // You might want to replace this hardcoded value with a dynamic one
                    Amount = payAmount,
                    Method = "PayPal",
                    PaymentDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    PaymentDetails = paymentDetails,
                    Status = "Success"
                };

                // Save the payment record
                var newPaymentResponse = await _paymentService.AddNewPaymentAsync(payment);
                if (!newPaymentResponse.Success)
                {
                    return BadRequest(new { Error = newPaymentResponse.Message });
                }

                // Save PaymentId into the session
                HttpContext.Session.SetInt32("paymentId", newPaymentResponse.Data);

                return Ok(new { Message = "Payment created successfully.", paymentId = newPaymentResponse.Data });
            }
            catch (Exception ex)
            {
                // Return error details
                return BadRequest(new { Error = ex.Message });
            }

        }
         


    }
}
