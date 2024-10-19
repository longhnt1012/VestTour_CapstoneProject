using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("BankingAccount")]
public partial class BankingAccount
{
    [Key]
    [Column("BankingAccountID")]
    public int BankingAccountId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AccountNumber { get; set; } = null!;

    [StringLength(255)]
    public string AccountName { get; set; } = null!;

    [StringLength(255)]
    public string? Bank { get; set; }

    [InverseProperty("BankingAccount")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
