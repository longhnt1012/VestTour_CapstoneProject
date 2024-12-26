using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class ShipperPartner
{
    public int ShipperPartnerId { get; set; }

    public string? Phone { get; set; }

    public string? Company { get; set; }

    public string? Status { get; set; }

}
