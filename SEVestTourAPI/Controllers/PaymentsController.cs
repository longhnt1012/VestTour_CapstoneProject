using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepo;

        public PaymentsController(IPaymentRepository repo)
        {
            _paymentRepo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            try
            {
                return Ok(await _paymentRepo.GetAllPaymentAsync());
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByID(int id)
        {
            try
            {
                var payment= await _paymentRepo.GetPaymentByIDAsync(id);
                return payment ==null ? NotFound() : Ok(payment);
            }catch 
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddNewPayment(PaymentModel payment)
        {
            try
            {
                var newPayment = await _paymentRepo.AddNewPayment(payment);
                var paymentAdded = await _paymentRepo.GetPaymentByIDAsync(newPayment);
                return paymentAdded == null ? NotFound() : Ok(paymentAdded);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentModel payment)
        {
            try
            {
                if (id != payment.PaymentId)
                {
                    return NotFound();
                }
                if (id != null)
                {
                    await _paymentRepo.UpdatePayment(id, payment);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                if(id != null)
                {
                    await _paymentRepo.DeletePayment(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        
        




    }
}
