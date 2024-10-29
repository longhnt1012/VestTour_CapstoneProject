using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class InventoryModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdate { get; set; }
    }

}
