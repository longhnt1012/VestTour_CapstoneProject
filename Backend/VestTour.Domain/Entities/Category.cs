using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? CategoryParentId { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
