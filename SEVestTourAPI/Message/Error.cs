<<<<<<<< HEAD:Backend/VestTour.Repository/Constants/Error.cs
﻿namespace VestTour.Repository.Constants
========
﻿namespace VestTour.Constants
>>>>>>>> bcd41fa4de15cca8d11df9b24c9fe030759871d1:Backend/VestTour.Service/Constants/Error.cs
{
    public static class Error
    {
        // User-related errors
        public const string InvalidEmail = "Invalid email format.";
        public const string InvalidUserStatus = "Invalid user status";
        public const string InvalidPassword = "Password must be between 6 and 18 characters.";
        public const string InvalidName = "Name must be between 5 and 25 characters.";
        public const string EmailTaken = "Email is already taken.";
        public const string InvalidGender = "Gender must be Nam or Nu.";
        public const string RegistrationFailed = "User registration failed.";
        public const string UserNotFound = "User not found.";
        public const string UserIdMismatch = "User ID mismatch.";
        public const string InvalidModelState = "Invalid model state.";

        // Fabric-related errors
        public const string FabricNotFound = "Fabric not found.";
        public const string FabricAddFailed = "Failed to add fabric.";
        public const string FabricUpdateFailed = "Failed to update fabric.";
        public const string FabricDeleteFailed = "Failed to delete fabric.";
        // bang voucher
        public const string VoucherNotFound = "Voucher not found.";
        public const string VoucherAddFailed = "Failed to add voucher.";
        public const string VoucherUpdateFailed = "Failed to update voucher.";
        public const string VoucherDeleteFailed = "Failed to delete voucher.";
        public const string InvalidVoucherCode = "Invalid voucher code.";
        public const string InvalidVoucherDateRange = "Invalid voucher date range.";

<<<<<<<< Updated upstream:SEVestTourAPI/Message/Error.cs
========
        public const string OrderNotFound = "Order not found.";
        public const string OrderAddFailed = "Failed to add order.";
        public const string OrderUpdateFailed = "Failed to update order.";
        public const string OrderDeleteFailed = "Failed to delete order.";
        public const string InvalidOrderStatus = "Invalid order status.";
        public const string OrderAlreadyShipped = "Order has already been shipped and cannot be modified.";

        // Category-related errors
        public const string InvalidCategoryId = "Invalid Category ID.";
        public const string CategoryNotFound = "Category not found.";
        public const string InvalidCategoryName = "Category name cannot be empty.";
        public const string NoCategoriesFound = "No categories found.";
        public const string InvalidParentCategoryId = "Invalid Parent Category ID.";
        public const string CategoryIDmismatch = "Category ID mismatch.";
        // Additional errors
        public const string UnknownError = "An unknown error occurred. Please try again.";
        public const string InvalidInputData = "Invalid input data. Please check your request.";

        //Booking error
        public const string InvalidBookingId = "The booking ID is invalid.";
        public const string BookingNotFound = "Booking not found.";
        public const string BookingCreateFailed = "Booking create failed.";
        public const string NoBookingsFound = "No bookings found.";
        public const string InvalidGuestName = "Guest name cannot be empty.";
<<<<<<<< HEAD:Backend/VestTour.Repository/Constants/Error.cs
        public const string InvalidBookingStatus = "The booking status is invalid.";
        public const string UpdateStatusFailed = "Failed to update booking status.";

========
       
>>>>>>>> bcd41fa4de15cca8d11df9b24c9fe030759871d1:Backend/VestTour.Service/Constants/Error.cs
>>>>>>>> Stashed changes:Backend/VestTour.Service/Constants/Error.cs
    }
}
