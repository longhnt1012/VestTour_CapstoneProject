using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Enums;

namespace VestTour.Domain.Entities;

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
<<<<<<<< Updated upstream:SEVestTourAPI/Entities/Fabric.cs
 public string? Tag {  get; set; }
========

    [StringLength(255)]
    public FabricEnums? Tag { get; set; }

>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/Fabric.cs
    [InverseProperty("Fabric")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
