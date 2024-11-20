using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public string? GuestName { get; set; }

    public string? GuestEmail { get; set; }

    public string? GuestPhone { get; set; }

    public TimeOnly? Time { get; set; }

    public string? Note { get; set; }

    public string? Status { get; set; }

    public int? StoreId { get; set; }

   // public decimal? DepositCost { get; set; }

    public string? Service { get; set; }

    public virtual Store? Store { get; set; }

    public virtual User? User { get; set; }
}
