using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ConfirmRequest
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}