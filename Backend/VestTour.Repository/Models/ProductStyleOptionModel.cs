using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Models
{
    public class ProductStyleOptionModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }  // Navigation property

        public int StyleOptionId { get; set; }
        public StyleOption StyleOption { get; set; }  // Navigation property
    }
}
