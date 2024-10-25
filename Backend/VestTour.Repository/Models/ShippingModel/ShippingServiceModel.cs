using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{
    public class ShippingServiceModel
    {
        public int ServiceId { get; set; }
        public string ShortName { get; set; }
        public int ServiceTypeId { get; set; }
        public string ConfigFeeId { get; set; }
        public string ExtraCostId { get; set; }
        public string StandardConfigFeeId { get; set; }
        public string StandardExtraCostId { get; set; }
        public int EcomConfigFeeId { get; set; }
        public int EcomExtraCostId { get; set; }
        public int EcomStandardConfigFeeId { get; set; }
        public int EcomStandardExtraCostId { get; set; }
    }

}
