namespace VestTour.Repository.Constants
{
    public static class Success
    {
        // User-related success messages
        public const string RegistrationSuccess = "User registration successful.";
        public const string LoginSuccess = "User logged in successfully.";
        public const string UserUpdated = "User information updated successfully.";
        public const string UserDeleted = "User deleted successfully.";
        public const string StatusUpdated = "User status updated successfully.";
        public const string OtpResentSuccess = "OTP has been resent successfully.";
        // Fabric-related success messages
        public const string FabricAdded = "Fabric successfully added.";
        public const string FabricUpdated = "Fabric successfully updated.";
        public const string FabricDeleted = "Fabric successfully deleted.";
        // Category-related success messages
        public const string CategoryAdded = "Category successfully added.";
        public const string CategoryUpdated = "Category successfully updated.";
        public const string CategoryDeleted = "Category successfully deleted.";
        // Measurement-related success messages
        public const string MeasurementAdded = "Measurement successfully added.";
        public const string MeasurementUpdated = "Measurement successfully updated.";
        public const string MeasurementDeleted = "Measurement successfully deleted.";

        // Voucher-related success messages
        public const string VoucherAdded = "Voucher added successfully.";
        public const string VoucherUpdated = "Voucher updated successfully.";
        public const string VoucherDeleted = "Voucher deleted successfully.";

        // Order-related success messages
        public const string OrderCreated = "Order created successfully.";
        public const string OrderUpdated = "Order updated successfully.";
        public const string OrderDeleted = "Order deleted successfully.";
        public const string OrderShipped = "Order has been shipped successfully.";
        //Lining 
        public const string SuccessDeleteLining = "Lining Deleted Successfully";
        public const string SuccessUpdateLining = "Lining updated Successfully";



        public const string BookingsRetrieved = "Bookings Retrieved.";
        public const string BookingCreated = "Booking created successfully.";
        public const string BookingUpdated = "Booking updated successfully.";
        public const string BookingDeleted = "Booking deleted successfully.";
        public const string BookingRetrieved = "Booking retrieved successfully.";
        public const string TotalBookingsRetrieved = "Total bookings retrieved successfully.";
         public const string BookingStatusUpdated = "Booking status has been successfully updated.";

       

        public const string TailorPartnerAdded = "Tailor partner successfully added.";
        public const string TailorPartnerUpdated = "Tailor partner successfully updated.";
        public const string TailorPartnerDeleted = "Tailor partner successfully deleted.";

        public const string ProcessingTailorAdded = "Processing tailor successfully added.";
        public const string ProcessingTailorUpdated = "Processing tailor successfully updated.";
        public const string ProcessingTailorDeleted = "Processing tailor successfully deleted.";
        //ProductInStore
        public const string ProductUpdatedInStore = "Product in store updated successfully.";
        public const string ProductDeletedFromStore = "Product in store deleted successfully.";
        public const string QuantityUpdatedInStore = "Product in store quantity updated successfully.";
        public const string ProductAddedToStore = "Product in store added successfully.";
        public const string ProcessingTailorStatusUpdated = "Processing tailor status updated successfully.";
        public const string SampleStatusUpdated = "Sample status updated successfully.";
        public const string FixStatusUpdated = "Fix status updated successfully.";
        public const string DeliveryStatusUpdated = "Delivery status updated successfully.";
        public const string StageNameUpdated = "Stage name updated successfully.";
        public const string OrderStatusUpdated = "Order status updated successfully.";
        public static string OtpConfirmed { get; set; }
        public static string ResetEmailSent { get; set; }
        public static string PasswordResetSuccess { get; set; }
    }
}
