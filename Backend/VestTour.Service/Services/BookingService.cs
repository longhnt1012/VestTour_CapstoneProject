﻿using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
using VestTour.Services;
using VestTour.Domain.Enums;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepo; // Add IUserRepository for user details

    public BookingService(IBookingRepository bookingRepository, IUserRepository userRepo)
    {
        _bookingRepository = bookingRepository;
        _userRepo = userRepo; 
    }
    public async Task<ServiceResponse> UpdateBookingStatusAsync(int bookingId, string status)
    {
        var response = new ServiceResponse();

        if (bookingId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidBookingId; // Sử dụng thông báo lỗi đã định nghĩa
            return response;
        }

        // Kiểm tra xem trạng thái có hợp lệ không
        if (string.IsNullOrEmpty(status) || !Enum.TryParse<BookingEnums>(status, true, out _))
        {
            response.Success = false;
            response.Message = "Status is not valid. Please provide a valid status."; // Thông báo lỗi tùy chỉnh
            return response;
        }

        // Tiến hành cập nhật trạng thái
        await _bookingRepository.UpdateBookingStatusAsync(bookingId, status);
        response.Message = Success.StatusUpdated; // Sử dụng thông báo thành công
        return response;
    }

    public async Task<ServiceResponse<BookingModel?>> GetBookingByIdAsync(int bookingId)
    {
        var response = new ServiceResponse<BookingModel?>();

        if (bookingId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidBookingId; // Use a constant from Error class
            return response;
        }
        

        var booking = await _bookingRepository.GetBookingById(bookingId);
        if (booking == null)
        {
            response.Success = false;
            response.Message = Error.BookingNotFound; // Use a constant from Error class
        }
        else
        {
            response.Data = booking;
        }

        return response;
    }

    public async Task<ServiceResponse<List<BookingModel>>> GetAllBookingsAsync()
    {
        var response = new ServiceResponse<List<BookingModel>>();
        var bookings = await _bookingRepository.GetAllBooking();

        if (bookings == null || !bookings.Any())
        {
            response.Success = false;
            response.Message = Error.NoBookingsFound; // Use a constant from Error class
        }
        else
        {
            response.Data = bookings;
        }

        return response;
    }

    public async Task<ServiceResponse<int>> AddBookingAsync(BookingModel booking)
    {
        var response = new ServiceResponse<int>();

        if (string.IsNullOrEmpty(booking.GuestName))
        {
            response.Success = false;
            response.Message = Error.InvalidGuestName; // Sử dụng thông báo lỗi đã định nghĩa
            return response;
        }

        // Đặt trạng thái mặc định là "Pending"
        booking.Status = BookingEnums.Pending.ToString();

        var newBookingId = await _bookingRepository.AddNewBookingAsync(booking);
        response.Data = newBookingId;
        response.Message = Success.BookingCreated; // Sử dụng thông báo thành công
        return response;
    }

    public async Task<ServiceResponse> UpdateBookingAsync(int id, BookingModel booking)
    {
        var response = new ServiceResponse();

        if (id <= 0 || string.IsNullOrEmpty(booking.GuestName))
        {
            response.Success = false;
            response.Message = Error.InvalidInputData;
            return response;
        }

        await _bookingRepository.UpdateBooking(id, booking);
        response.Message = Success.BookingUpdated;
        return response;
    }

    public async Task<ServiceResponse> DeleteBookingAsync(int bookingId)
    {
        var response = new ServiceResponse();

        if (bookingId <= 0)
        {
            response.Success = false;
            response.Message = Error.InvalidBookingId; // Use a constant from Error class
            return response;
        }

        await _bookingRepository.DeleteBookingAsync(bookingId);
        response.Message = Success.BookingDeleted; // Use a constant from Success class
        return response;
    }

    public async Task<ServiceResponse<int>> GetTotalBookingCountAsync()
    {
        var response = new ServiceResponse<int>();

        var totalBookings = await _bookingRepository.GetTotalBookingCountAsync();
        response.Data = totalBookings;
        return response;
    }

    // New method for creating booking for logged-in user
    public async Task<ServiceResponse<BookingModel>> CreateBookingForLoggedInUserAsync(int userId, BookingModel model)
    {
        var response = new ServiceResponse<BookingModel>();

        // Fetch user details from repository
        var user = await _userRepo.GetUserByIdAsync(userId);
        if (user == null)
        {
            response.Success = false;
            response.Message = Error.UserNotFound; // Use a constant from Error class
            return response; // Return response if user not found
        }

        // Create the booking with user details
        var newBooking = new BookingModel
        {
            UserId = user.UserId,
            BookingDate = model.BookingDate,
            Time = model.Time,
            Note = model.Note,
            Status = BookingEnums.Pending.ToString(), // Set default status to "Pending"
            StoreId = model.StoreId,
            GuestName = user.Name,
            GuestEmail = user.Email,
            GuestPhone = user.Phone
        };

        var newBookingId = await _bookingRepository.AddNewBookingAsync(newBooking);
        var booking = await _bookingRepository.GetBookingById(newBookingId);

        if (booking == null)
        {
            response.Success = false;
            response.Message = Error.BookingCreateFailed; // Add an appropriate error message
        }
        else
        {
            response.Data = booking;
            response.Message = Success.BookingCreated; // Use a constant from Success class
        }

        // Return the response regardless of success or failure
        return response;
    }

}
