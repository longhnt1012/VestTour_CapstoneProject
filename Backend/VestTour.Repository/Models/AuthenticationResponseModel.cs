using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class AuthenticationResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
