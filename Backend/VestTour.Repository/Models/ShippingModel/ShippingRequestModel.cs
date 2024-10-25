using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{
    public class ShippingRequestModel
    {
        public int ShopId { get; set; }
        public int FromDistrict { get; set; }
        public int ToDistrict { get; set; }
    }

}
