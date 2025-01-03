﻿namespace VestTour.Repository.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public DateOnly? Dob { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsConfirmed { get; set; }

        public string Status { get; set; }
        public string? Phone { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? AvtUrl { get; set; }
        public bool? IsOnline { get; set; }

    }
}
