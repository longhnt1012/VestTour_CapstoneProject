using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingsController(IBookingRepository bookingRepo) 
        {
            _bookingRepo = bookingRepo;
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
                return booking ==null ? NotFound() : Ok(booking);
            }
            catch
            {
                return BadRequest();
            } 
         
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewBooking(BookingModel model)
        {
            if(model== null)
            {
                return BadRequest("invalid booking data");

            }
            var newBooking = new BookingModel
            {
                UserId = model.UserId,
                BookingDate = model.BookingDate,
                Time = model.Time,
                Note = model.Note,
                Status = model.Status ?? "on-going",
                StoreId = model.StoreId,

            };

           var newbooking= await _bookingRepo.AddNewBookingAsync(newBooking);
            var booking = await _bookingRepo.GetBookingById(newbooking);

            return booking == null ? NotFound() : Ok(booking); 
        }

      

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, BookingModel booking)
        {
            try
            {
                var updateBooking = await _bookingRepo.GetBookingById(id);
                if (updateBooking == null)
                {
                    return NotFound();
                }
                booking.UserId = booking.UserId;
                booking.BookingDate = booking.BookingDate;
                booking.Time = booking.Time;
                booking.Note = booking.Note;

                booking.Status = booking.Status ?? "cancel";
                booking.StoreId = booking.StoreId;

                await _bookingRepo.UpdateBooking(id, booking);
                return Ok();
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
                if(id != null)
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
    }
}
