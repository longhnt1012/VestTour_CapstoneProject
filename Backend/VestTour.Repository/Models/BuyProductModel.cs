﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class BuyProductModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}