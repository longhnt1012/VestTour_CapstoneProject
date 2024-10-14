using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

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
    public bool IsCustom { get; set; }
    public string? ImgURL { get; set; }
    public decimal? Price { get; set; }
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
}
