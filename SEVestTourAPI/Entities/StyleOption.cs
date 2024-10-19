using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("StyleOption")]
public partial class StyleOption
{
    [Key]
    [Column("StyleOptionID")]
    public int StyleOptionId { get; set; }

    [Column("StyleID")]
    public int? StyleId { get; set; }

    [StringLength(100)]
    public string? OptionType { get; set; }

    [StringLength(100)]
    public string? OptionValue { get; set; }

<<<<<<<< Updated upstream:SEVestTourAPI/Entities/StyleOption.cs
========
    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/StyleOption.cs
    [ForeignKey("StyleId")]
    [InverseProperty("StyleOptions")]
    public virtual Style? Style { get; set; }

    [ForeignKey("StyleOptionId")]
    [InverseProperty("StyleOptions")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
