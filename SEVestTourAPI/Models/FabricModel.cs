using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SEVestTourAPI.Models
{
    public class FabricModel
    {
        [Key]
        [Column("FabricID")]
        public int FabricId { get; set; }

        [StringLength(255)]
        public string? FabricName { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Column("ImageURL")]
        [StringLength(255)]
        public string? ImageUrl { get; set; }

    }
}
