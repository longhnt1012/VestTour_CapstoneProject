using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class OrderPayment
{
    public int OrderId { get; set; }

    public int PaymentId { get; set; }

    public int Times { get; set; }

    public DateOnly PaidDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
