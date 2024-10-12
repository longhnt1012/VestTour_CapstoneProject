namespace SEVestTourAPI.Models
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
        
    }
}
