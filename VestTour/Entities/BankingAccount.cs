using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class BankingAccount
{
    public int BankingAccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string? Bank { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
