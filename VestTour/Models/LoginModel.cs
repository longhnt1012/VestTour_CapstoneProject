using System.ComponentModel.DataAnnotations;

namespace VestTour.Models
{
    public class LoginModel
    {

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
