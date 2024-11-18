using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ProductQuantityModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
