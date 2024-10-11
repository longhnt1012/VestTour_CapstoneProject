using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class ShipperPartner
{
    public int ShipperPartnerId { get; set; }

    public string ShipperPartnerName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Company { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
