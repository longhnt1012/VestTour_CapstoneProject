using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class ProductStyleOption
{
    public int? ProductId { get; set; }

    public int? StyleOptionId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual StyleOption? StyleOption { get; set; }
}
