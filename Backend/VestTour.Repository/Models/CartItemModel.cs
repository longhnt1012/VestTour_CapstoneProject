namespace VestTour.Repository.Models
{
    public class CartItemModel
    {
<<<<<<< Updated upstream
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal => Price * Quantity;
=======
        public int CartItemId { get; set; }           // Unique ID for the item in the cart
        public CustomProductModel CustomProduct { get; set; } // The customized product details

        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;

>>>>>>> Stashed changes
    }
}