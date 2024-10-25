namespace VestTour.Repository.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }           // Unique ID for the item in the cart
        public CustomProductModel CustomProduct { get; set; } // The customized product details

        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;

    }
}