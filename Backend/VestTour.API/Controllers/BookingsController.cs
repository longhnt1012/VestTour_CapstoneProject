using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("{bookingId}")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var response = await _bookingService.GetBookingByIdAsync(bookingId);
        if (!response.Success)
        {
            return NotFound(new { Message = response.Message });
        }
        return Ok(new { Message = Success.BookingRetrieved, Data = response.Data });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var response = await _bookingService.GetAllBookingsAsync();
        if (!response.Success)
        {
            return NotFound(new { Message = response.Message });
        }
        return Ok(new { Message = Success.TotalBookingsRetrieved, Data = response.Data });
    }

    [HttpPost("guest-booking")]
    public async Task<IActionResult> AddBooking([FromBody] BookingModel booking)
    {
        var response = await _bookingService.AddBookingAsync(booking);
        if (!response.Success)
        {
            return BadRequest(new { Message = response.Message });
        }
        return CreatedAtAction(nameof(GetBookingById), new { bookingId = response.Data }, new { Message = response.Message, BookingId = response.Data });
    }
    [HttpPost("loggedin-user-booking")]
    [Authorize]  // Ensure the user is authenticated
    public async Task<IActionResult> CreateBookingForLoggedInUser(BookingModel model)
    {
        // Get the UserId from JWT claims
        var userIdClaim = User.FindFirst(ClaimTypes.Name);
        if (userIdClaim == null)
        {
            return Unauthorized(Error.UserNotFound);
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            return BadRequest(Error.UserIdMismatch);
        }

        var serviceResponse = await _bookingService.CreateBookingForLoggedInUserAsync(userId, model);

        if (!serviceResponse.Success)
        {
            return serviceResponse.Message switch
            {
                Error.UserNotFound => NotFound(serviceResponse.Message),
                Error.OrderNotFound => NotFound(serviceResponse.Message),
                _ => BadRequest(serviceResponse.Message),
            };
        }

        return Ok(serviceResponse.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingModel booking)
    {
        var response = await _bookingService.UpdateBookingAsync(id, booking);
        if (!response.Success)
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(new { Message = response.Message });
    }

    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> DeleteBooking(int bookingId)
    {
        var response = await _bookingService.DeleteBookingAsync(bookingId);
        if (!response.Success)
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(new { Message = response.Message });
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetTotalBookingCount()
    {
        var response = await _bookingService.GetTotalBookingCountAsync();
        return Ok(new { TotalBookings = response.Data });
    }
}
