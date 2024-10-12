using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Category")]
public partial class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column("CategoryParentID")]
    public int? CategoryParentId { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [Column("ImageURL")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
