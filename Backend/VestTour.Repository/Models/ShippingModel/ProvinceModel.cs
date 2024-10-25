using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{


    public class ProvinceModel
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CountryID { get; set; }
        public string Code { get; set; }
        public List<string> NameExtension { get; set; }
        public bool IsEnable { get; set; }
        public int RegionID { get; set; }
        public string UpdatedAt { get; set; }
        // Add any other fields from your JSON response
    }

}
