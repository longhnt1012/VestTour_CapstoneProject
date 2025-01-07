using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class OrderDetailModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
    }
}
