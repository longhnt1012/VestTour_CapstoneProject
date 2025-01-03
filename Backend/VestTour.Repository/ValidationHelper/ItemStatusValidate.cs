using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class ItemStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
    {
        "Available","Unavailable"
    };

        // Method to validate the service type
        public static bool IsValidStatus(string status)
        {
            if (status == null)
            {
                return true; 
            }

            return AllowedStatus.Contains(status);
        }
    }
}
