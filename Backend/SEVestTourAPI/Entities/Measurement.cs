using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Measurement")]
public partial class Measurement
{
    [Key]
    [Column("MeasurementID")]
    public int MeasurementId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Weight { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Height { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Neck { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Hip { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Waist { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Armhole { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Biceps { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PantsWaist { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Crotch { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Thigh { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? PantsLength { get; set; }

    [InverseProperty("Measurement")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [ForeignKey("UserId")]
    [InverseProperty("Measurements")]
    public virtual User? User { get; set; }
}
