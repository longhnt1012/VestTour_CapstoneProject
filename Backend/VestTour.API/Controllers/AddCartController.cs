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
        public async Task<IActionResult> ConfirmOrder(string? guestName, string? guestEmail, string? guestAddress, decimal deposit, decimal shippingfee, string deliverymethod, int storeId, int? voucherId)
        {
            var userId = GetUserId();
            try
            {
                await _addCartService.ConfirmOrderAsync(userId, guestName, guestEmail, guestAddress, deposit, shippingfee, deliverymethod, storeId, voucherId);

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
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken, bool isDeposit = false)
        {
            var userId = GetUserId();
            var totalPrice = await _addCartService.GetTotalPriceAsync(userId);
            if (totalPrice <= 0)
            {
                return BadRequest("The cart is empty or has invalid items.");
            }
            var tongtien = totalPrice.ToString("F2");
            decimal tienphaitra;
            if (isDeposit)
            {
                tienphaitra = totalPrice / 2;
                tongtien = tienphaitra.ToString("F2");
            }
            HttpContext.Session.SetString("tongtien", tongtien);
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
        //[Authorize]
        [HttpPost("Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                // Capture the PayPal order
                var response = await _paypalClient.CaptureOrder(orderID);
                if (response.status != "COMPLETED")
                {
                    return BadRequest(new { Error = "PayPal order capture failed." });
                }

                // Retrieve the "tongtien" from the session
                var tongtienString = _httpContextAccessor.HttpContext?.Session.GetString("tongtien");
                if (string.IsNullOrEmpty(tongtienString))
                {
                    return BadRequest(new { Error = "Tongtien is not found in the session." });
                }

                // Validate and parse "tongtien"
                if (!decimal.TryParse(tongtienString, out var tongtien))
                {
                    return BadRequest(new { Error = "Invalid format for tongtien." });
                }

                // Get the user ID and calculate the total cart price
                var userId = GetUserId();
                var totalPrice = await _addCartService.GetTotalPriceAsync(userId);

                // Determine payment details
                string paymentDetails = tongtien < totalPrice ? "Make deposit 50%" : "Paid full";

                // Create a PaymentModel object
                var payment = new PaymentModel
                {
                    UserId = userId,
                    OrderId = 26, // You might want to replace this hardcoded value with a dynamic one
                    Amount = tongtien,
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
                HttpContext.Session.SetInt32("PaymentId", newPaymentResponse.Data);

                return Ok(new { Message = "Payment created successfully.", PaymentId = newPaymentResponse.Data });
            }
            catch (Exception ex)
            {
                // Return error details
                return BadRequest(new { Error = ex.Message });
            }
        }


    }
}
