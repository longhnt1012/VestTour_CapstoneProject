using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class BankingAccount
{
    public int BankingAccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountName { get; set; } = null!;

    public string? Bank { get; set; }

    public int? PaymentId { get; set; }

    public virtual Payment? Payment { get; set; }
}
