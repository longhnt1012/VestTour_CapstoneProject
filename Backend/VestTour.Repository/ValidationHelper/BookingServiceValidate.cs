using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public static class BookingServiceValidate
    {
        // Define allowed service types with exact matches
        private static readonly HashSet<string> AllowedServices = new HashSet<string>
    {
        "Tailor", "Return", "Exchange", "Fix"
    };

        // Method to validate the service type
        public static bool IsValidService(string service)
        {
            return !string.IsNullOrEmpty(service) && AllowedServices.Contains(service);
        }
    }

}
