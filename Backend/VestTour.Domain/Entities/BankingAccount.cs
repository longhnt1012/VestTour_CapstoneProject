using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class BankingAccount
{
    public int BankingAccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string? Bank { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
