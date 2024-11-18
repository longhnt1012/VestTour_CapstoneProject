using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.ValidationHelper
{
    public class StageNameValidate
    {
        private static readonly HashSet<string> AllowedStages = new HashSet<string>
    {
        "Make Sample", "Fix", "Finish"
    };

        
        public static bool IsValidStage(string stage)
        {
            return !string.IsNullOrEmpty(stage) && AllowedStages.Contains(stage);
        }
    }
}
