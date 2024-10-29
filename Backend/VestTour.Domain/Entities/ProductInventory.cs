using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class ProductInventory
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
