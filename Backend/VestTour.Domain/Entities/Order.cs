using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? PaymentId { get; set; }

    public int? StoreId { get; set; }

    public int? VoucherId { get; set; }

    public int? ShipperPartnerId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public string? Note { get; set; }

    public bool Paid { get; set; }

    public string? Status { get; set; }

<<<<<<< Updated upstream
    public decimal TotalPrice { get; set; }

=======
    public decimal? TotalPrice { get; set; }

    public decimal? Deposit { get; set; }
    public decimal? ShippingFee { get; set; }
    public decimal? BalancePayment { get; set; }
   
>>>>>>> Stashed changes
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Payment? Payment { get; set; }

    public virtual ShipperPartner? ShipperPartner { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }

    public virtual Voucher? Voucher { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
