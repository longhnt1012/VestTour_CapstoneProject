using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SEVestTourAPI.Entities;

[Table("Booking")]
public partial class Booking
{
    [Key]
    [Column("BookingID")]
    public int BookingId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public TimeOnly? Time { get; set; }

    [StringLength(255)]
    public string? Note { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column("StoreID")]
    public int? StoreId { get; set; }
    public string? GuestName {  get; set; }
    public string? GuestEmail { get; set; }
    public string? GuestPhone { get; set; }
    [ForeignKey("StoreId")]
    [InverseProperty("Bookings")]
    public virtual Store? Store { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Bookings")]
    public virtual User? User { get; set; }
}
