using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int ShipperPartnerId { get; set; }

    public string? TrackNumber { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientAddress { get; set; }

    public string? Status { get; set; }

    public DateOnly? CreateAt { get; set; }

    public DateOnly? ShippedAt { get; set; }

    public DateOnly? DeliveredAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ShipperPartner ShipperPartner { get; set; } = null!;
}
