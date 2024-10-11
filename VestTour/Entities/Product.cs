using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductCode { get; set; }

    public int? MeasurementId { get; set; }

    public int? CategoryId { get; set; }

    public int? FabricId { get; set; }

    public int? LiningId { get; set; }

    public int? OrderId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Fabric? Fabric { get; set; }

    public virtual Lining? Lining { get; set; }

    public virtual Measurement? Measurement { get; set; }

    public virtual Order? Order { get; set; }
}
