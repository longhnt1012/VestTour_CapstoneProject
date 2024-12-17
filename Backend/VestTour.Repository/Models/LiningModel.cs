using System.ComponentModel.DataAnnotations;

namespace VestTour.Repository.Models
{
    public class LiningModel
    {
        [Key]
        public int LiningId { get; set; }

        [Required(ErrorMessage = "Lining name is required.")]
        [StringLength(100, ErrorMessage = "Lining name can't exceed 100 characters.")]
        public string? LiningName { get; set; }

        
        [StringLength(255, ErrorMessage = "Image URL can't exceed 255 characters.")]
        public string? ImageUrl { get; set; }
        public string? Status { get; set; }
    }
}
