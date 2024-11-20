using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }

        public string? Comment { get; set; }
        public DateOnly? DateSubmitted { get; set; }
        public int? Rating { get; set; }

        public string? Response { get; set; }
        public DateOnly? DateResponse { get; set; }

       

        public int? UserId { get; set; }

        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
