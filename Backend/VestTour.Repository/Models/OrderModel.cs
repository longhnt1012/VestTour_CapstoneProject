using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int? UserID { get; set; }
        public int? StoreId { get; set; }
        public int? VoucherId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public DateOnly? ShippedDate { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }
        public bool Paid { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestAddress { get; set; }
        public MeasurementModel? GuestMeasurements { get; set; }

        [JsonIgnore]
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public decimal? TotalPrice { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? BalancePayment => TotalPrice - Deposit + ShippingFee;
        public string? DeliveryMethod { get; set; }
        public decimal? RevenueShare { get; set; }
        public string? ShipStatus { get; set; }

        // [JsonIgnore]
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();
    }
}

