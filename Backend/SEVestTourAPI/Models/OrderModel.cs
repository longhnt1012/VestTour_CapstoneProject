using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SEVestTourAPI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        
        public int? PaymentId { get; set; }

        
        public int? StoreId { get; set; }

        
        public int? VoucherId { get; set; }

      
        public int? ShipperPartnerId { get; set; }

        public DateOnly? OrderDate { get; set; }

        public DateOnly? ShippedDate { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }

        public bool Paid { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
