namespace VestTour.Repository.Models
{
    public class UpdateUserModel
    {
        public string Name { get; set; } = null!;
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateOnly? Dob { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
      
    }
}
