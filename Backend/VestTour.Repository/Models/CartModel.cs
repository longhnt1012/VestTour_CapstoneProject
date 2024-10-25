namespace VestTour.Repository.Models
{
    public class CartModel
    {
        
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

       public decimal? CartTotal => CartItems.Sum(item => item.Price*item.Quantity);
    }
}
