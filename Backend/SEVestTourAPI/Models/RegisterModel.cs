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
        public string Email { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "RoleID is required.")]
        public int RoleID { get; set; }
        

        [Required(ErrorMessage = "Phone is required.")]
        public string? Phone { get; set; }
    }
}
