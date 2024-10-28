using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }

    public virtual Product Product { get; set; } = null!;
}
