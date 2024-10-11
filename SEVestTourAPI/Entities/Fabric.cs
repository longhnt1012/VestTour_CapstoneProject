using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Fabric")]
public partial class Fabric
{
    [Key]
    [Column("FabricID")]
    public int FabricId { get; set; }

    [StringLength(255)]
    public string? FabricName { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column("ImageURL")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Fabric")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
