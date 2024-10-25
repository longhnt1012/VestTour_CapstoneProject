using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductCode { get; set; }

    public int? MeasurementId { get; set; }

    public int? CategoryId { get; set; }

    public int? FabricId { get; set; }

    public int? LiningId { get; set; }

    public bool? IsCustom { get; set; }

    public string? ImgUrl { get; set; }

    public decimal? Price { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Fabric? Fabric { get; set; }

    public virtual Lining? Lining { get; set; }

    public virtual Measurement? Measurement { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StyleOption> StyleOptions { get; set; } = new List<StyleOption>();
    public ICollection<ProductStyleOption> ProductStyleOptions { get; set; }

}
