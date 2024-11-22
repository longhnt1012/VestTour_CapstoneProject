using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class DeliveryMethodValidate
    {
        private static readonly HashSet<string> AllowedDeliveryMethods = new HashSet<string>
    {
        "Pick up","Delivery"
    };

        // Method to validate the service type
        public static bool IsValidDeliveryMethod(string method)
        {
            return !string.IsNullOrEmpty(method) && AllowedDeliveryMethods.Contains(method);
        }
    }
}
