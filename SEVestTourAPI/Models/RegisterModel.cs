using System.ComponentModel.DataAnnotations;

namespace SEVestTourAPI.Models
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

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^(\+84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Invalid phone number.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RoleID is required.")]
        public int RoleID { get; set; }
    }
}
