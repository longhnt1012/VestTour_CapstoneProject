using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VestTour.Repository.Models
{
    public class BookingModel
    {
       
        public int BookingId { get; set; }

      
        public int? UserId { get; set; }

        public DateOnly? BookingDate { get; set; }

        public TimeOnly? Time { get; set; }

       
        public string? Note { get; set; }

        
        public string? Status { get; set; }

        
        public int? StoreId { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestPhone { get; set; }
       // public decimal? DepositCost { get; set; }

        public string? Service { get; set; }
    }
}
