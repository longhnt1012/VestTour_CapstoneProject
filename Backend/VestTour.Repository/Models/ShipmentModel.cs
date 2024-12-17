using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ShipmentModel
    {
        public int ShipmentId { get; set; }
        public int ShipperPartnerId { get; set; }
        public string? TrackNumber { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public string? Status { get; set; } // Pending, Shipped, Delivered,Finished
        public DateOnly? CreateAt { get; set; }
        public DateOnly? ShippedAt { get; set; }
        public DateOnly? DeliveredAt { get; set; }

    }
}
