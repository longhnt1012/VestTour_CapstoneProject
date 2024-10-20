using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Models;

namespace VestTour.Services
{
    public class BookingRepository : IBookingRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddNewBookingAsync(BookingModel booking)
        {

            var newBooking= _mapper.Map<Booking>(booking);
            _context.Bookings!.Add(newBooking);
            await _context.SaveChangesAsync();

            return newBooking.BookingId;
          

        }
        public async Task<int> GetTotalBookingCountAsync()
        {
            return await _context.Bookings!.CountAsync();
        }
        public async Task DeleteBookingAsync(int id)
        {
            var deleteBooking = _context.Bookings!.SingleOrDefault(bo => bo.BookingId == id);
           
            if(deleteBooking != null)
            {
                _context.Bookings.Remove(deleteBooking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookingModel>> GetAllBooking()
        {
            var bookings= await _context.Bookings!.ToListAsync();
            return _mapper.Map<List<BookingModel>>(bookings);
        }

        public async Task<BookingModel> GetBookingById(int id)
        {
            var booking = await _context.Bookings!.FindAsync(id);
            return _mapper.Map<BookingModel>(booking);
        }

        public async Task UpdateBooking(int id, BookingModel model)
        {
           if(id == model.BookingId)
            {
                var updateBooking = _mapper.Map<Booking>(model);
                _context.Bookings!.Update(updateBooking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
