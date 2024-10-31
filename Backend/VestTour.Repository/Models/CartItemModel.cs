namespace VestTour.Repository.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public CustomProductModel CustomProduct { get; set; } // Only populated for custom products
        public ProductModel Product { get; set; }             // Only populated for regular products
        public int? ProductID => IsCustom ? null : Product?.ProductID;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsCustom { get; set; } // True for custom products, false for regular products
    }
}