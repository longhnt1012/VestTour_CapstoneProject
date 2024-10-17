using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Voucher")]
[Index("VoucherCode", Name = "UQ__Voucher__7F0ABCA98F441C47", IsUnique = true)]
public partial class Voucher
{
    [Key]
    [Column("VoucherID")]
    public int VoucherId { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? VoucherCode { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiscountNumber { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    [InverseProperty("Voucher")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
