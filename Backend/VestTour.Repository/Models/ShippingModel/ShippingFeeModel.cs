using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models.ShippingModel
{
    public class ShippingFeeModel
    {
        public int Total { get; set; }
        public int ServiceFee { get; set; }
        public int InsuranceFee { get; set; }
        public int PickStationFee { get; set; }
        public int CouponValue { get; set; }
        public int R2sFee { get; set; }
        public int ReturnAgain { get; set; }
        public int DocumentReturn { get; set; }
        public int DoubleCheck { get; set; }
        public int CodFee { get; set; }
        public int PickRemoteAreasFee { get; set; }
        public int DeliverRemoteAreasFee { get; set; }
        public int CodFailedFee { get; set; }
    }
}
