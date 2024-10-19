namespace VestTour.Repository.Models
{
    public class CartModel
    {
        
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

        // Property to calculate total price of the cart
        public decimal CartTotal => CartItems.Sum(item => item.PriceTotal);
    }
}
