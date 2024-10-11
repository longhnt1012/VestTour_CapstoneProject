using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Style")]
public partial class Style
{
    [Key]
    [Column("StyleID")]
    public int StyleId { get; set; }

    [StringLength(255)]
    public string? StyleName { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [InverseProperty("Style")]
    public virtual ICollection<StyleOption> StyleOptions { get; set; } = new List<StyleOption>();
}
