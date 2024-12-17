namespace VestTour.Repository.Constants
{
    public static class Error
    {
        public const string InvalidEmail = "Invalid email format.";
        public const string InvalidUserStatus = "Invalid user status";
        public const string InvalidUserId = "Invalid user Id";
        public const string InvalidPassword = "Password must be between 6 and 18 characters.";
        public const string InvalidName = "Name must be between 5 and 25 characters.";
        public const string EmailTaken = "Email is already taken.";
        public const string InvalidGender = "Gender must be Male or Female.";
        public const string RegistrationFailed = "User registration failed.";
        public const string UserNotFound = "User not found.";
        public const string UserIdMismatch = "User ID mismatch.";
        public const string InvalidPhone = "Invalid phone number format.";
        public const string InvalidModelState = "Invalid model state.";
        public const string InvalidDobFormat = "Invalid Date of Birth format. Please use MM-DD-YYYY.";
        public const string EmailNotFound = "The provided email is not associated with any user.";
        // Fabric-related errors
        public const string InvalidFabricId = "Invalid Fabric ID.";
        public const string FabricNotFound = "Fabric not found.";
        public const string FabricAddFailed = "Failed to add fabric.";
        public const string FabricUpdateFailed = "Failed to update fabric.";
        public const string FabricDeleteFailed = "Failed to delete fabric.";
        public const string InvalidInputData = "Invalid input data.";

        // bang voucher
        public const string VoucherNotFound = "Voucher not found.";
        public const string VoucherAddFailed = "Failed to add voucher.";
        public const string VoucherUpdateFailed = "Failed to update voucher.";
        public const string VoucherDeleteFailed = "Failed to delete voucher.";
        public const string InvalidVoucherCode = "Invalid voucher code.";
        public const string InvalidVoucherDateRange = "Invalid voucher date range.";

        public const string OrderNotFound = "Order not found.";
        public const string OrderAddFailed = "Failed to add order.";
        public const string OrderUpdateFailed = "Failed to update order.";
        public const string OrderDeleteFailed = "Failed to delete order.";
        public const string InvalidOrderStatus = "Invalid order status.";
        public const string OrderAlreadyShipped = "Order has already been shipped and cannot be modified.";

        // Category-related errors
        public const string InvalidParentCategoryId = "Invalid parent category ID.";
        public const string InvalidCategoryId = "Invalid Category ID.";
        public const string CategoryNotFound = "Category not found.";
        public const string NoCategoriesFound = "No categories found.";
        public const string InvalidCategoryName = "Invalid Category name.";
        public const string CategoryIDmismatch = "Category ID mismatch.";
        // Additional errors
        public const string UnknownError = "An unknown error occurred. Please try again.";


        //Booking error
        public const string InvalidBookingId = "The booking ID is invalid.";
        public const string BookingNotFound = "Booking not found.";
        public const string BookingCreateFailed = "Booking create failed.";
        public const string NoBookingsFound = "No bookings found.";
        public const string InvalidGuestName = "Guest name cannot be empty.";
        public const string InvalidBookingStatus = "The booking status is invalid.";
        public const string UpdateStatusFailed = "Failed to update booking status.";
        //Bang product 
        public const string ProductNotFound = "Product not found.";

        //Measurement table

        public const string InvalidMeasurementId = "Invalid Measurement ID.";
        public const string MeasurementNotFound = "Measurement not found.";
        //Lining table

        public const string FailedDeleteLining = "Lining Deleted failed";
        public const string LiningNotFound = "Lining not found";


        public const string InvalidTailorPartnerId = "Invalid tailor partner ID provided.";
        public const string TailorPartnerNotFound = "Tailor partner not found.";
        public const string NoTailorPartnersFound = "No tailor partners found.";

        public const string InvalidProcessingTailorId = "Invalid processing tailor ID provided.";
        public const string InvalidStageName = "Invalid stage name.";
        public const string InvalidProcessStatus = "Invalid process status.";
        public const string InvalidStageStatus = "Invalid stage status.";
        public const string ProcessingTailorNotFound = "Processing tailor not found.";
        public const string NoProcessingTailorsFound = "No processing tailors found.";
        public const string NoProcessesAssigned = "No process assigned.";
        public static string InvalidOrderId = "The provided Order ID is invalid.";
        public static string NoProcessingTailorsFoundForOrder = "No processing tailors found for the specified Order ID.";

        public const string InvalidStoreId = "Invalid store ID provided.";
        public static string InvalidOtpOrExpired { get; set; }
        public static string InvalidToken { get; set; }

        public const string InvalidShipmentId = "Invalid shipment ID.";
        public const string ShipmentNotFound = "Shipment not found";
        public const string NoShipmentsFound = "No shipments found.";
        public const string InvalidShipmentData = "Invalid shipment data. Recipient name and status cannot be empty.";
        public const string InvalidShipmentStatus = "Invalid shipment status.";
        public const string InvalidRecipientName = "Invalid recipient name. It cannot be empty.";
    }
}
