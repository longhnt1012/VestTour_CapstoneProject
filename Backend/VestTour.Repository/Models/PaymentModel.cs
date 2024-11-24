using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VestTour.Repository.Models
{
    public class PaymentModel
    {

        [Key]
        [Column("PaymentID")]
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }

        [Column("UserID")]
        public int? UserId { get; set; }


        [StringLength(50)]
        public string? Method { get; set; }

        public DateOnly? PaymentDate { get; set; }

        [StringLength(255)]
        public string? PaymentDetails { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }
        public decimal? Amount { get; set; }
        //public string? PaymentCode { get; set; }
    }
}
