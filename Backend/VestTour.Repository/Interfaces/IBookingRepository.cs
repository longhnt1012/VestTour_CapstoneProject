using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Services
{
    public interface IBookingRepository
    {
        public Task<List<BookingModel>> GetAllBooking();
        public Task<BookingModel> GetBookingById(int id);

        public Task UpdateBooking(int id, BookingModel model);
        Task UpdateBookingStatusAsync(int bookingId, string status);
        public Task<int> AddNewBookingAsync(BookingModel booking);
        public Task DeleteBookingAsync(int id);
        Task<int> GetTotalBookingCountAsync();
        //Task<decimal?> GetLastDepositCostAsync(int? userId, string? guestName, string? email);
        Task<List<BookingModel>> GetBookingsByUserIdAndDateRangeAsync(int userId, DateOnly? startDate, DateOnly? endDate);
        Task<List<BookingModel>> GetBookingsByGuestNameAndDateRangeAsync(string guestName, DateOnly? startDate, DateOnly? endDate);
        Task<List<BookingModel>> GetBookingsByEmailAndDateRangeAsync(string email, DateOnly? startDate, DateOnly? endDate);
        Task<Booking> GetLastBookingAsync(int? userId, string? guestName, string? email);
    }
}
