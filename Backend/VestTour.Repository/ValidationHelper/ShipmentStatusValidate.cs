using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public static class ShipmentStatusValidate
    {
        // Define allowed service types with exact matches
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
    {
         "Tailoring","Shipping","Ready", "Finished"
    };

        // Method to validate the service type
        public static bool IsValidStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }

}
