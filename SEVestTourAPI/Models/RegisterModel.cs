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
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

<<<<<<<< Updated upstream:SEVestTourAPI/Models/RegisterModel.cs
        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^(\+84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Invalid phone number.")]
========
        [Required(ErrorMessage = "Password is required.")]
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Models/RegisterModel.cs
        public string Password { get; set; }
    }
}
