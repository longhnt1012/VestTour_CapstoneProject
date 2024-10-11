using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Lining
{
    public int LiningId { get; set; }

    public string? LiningName { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
