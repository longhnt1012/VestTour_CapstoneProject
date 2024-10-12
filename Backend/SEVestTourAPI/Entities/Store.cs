using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Store")]
public partial class Store
{
    [Key]
    [Column("StoreID")]
    public int StoreId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContactNumber { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Store")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("Stores")]
    public virtual User? User { get; set; }
}
