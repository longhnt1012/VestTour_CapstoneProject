using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class AddToCartRequestModel
    {
        public int UserId { get; set; }
        public bool IsCustom { get; set; }
        public int? ProductId { get; set; } // For regular products
        public CustomProductModel CustomProduct { get; set; } // For custom products
    }

}
