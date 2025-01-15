using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payments
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            var response = await _paymentService.GetAllPaymentsAsync();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        // GET: api/Payments/{id}
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var response = await _paymentService.GetPaymentByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        // POST: api/Payments
        [HttpPost]
       // [Authorize(Roles = "customer, staff")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentModel payment)
        {
            if (payment == null)
            {
                return BadRequest("Payment data is required.");
            }

            var response = await _paymentService.AddNewPaymentAsync(payment);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetPaymentById), new { id = response.Data }, response.Data);
        }

        // PUT: api/Payments/{id}
        [HttpPut("{id}")]
      //  [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentModel payment)
        {
            if (payment == null || id <= 0)
            {
                return BadRequest("Invalid payment data.");
            }

            var response = await _paymentService.UpdatePaymentAsync(id, payment);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        // DELETE: api/Payments/{id}
        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid payment ID.");
            }

            var response = await _paymentService.DeletePaymentAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        // PATCH: api/Payments/{paymentId}/order/{orderId}
        [HttpPatch("{paymentId}/order/{orderId}")]
       // [Authorize(Roles = "customer")]
        public async Task<IActionResult> UpdatePaymentOrderId(int paymentId, int orderId)
        {
            try
            {
                await _paymentService.UpdatePaymentOrderIdAsync(paymentId, orderId);
                return Ok("Payment order ID updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("by-order/{orderId}")]
       // [Authorize]
        public async Task<IActionResult> GetPaymentsByOrderId(int orderId)
        {
            var response = await _paymentService.GetPaymentsByOrderIdAsync(orderId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        // GET: api/Payments/by-user/{userId}
        [HttpGet("by-user/{userId}")]
      //  [Authorize]
        public async Task<IActionResult> GetPaymentsByUserId(int userId)
        {
            var response = await _paymentService.GetPaymentsByUserIdAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
