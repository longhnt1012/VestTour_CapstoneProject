using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Order")]
public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("PaymentID")]
    public int? PaymentId { get; set; }

    [Column("StoreID")]
    public int? StoreId { get; set; }

    [Column("VoucherID")]
    public int? VoucherId { get; set; }

    [Column("ShipperPartnerID")]
    public int? ShipperPartnerId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    public bool Paid { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }
<<<<<<<< Updated upstream:SEVestTourAPI/Entities/Order.cs
========

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalPrice { get; set; }
>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/Order.cs

    [InverseProperty("Order")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [ForeignKey("PaymentId")]
    [InverseProperty("Orders")]
    public virtual Payment? Payment { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [ForeignKey("ShipperPartnerId")]
    [InverseProperty("Orders")]
    public virtual ShipperPartner? ShipperPartner { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Orders")]
    public virtual Store? Store { get; set; }

    [ForeignKey("VoucherId")]
    [InverseProperty("Orders")]
    public virtual Voucher? Voucher { get; set; }
}
