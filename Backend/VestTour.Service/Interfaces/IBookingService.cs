using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Service.Interface
{
    public interface IBookingService
    {
        Task<ServiceResponse> UpdateBookingStatusAsync(int bookingId, string status);
        Task<ServiceResponse<BookingModel?>> GetBookingByIdAsync(int bookingId);
        Task<ServiceResponse<List<BookingModel>>> GetAllBookingsAsync();
        Task<ServiceResponse<int>> AddBookingAsync(BookingModel booking);
        Task<ServiceResponse> UpdateBookingAsync(int id, BookingModel booking);
        Task<ServiceResponse> DeleteBookingAsync(int bookingId);
        Task<ServiceResponse<int>> GetTotalBookingCountAsync();
        Task<Booking> GetLastBookingAsync(int? userId, string? guestName, string? email);
        Task<ServiceResponse<BookingModel>> CreateBookingForLoggedInUserAsync(int userId, BookingModel model);
        Task<ServiceResponse<List<BookingModel>>> GetUserBookingsAsync(int? userId, string? guestName, string? email, DateOnly? startDate, DateOnly? endDate);

    }
}
