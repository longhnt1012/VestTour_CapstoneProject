﻿namespace VestTour.Repository.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal => Price * Quantity;
    }
}
