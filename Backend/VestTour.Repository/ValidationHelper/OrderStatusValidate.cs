using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class OrderStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
        {
            "Pending", "Processing", "Finish","Cancel","Ready"
        };


        public static bool IsValidOrderStatus(string status)
        {
          return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }
}
