using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class StatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
    {
        "Active","Deactive"
    };

        // Method to validate the service type
        public static bool IsValidStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }
}
