using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Keyless]
[Table("ProductStyleOption")]
public partial class ProductStyleOption
{
    [Column("ProductID")]
    public int? ProductId { get; set; }

    [Column("StyleOptionID")]
    public int? StyleOptionId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("StyleOptionId")]
    public virtual StyleOption? StyleOption { get; set; }
}
