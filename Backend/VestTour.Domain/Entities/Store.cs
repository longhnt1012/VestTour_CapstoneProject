using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Store
{
    public int StoreId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContactNumber { get; set; }

    public int? StoreCode { get; set; }
    public TimeOnly? OpenTime { get; set; }
    public TimeOnly? CloseTime { get; set; }
    public string? StaffIDs { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual TailorPartner? TailorPartner { get; set; }

    public virtual User? User { get; set; }
}
