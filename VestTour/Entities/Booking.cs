using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public int? VoucherId { get; set; }

    public int? StoreId { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
