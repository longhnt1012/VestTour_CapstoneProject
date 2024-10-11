﻿namespace VestTour.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
