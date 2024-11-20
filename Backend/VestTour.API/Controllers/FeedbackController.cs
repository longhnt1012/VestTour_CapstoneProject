using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
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

        // Endpoint to add new feedback
        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody] WriteFeedbackModel feedbackModel)
        {
            var response = await _feedbackService.AddFeedbackAsync(feedbackModel);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetFeedbacksByUserId), new { userId = feedbackModel.UserId }, response.Data);
            }
            return BadRequest(response.Message);
        }

        // Endpoint to update feedback
        [HttpPut("{feedbackId}")]
        public async Task<IActionResult> UpdateFeedback(int feedbackId, [FromBody] WriteFeedbackModel feedbackModel)
        {
            var response = await _feedbackService.UpdateFeedbackAsync(feedbackId, feedbackModel);
            if (response.Success)
            {
                return NoContent();
            }
            return NotFound(response.Message);
        }
        // Endpoint to update feedback
        [HttpPut("response/{feedbackId}")]
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