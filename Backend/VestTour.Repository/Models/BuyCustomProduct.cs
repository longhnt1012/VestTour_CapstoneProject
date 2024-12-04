using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class BuyCustomProduct
    {
        public int FabricID { get; set; }
        public int? LiningID { get; set; }
        public int? MeasurementID { get; set; }
        public int Quantity { get; set; }
        public List<PickedStyleOptionModel> PickedStyleOptions { get; set; } = new List<PickedStyleOptionModel>();
    }
}
