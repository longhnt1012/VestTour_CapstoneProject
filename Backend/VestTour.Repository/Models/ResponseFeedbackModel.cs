using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ResponseFeedbackModel
    {
        public int FeedbackId { get; set; }
        public string? Response { get; set; }
        public DateOnly? DateResponse { get; set; }

    }
}
