using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class StaffAssistRequest
    {
        public string Status { get; set; } // The status to update the booking to
        public int StaffId { get; set; }   // The staff member assisting with the booking
    }

}
