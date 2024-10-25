using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{
    public class ShippingFeeRequestModel
    {
        public int ServiceId { get; set; }
        public int InsuranceValue { get; set; }
        public string Coupon { get; set; }
        public string ToWardCode { get; set; }
        public int ToDistrictId { get; set; }
        public int FromDistrictId { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ShopId { get; set; }
    }
}
