using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
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
    [HttpGet("user-booking")]
    public async Task<IActionResult> GetUserBooking([FromQuery] int? userId, [FromQuery] string? guestName, [FromQuery] string? email, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
    {
        // Call the service method that handles the logic for filtering bookings
        var result = await _bookingService.GetUserBookingsAsync(userId, guestName, email, startDate, endDate);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Data);
    }
    [HttpGet("last-booking")]
    public async Task<IActionResult> GetLastBooking([FromQuery] int? userId, [FromQuery] string? guestName, [FromQuery] string? email)
    {
        // Call the service method to get the last booking
        var lastBooking = await _bookingService.GetLastBookingAsync(userId, guestName, email);

        // Check if the last booking is null and return appropriate response
        if (lastBooking == null)
        {
            return NotFound("No bookings found."); // Return 404 if no booking is found
        }

        return Ok(lastBooking); // Return 200 with the last booking details
    }
    [HttpGet]
    [Authorize(Roles = "admin,store manager,staff")]
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
    [Authorize(Roles = "customer")]
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
    [Authorize(Roles = "admin,store manager,staff")]
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
    [Authorize(Roles = "admin,store manager,staff")]
    public async Task<IActionResult> DeleteBooking(int bookingId)
    {
        var response = await _bookingService.DeleteBookingAsync(bookingId);
        if (!response.Success)
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(new { Message = response.Message });
    }
    [HttpPut("status/{bookingId}")]
    [Authorize(Roles = "admin,store manager,staff")]
    public async Task<IActionResult> UpdateBookingStatus(int bookingId, [FromBody] string status)
    {
        var response = await _bookingService.UpdateBookingStatusAsync(bookingId, status);
        if (!response.Success)
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(new { Message = response.Message });
    }


    [HttpGet("count")]
    [Authorize(Roles = "admin,store manager,staff")]
    public async Task<IActionResult> GetTotalBookingCount()
    {
        var response = await _bookingService.GetTotalBookingCountAsync();
        return Ok(new { TotalBookings = response.Data });
    }
}
