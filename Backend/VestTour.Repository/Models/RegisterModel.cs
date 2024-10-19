using System.ComponentModel.DataAnnotations;

namespace VestTour.Repository.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
