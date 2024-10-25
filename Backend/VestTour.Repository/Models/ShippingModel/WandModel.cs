using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{
    public class WardModel
    {
        public string WardCode { get; set; }
        public int DistrictID { get; set; }
        public string WardName { get; set; }
        public List<string> NameExtension { get; set; }
        public int IsEnable { get; set; }
        public bool CanUpdateCOD { get; set; }
       
      
        public int SupportType { get; set; }
        public int PickType { get; set; }
        public int DeliverType { get; set; }
       
       
    }


}
