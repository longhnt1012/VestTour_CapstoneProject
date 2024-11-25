using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class VoucherStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
        {
            "Pending","OnGoing","Expired"
        };


        public static bool IsValidVoucherStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }
}
