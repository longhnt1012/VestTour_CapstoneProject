using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

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

<<<<<<<< Updated upstream:SEVestTourAPI/Entities/Booking.cs
========
    [StringLength(255)]
    public string? GuestName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? GuestEmail { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? GuestPhone { get; set; }

>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/Booking.cs
    [ForeignKey("StoreId")]
    [InverseProperty("Bookings")]
    public virtual Store? Store { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Bookings")]
    public virtual User? User { get; set; }
}
