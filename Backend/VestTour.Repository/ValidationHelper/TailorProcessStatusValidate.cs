using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class TailorProcessStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
        {
            "Start","Doing", "Finish", "Due","Cancel"
        };


        public static bool IsValidProcessStatus(string status)
        {
            return !string.IsNullOrEmpty(status) && AllowedStatus.Contains(status);
        }
    }
}
