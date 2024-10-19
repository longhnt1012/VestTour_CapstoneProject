using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Lining")]
public partial class Lining
{
    [Key]
    [Column("LiningID")]
    public int LiningId { get; set; }

    [StringLength(255)]
    public string? LiningName { get; set; }

    [Column("ImageURL")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Lining")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
