using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using SEVestTourAPI.ValidationHelpers;
using SEVestTourAPI.Message;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IUserRepository _userRepo;

        public BookingsController(IBookingRepository bookingRepo, IUserRepository userRepo)
        {
            _bookingRepo = bookingRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            try
            {
                return Ok(await _bookingRepo.GetAllBooking());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByID(int id)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingById(id);
                return booking == null ? NotFound() : Ok(booking);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBooking(BookingModel model)
        {
            if (model == null)
            {
                return BadRequest(Error.InvalidModelState);
            }

            // Validate guest name, email, and phone
            if (!UserValidate.IsValidName(model.GuestName))
            {
                return BadRequest(Error.InvalidName);
            }

            if (!UserValidate.IsValidEmail(model.GuestEmail))
            {
                return BadRequest(Error.InvalidEmail);
            }

            if (!UserValidate.IsValidPhone(model.GuestPhone))
            {
                return BadRequest(Error.InvalidPhone);
            }

            var newBooking = new BookingModel
            {
                UserId = model.UserId,
                BookingDate = model.BookingDate,
                Time = model.Time,
                Note = model.Note,
                Status = model.Status ?? "on-going",
                StoreId = model.StoreId,
                GuestName = model.GuestName,
                GuestEmail = model.GuestEmail,
                GuestPhone = model.GuestPhone
            };

            var newbooking = await _bookingRepo.AddNewBookingAsync(newBooking);
            var booking = await _bookingRepo.GetBookingById(newbooking);

            return booking == null ? NotFound() : Ok(booking);
        }
        [HttpPost("create-for-loggedin-user")]
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

            // Fetch user details from repository
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }

            // Create the booking with user details
            var newBooking = new BookingModel
            {
                UserId = user.UserId,
                BookingDate = model.BookingDate,
                Time = model.Time,
                Note = model.Note,
                Status = model.Status ?? "on-going",
                StoreId = model.StoreId,
                GuestName = user.Name,      // Set guest info from user data
                GuestEmail = user.Email,
                GuestPhone = user.Phone
            };

            var newbookingId = await _bookingRepo.AddNewBookingAsync(newBooking);
            var booking = await _bookingRepo.GetBookingById(newbookingId);

            return booking == null ? NotFound(Error.OrderNotFound) : Ok(booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, BookingModel booking)
        {
            if (!UserValidate.IsValidName(booking.GuestName))
            {
                return BadRequest(Error.InvalidName);
            }

            if (!UserValidate.IsValidEmail(booking.GuestEmail))
            {
                return BadRequest(Error.InvalidEmail);
            }

            if (!UserValidate.IsValidPhone(booking.GuestPhone))
            {
                return BadRequest(Error.InvalidPhone);
            }

            try
            {
                var updateBooking = await _bookingRepo.GetBookingById(id);
                if (updateBooking == null)
                {
                    return NotFound();
                }

                // Update booking details
                updateBooking.UserId = booking.UserId;
                updateBooking.BookingDate = booking.BookingDate;
                updateBooking.Time = booking.Time;
                updateBooking.Note = booking.Note;
                updateBooking.Status = booking.Status ?? "cancel";
                updateBooking.StoreId = booking.StoreId;
                updateBooking.GuestName = booking.GuestName;
                updateBooking.GuestEmail = booking.GuestEmail;
                updateBooking.GuestPhone = booking.GuestPhone;

                await _bookingRepo.UpdateBooking(id, updateBooking);
                return Ok(updateBooking);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            try
            {
                if (id != null)
                {
                    await _bookingRepo.DeleteBookingAsync(id);
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

        [HttpGet("count")]
        public async Task<IActionResult> GetTotalBookingCount()
        {
            try
            {
                var totalBookings = await _bookingRepo.GetTotalBookingCountAsync();
                return Ok(new { TotalBookings = totalBookings });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
