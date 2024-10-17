using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("ShipperPartner")]
public partial class ShipperPartner
{
    [Key]
    [Column("ShipperPartnerID")]
    public int ShipperPartnerId { get; set; }

    [StringLength(255)]
    public string ShipperPartnerName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string? Company { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [InverseProperty("ShipperPartner")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
