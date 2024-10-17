using VestTour.Repository.Models;

namespace VestTour.Services
{
    public interface IBookingRepository
    {
        public Task<List<BookingModel>> GetAllBooking();
        public Task<BookingModel> GetBookingById(int id);

        public Task UpdateBooking(int id, BookingModel model);

        public Task<int> AddNewBookingAsync(BookingModel booking);
        public Task DeleteBookingAsync(int id);
        Task<int> GetTotalBookingCountAsync();
    }
}
