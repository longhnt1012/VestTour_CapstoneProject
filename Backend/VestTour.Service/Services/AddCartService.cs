using VestTour.Domain.Entities;
using VestTour.Repository.Implementation;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Services
{
    public class AddCartService : IAddCartService
    {
        private readonly IAddCartRepository _addCartRepository;
        private readonly IProductStyleOptionervice _ProductStyleOptionervice;
        private readonly IFabricRepository _fabricRepository;
        private readonly IProductService _productService;
        public AddCartService(IAddCartRepository addCartRepository,IProductStyleOptionervice ProductStyleOptionervice,IFabricRepository fabricRepository,IProductService productService)
        {
            _addCartRepository = addCartRepository;
            _ProductStyleOptionervice = ProductStyleOptionervice;
            _fabricRepository = fabricRepository;
            _productService = productService;
        }
        private async Task<decimal> CalculatePrice(CustomProductModel customProduct)
        {
            decimal basePrice = 100; // Assume a base price
            decimal customizationCost = 0;

            // Get the price of the selected fabric
            decimal? fabricPrice = await _fabricRepository.GetFabricPriceByIdAsync(customProduct.FabricID);
            if (fabricPrice.HasValue)
            {
                customizationCost += fabricPrice.Value;
            }
            // Adding costs for picked style options
            customizationCost += customProduct.PickedStyleOptions.Count * 5; // Example cost per style option

            // Total price calculation
            return basePrice + customizationCost;
        }

        public async Task AddToCartAsync(int userId, bool isCustom, int? productId = null, CustomProductModel customProduct = null)
        {
            CartItemModel cartItem;

            if (isCustom && customProduct != null)
            {
                decimal price = await CalculatePrice(customProduct);
                cartItem = new CartItemModel
                {
                    CustomProduct = customProduct,
                    Price = price,
                    Quantity = 1,
                    IsCustom = true
                };
            }
            else if (!isCustom && productId.HasValue)
            {
                // Fetch the regular product details using productId
                var product = await _productService.GetProductByIdAsync(productId.Value);
                if (product == null) throw new ArgumentException("Product not found");

                cartItem = new CartItemModel
                {
                    Product = product,
                    Price = product.Price ?? 0,
                    Quantity = 1,
                    IsCustom = false
                };
            }
            else
            {
                throw new ArgumentException("Invalid product information");
            }

            await _addCartRepository.AddToCartAsync(userId, cartItem);
        }




        public async Task RemoveFromCartAsync(int userId, string productCode)
        {
            await _addCartRepository.RemoveFromCartAsync(userId, productCode);
        }

        public async Task<CartModel> GetUserCartAsync(int userId)
        {
            var UserID = userId;
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            return new CartModel { CartItems = cartItems };
        }

        public async Task DecreaseQuantityAsync(int userId, string productCode)
        {
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            var item = cartItems.FirstOrDefault(c => (c.IsCustom && c.CustomProduct.ProductCode == productCode) ||
                                                      (!c.IsCustom && c.Product.ProductCode == productCode));

            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }

            await _addCartRepository.UpdateCartAsync(userId, cartItems);
        }

        public async Task IncreaseQuantityAsync(int userId, string productCode)
        {
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            var item = cartItems.FirstOrDefault(c => (c.IsCustom && c.CustomProduct.ProductCode == productCode) ||
                                                      (!c.IsCustom && c.Product.ProductCode == productCode));

            if (item != null)
            {
                item.Quantity++;
            }

            await _addCartRepository.UpdateCartAsync(userId, cartItems);
        }

        public async Task ConfirmOrderAsync(int userId)
        {
            // Lấy các sản phẩm từ giỏ hàng của người dùng
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }

            foreach (var item in cartItems)
            {
                // Kiểm tra nếu sản phẩm là tùy chỉnh (IsCustom = true)
                if (item.IsCustom)
                {
                    // Tạo sản phẩm mới từ sản phẩm tùy chỉnh
                    var customProduct = item.CustomProduct;

                    // Tạo ProductModel từ CustomProductModel
                    var productToAdd = new ProductModel
                    {
                        ProductCode = customProduct.ProductCode, // Sản phẩm đã được tự động sinh
                        CategoryID = customProduct.CategoryID,
                        FabricID = customProduct.FabricID,
                        LiningID = customProduct.LiningID,
                        MeasurementID = customProduct.MeasurementID,
                        // Các thuộc tính khác của ProductModel cần được thiết lập
                        Price = item.Price, // Đặt giá từ CartItemModel
                        IsCustom = true // Đánh dấu sản phẩm là tùy chỉnh
                    };

                    // Lưu sản phẩm vào bảng sản phẩm
                    //var productId= await _productService.AddProductAsync(productToAdd);

                    // Thêm các tùy chọn phong cách cho sản phẩm
                   // foreach (var pickedOption in customProduct.PickedStyleOptions)
                   // {
                    //    await _ProductStyleOptionervice.AddStyleOptionAsync(productId, pickedOption.StyleOptionID);
                    //}
                }
            }

            // Xóa các sản phẩm đã được xác nhận khỏi giỏ hàng
            await _addCartRepository.RemoveAllFromCartAsync(userId); // Bạn cần thêm phương thức này trong AddCartRepository
        }



    }
}
