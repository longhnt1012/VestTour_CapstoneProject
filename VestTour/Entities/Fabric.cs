using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Fabric
{
    public int FabricId { get; set; }

    public string? FabricName { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
