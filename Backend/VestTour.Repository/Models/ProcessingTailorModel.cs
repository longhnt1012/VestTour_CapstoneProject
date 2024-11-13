using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ProcessingTailorModel
    {
        public int ProcessingId { get; set; }
        public string? StageName { get; set; }
        public int TailorPartnerId { get; set; }
        public string? Status { get; set; }
        public int OrderId { get; set; }
        public string? Note { get; set; }
    }
}
