using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("User")]
[Index("Email", Name = "UQ__User__A9D10534ACC7C52F", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string? Gender { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [Column("DOB")]
    public DateOnly? Dob { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public bool IsConfirmed { get; set; }
<<<<<<<< Updated upstream:SEVestTourAPI/Entities/User.cs
	public string Status { get; set; }
========

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? Phone { get; set; }

>>>>>>>> Stashed changes:Backend/VestTour.Domain/Entities/User.cs
    [InverseProperty("User")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("User")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("User")]
    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

   
}
