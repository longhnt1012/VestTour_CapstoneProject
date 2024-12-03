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
            var newBooking = _mapper.Map<Booking>(booking);
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
            var deleteBooking = await _context.Bookings!.SingleOrDefaultAsync(bo => bo.BookingId == id);
            if (deleteBooking != null)
            {
                _context.Bookings.Remove(deleteBooking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBookingStatusAsync(int bookingId, string status)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookingModel>> GetAllBooking()
        {
            var bookings = await _context.Bookings!.ToListAsync();
            return _mapper.Map<List<BookingModel>>(bookings);
        }

        public async Task<BookingModel> GetBookingById(int id)
        {
            var booking = await _context.Bookings!.FindAsync(id);
            return _mapper.Map<BookingModel>(booking);
        }

        public async Task UpdateBooking(int id, BookingModel model)
        {
            if (id == model.BookingId)
            {
                var updateBooking = _mapper.Map<Booking>(model);
                _context.Bookings!.Update(updateBooking);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Booking> GetLastBookingAsync(int? userId, string? guestName, string? email)
        {
            IQueryable<Booking> query = _context.Bookings;

            if (userId.HasValue)
            {
                query = query.Where(b => b.UserId == userId.Value);
            }

            if (!string.IsNullOrWhiteSpace(guestName))
            {
                query = query.Where(b => b.GuestName != null && b.GuestName.Contains(guestName));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(b => b.GuestEmail == email);
            }

            // Get the last booking based on BookingDate or BookingId (assumed that later means the last)
            var lastBooking = await query.OrderByDescending(b => b.BookingDate).FirstOrDefaultAsync();

            return lastBooking; // Returns null if no booking found
        }

        //public async Task<List<BookingModel>> GetBookingsByUserIdAsync(int userId)
        //{
        //    var bookings = await _context.Bookings!
        //        .Where(b => b.UserId == userId)
        //        .ToListAsync();

        //    return _mapper.Map<List<BookingModel>>(bookings);
        //}

        //public async Task<List<BookingModel>> GetBookingsByGuestNameAsync(string guestName)
        //{
        //    var bookings = await _context.Bookings!
        //        .Where(b => b.GuestName != null && b.GuestName.Contains(guestName))
        //        .ToListAsync();

        //    return _mapper.Map<List<BookingModel>>(bookings);
        //}

        //public async Task<List<BookingModel>> GetBookingsByEmail(string email)
        //{
        //    var bookings = await _context.Bookings!
        //        .Where(b => b.GuestEmail == email)
        //        .ToListAsync();
        //    return _mapper.Map<List<BookingModel>>(bookings);
        //}

        // New method to get bookings by user ID with date range
        public async Task<List<BookingModel>> GetBookingsByUserIdAndDateRangeAsync(int userId, DateOnly? startDate, DateOnly? endDate)
        {
            var query = _context.Bookings.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= endDate.Value);
            }

            var bookings = await query.Where(b => b.UserId == userId).ToListAsync();
            return _mapper.Map<List<BookingModel>>(bookings);
        }

        // New method to get bookings by guest name with date range
        public async Task<List<BookingModel>> GetBookingsByGuestNameAndDateRangeAsync(string guestName, DateOnly? startDate, DateOnly? endDate)
        {
            var query = _context.Bookings.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= endDate.Value);
            }

            var bookings = await query
                .Where(b => b.GuestName != null && b.GuestName.Contains(guestName))
                .ToListAsync();

            return _mapper.Map<List<BookingModel>>(bookings);
        }

        // New method to get bookings by email with date range
        public async Task<List<BookingModel>> GetBookingsByEmailAndDateRangeAsync(string email, DateOnly? startDate, DateOnly? endDate)
        {
            var query = _context.Bookings.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= endDate.Value);
            }

            var bookings = await query
                .Where(b => b.GuestEmail == email)
                .ToListAsync();

            return _mapper.Map<List<BookingModel>>(bookings);
        }
        public async Task<List<BookingModel>> GetBookingsByStoreIdAsync(int storeId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.StoreId == storeId)
                .ToListAsync();
            return _mapper.Map<List<BookingModel>>(bookings);
        }
        public async Task StaffAssistWithBooking(int bookingId, string assistStaffName, string note)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking != null)
            {
                booking.Status = "Confirmed";
                booking.AssistStaffName = assistStaffName; // Update the staff assist name
                booking.Note = note;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();
            }
        }



    }
}
