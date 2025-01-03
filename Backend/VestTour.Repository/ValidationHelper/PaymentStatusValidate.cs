using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class PaymentStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
    {
        "Success","Failed"
    };

        // Method to validate the service type
        public static bool IsValidStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }
}
