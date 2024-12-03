using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? StoreId { get; set; }

    public int? VoucherId { get; set; }

    public int? ShipperPartnerId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public string? Note { get; set; }

    public bool Paid { get; set; }

    public string? Status { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? BalancePayment { get; set; }

    public decimal? ShippingFee { get; set; }

    public string? GuestName { get; set; }

    public string? GuestEmail { get; set; }

    public string? GuestAddress { get; set; }
    public string? DeliveryMethod { get; set; }
    public decimal? RevenueShare { get; set; }
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    //public virtual Payment? Payment { get; set; }

    public virtual ICollection<ProcessingTailor> ProcessingTailors { get; set; } = new List<ProcessingTailor>();

    public virtual ShipperPartner? ShipperPartner { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }

    public virtual Voucher? Voucher { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
