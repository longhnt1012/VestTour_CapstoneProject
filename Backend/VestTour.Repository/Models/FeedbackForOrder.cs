using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class FeedbackForOrder
    {
        public int FeedbackId { get; set; }
        public string? Comment { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        public DateOnly? DateSubmitted { get; set; }
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
       
    }
}
