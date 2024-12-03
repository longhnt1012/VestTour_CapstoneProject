using System;
using System.Collections.Generic;

namespace VestTour.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public bool IsConfirmed { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string? AvtUrl { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    public virtual TailorPartner? TailorPartner { get; set; }
}
