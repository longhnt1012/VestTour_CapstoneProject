using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ProductWithQuantityModel
    {
        public ProductModel Product { get; set; }  // The product details
        public int Quantity { get; set; }           // The quantity of the product in ProductInStore
    }
}
