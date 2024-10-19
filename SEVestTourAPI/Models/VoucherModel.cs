namespace VestTour.Repository.Models
{
    public class VoucherModel
    {
        public int VoucherId { get; set; }          // Corresponds to VoucherID in the database

        public string? Status { get; set; }         // Status column

        public string VoucherCode { get; set; }     // Unique VoucherCode

        public string? Description { get; set; }    // Description column

        public decimal? DiscountNumber { get; set; } // DiscountNumber column

        public DateOnly? DateStart { get; set; }     // DateStart column

        public DateOnly? DateEnd { get; set; }       // DateEnd column
    }
}
