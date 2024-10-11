using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SEVestTourAPI.Models
{
    public class BankingAccountModel
    {
        [Key]
        [Column("BankingAccountID")]
        public int BankingAccountId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string AccountNumber { get; set; } = null!;

        [StringLength(255)]
        public string AccountName { get; set; } = null!;

        [StringLength(255)]
        public string? Bank { get; set; }
    }
}
