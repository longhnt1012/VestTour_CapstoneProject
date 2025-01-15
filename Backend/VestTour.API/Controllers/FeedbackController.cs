using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // Endpoint to get all feedbacks
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFeedback()
        {
            var response = await _feedbackService.GetAllFeedbackAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        // Endpoint to get feedbacks by userId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFeedbacksByUserId(int userId)
        {
            var response = await _feedbackService.GetFeedbacksByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        // Endpoint to get feedback by orderId
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetFeedbacksByOrderId(int orderId)
        {
            var response = await _feedbackService.GetFeedbacksByOrderIdAsync(orderId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        // Endpoint to get feedback by productId
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetFeedbacksByProductId(int productId)
        {
            var response = await _feedbackService.GetFeedbacksByProductIdAsync(productId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }
        // Add feedback for product
        [HttpPost("feedbackforproduct")]
        //[Authorize(Roles = "customer")]
        public async Task<IActionResult> AddFeedbackForProduct([FromBody] FeedBackForProduct feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");

            var response = await _feedbackService.AddFeedbackForProductAsync(feedback);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // Add feedback for order
        [HttpPost("feedbackfororder")]
        //[Authorize(Roles = "customer")]
        public async Task<IActionResult> AddFeedbackForOrder([FromBody] FeedbackForOrder feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");

            var response = await _feedbackService.AddFeedbackForOrderAsync(feedback);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // Update feedback for product
        [HttpPut("updateproductfeedback/{feedbackId}")]
        //[Authorize(Roles = "customer")]
        public async Task<IActionResult> UpdateFeedbackForProduct(int feedbackId, [FromBody] FeedBackForProduct feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");

            var response = await _feedbackService.UpdateFeedbackForProductAsync(feedbackId, feedback);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // Update feedback for order
        [HttpPut("updateorderfeedbackorder/{feedbackId}")]
       // [Authorize(Roles = "customer")]
        public async Task<IActionResult> UpdateFeedbackForOrder(int feedbackId, [FromBody] FeedbackForOrder feedback)
        {
            if (feedback == null)
                return BadRequest("Feedback cannot be null.");

            var response = await _feedbackService.UpdateFeedbackForOrderAsync(feedbackId, feedback);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        // Endpoint to update feedback
        [HttpPut("response/{feedbackId}")]
        //[Authorize(Roles = "staff")]
        public async Task<IActionResult> ResponseFeedback(int feedbackId, [FromBody] ResponseFeedbackModel responsefeedbackModel)
        {
            var response = await _feedbackService.ResponseFeedbackAsync(feedbackId, responsefeedbackModel);
            if (response.Success)
            {
                return NoContent();
            }
            return NotFound(response.Message);
        }
        // Endpoint to delete feedback
        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(int feedbackId)
        {
            var response = await _feedbackService.DeleteFeedbackAsync(feedbackId);
            if (response.Success)
            {
                return NoContent();
            }
            return NotFound(response.Message);
        }
    }
}