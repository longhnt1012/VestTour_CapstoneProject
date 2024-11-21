using System;
using System.Collections.Generic;

namespace VestTour.Repository.ValidationHelper
{
    public class StageStatusValidate
    {
        private static readonly HashSet<string> AllowedStatus = new HashSet<string>
        {
            "Doing", "Finish", "Due"
        };

        public static bool IsValidStageStatus(string status)
        {
            if (status == null)
            {
                return true;
            }

            return AllowedStatus.Contains(status);
        }
    }
}
