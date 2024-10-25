using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class UpdateVoucherModel
    {
        public string? Status { get; set; }         // Status column

        public string VoucherCode { get; set; }     // Unique VoucherCode

        public string? Description { get; set; }    // Description column

        public decimal? DiscountNumber { get; set; } // DiscountNumber column

        public DateOnly? DateStart { get; set; }     // DateStart column

        public DateOnly? DateEnd { get; set; }
    }
}
