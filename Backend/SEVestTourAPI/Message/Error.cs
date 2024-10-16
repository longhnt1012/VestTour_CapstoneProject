﻿namespace SEVestTourAPI.Message
{
    public static class Error
    {
        // User-related errors
        public const string InvalidEmail = "Invalid email format.";
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

        public const string OrderNotFound = "Order not found.";
        public const string OrderAddFailed = "Failed to add order.";
        public const string OrderUpdateFailed = "Failed to update order.";
        public const string OrderDeleteFailed = "Failed to delete order.";
        public const string InvalidOrderStatus = "Invalid order status.";
        public const string OrderAlreadyShipped = "Order has already been shipped and cannot be modified.";
    }
}
