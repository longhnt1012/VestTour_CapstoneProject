﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int? UserID { get; set; }
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
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public decimal? TotalPrice => Products.Sum(p => p.Price ?? 0m);

    }
}
