using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string? ProductCode { get; set; }

    [Column("MeasurementID")]
    public int? MeasurementId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [Column("FabricID")]
    public int? FabricId { get; set; }

    [Column("LiningID")]
    public int? LiningId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

<<<<<<<< Updated upstream:SEVestTourAPI/Entities/Product.cs
========
    public bool? IsCustom { get; set; }

    [Column("ImgURL")]
    [StringLength(255)]
    public string? ImgUrl { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/Product.cs
    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [ForeignKey("FabricId")]
    [InverseProperty("Products")]
    public virtual Fabric? Fabric { get; set; }

    [ForeignKey("LiningId")]
    [InverseProperty("Products")]
    public virtual Lining? Lining { get; set; }

    [ForeignKey("MeasurementId")]
    [InverseProperty("Products")]
    public virtual Measurement? Measurement { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Products")]
    public virtual Order? Order { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<StyleOption> StyleOptions { get; set; } = new List<StyleOption>();
}
