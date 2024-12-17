using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public string? Method { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentDetails { get; set; }

    public string? Status { get; set; }

    public int? OrderId { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<BankingAccount> BankingAccounts { get; set; } = new List<BankingAccount>();

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
