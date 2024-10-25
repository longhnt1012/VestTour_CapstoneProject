using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? Status { get; set; }

    public string? VoucherCode { get; set; }

    public string? Description { get; set; }

    public decimal? DiscountNumber { get; set; }

    public DateOnly? DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
