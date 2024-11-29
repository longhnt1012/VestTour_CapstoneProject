using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ConfirmOrderModel
    {
        public int? UserId { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestAddress { get; set; }
        public decimal Deposit { get; set; }
        public decimal ShippingFee { get; set; }
        public string DeliveryMethod { get; set; }
        public int StoreId { get; set; }
        public int? VoucherId { get; set; }
    }

}
