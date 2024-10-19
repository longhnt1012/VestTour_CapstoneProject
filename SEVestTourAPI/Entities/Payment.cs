using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Payment")]
public partial class Payment
{
    [Key]
    [Column("PaymentID")]
    public int PaymentId { get; set; }

    [Column("BankingAccountID")]
    public int? BankingAccountId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(50)]
    public string? Method { get; set; }

    public DateOnly? PaymentDate { get; set; }

    [StringLength(255)]
    public string? PaymentDetails { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [ForeignKey("BankingAccountId")]
    [InverseProperty("Payments")]
    public virtual BankingAccount? BankingAccount { get; set; }

    [InverseProperty("Payment")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual User? User { get; set; }
}
