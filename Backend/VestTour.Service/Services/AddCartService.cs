using VestTour.Repository.Implementation;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Services
{
    public class AddCartService : IAddCartService
    {
        private readonly IAddCartRepository _addCartRepository;
        private readonly IProductService _productService;
        private readonly IFabricRepository _fabricRepository;
        public AddCartService(IAddCartRepository addCartRepository, IProductService productService,IFabricRepository fabricRepository)
        {
            _addCartRepository = addCartRepository;
            _productService = productService;
            _fabricRepository = fabricRepository;
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

        public async Task AddToCartAsync(int userId, CustomProductModel customProduct)
        {
            // Assume price calculation or retrieval is handled in the service layer
            decimal price = await CalculatePrice(customProduct); // Method to calculate price based on customization

            var cartItem = new CartItemModel
            {
                CustomProduct = customProduct,
                Price = price,
                Quantity = 1
            };

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
            var item = cartItems.FirstOrDefault(c => c.CustomProduct.ProductCode == productCode); // Or any unique identifier

            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }

            await _addCartRepository.UpdateCartAsync(userId, cartItems);
        }

        public async Task IncreaseQuantityAsync(int userId, string productCode)
        {
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            var item = cartItems.FirstOrDefault(c => c.CustomProduct.ProductCode == productCode); // Or any unique identifier

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
                                    // Bạn có thể thêm các thuộc tính khác nếu cần
                };

                // Lưu sản phẩm vào bảng sản phẩm
                await _productService.AddProductAsync(productToAdd);
                //foreach (var pickedOption in customProduct.PickedStyleOptions)
                //{
                //    await _productService.AddStyleOptionToProductAsync(productId, pickedOption.StyleOptionID);
                //}
            }

            // Xóa các sản phẩm đã được xác nhận khỏi giỏ hàng
            await _addCartRepository.RemoveAllFromCartAsync(userId); // Bạn cần thêm phương thức này trong AddCartRepository
        }


    }
}
