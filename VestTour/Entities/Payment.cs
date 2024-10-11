using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? BankingAccountId { get; set; }

    public int? UserId { get; set; }

    public string? Method { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentDetails { get; set; }

    public string? Status { get; set; }

    public virtual BankingAccount? BankingAccount { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
