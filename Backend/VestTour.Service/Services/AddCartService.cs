using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Implementation;
using VestTour.Service.Interfaces;

namespace VestTour.Services
{
    public class AddCartService : IAddCartService
    {
        private readonly VestTourDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IAddCartRepository _addCartRepository;
        private readonly IOrderService _orderService;
       // private readonly IProductStyleOptionervice _productStyleOptionService;
        private readonly IFabricRepository _fabricRepository;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddCartService(
            VestTourDbContext context,
            IProductRepository productRepository,
            IAddCartRepository addCartRepository,
            IOrderService orderService,
           // IProductStyleOptionervice productStyleOptionService,
            IFabricRepository fabricRepository,
            IProductService productService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _productRepository = productRepository;
            _orderService = orderService;
            _addCartRepository = addCartRepository;
           // _productStyleOptionService = productStyleOptionService;
            _fabricRepository = fabricRepository;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetOrCreateGuestId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context.Request.Cookies.ContainsKey("GuestId"))
            {
                return int.Parse(context.Request.Cookies["GuestId"]);
            }
            else
            {
                int guestId = GenerateGuestId();
                context.Response.Cookies.Append("GuestId", guestId.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(7) });
                return guestId;
            }
        }

        private int GenerateGuestId()
        {
            return new Random().Next(1000000, 9999999);
        }

        public async Task AddToCartAsync(int? userId, bool isCustom, int? productId = null, CustomProductModel customProduct = null)
        {
            var id = userId ?? GetOrCreateGuestId();
            CartItemModel cartItem;

            if (isCustom && customProduct != null)
            {
                if (string.IsNullOrEmpty(customProduct.ProductCode))
                {
                    customProduct.ProductCode= await customProduct.GenerateProductCodeAsync(_fabricRepository);
                }

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

            await _addCartRepository.AddToCartAsync(id, cartItem);
        }



        public async Task<CartModel> GetUserCartAsync(int? userId)
        {
            int id = userId ?? GetOrCreateGuestId();
            var cartItems = await _addCartRepository.GetUserCartAsync(id);
            return new CartModel { CartItems = cartItems };
        }
        public async Task RemoveFromCartAsync(int? userId, string productCode)
        {
            int id = userId ?? GetOrCreateGuestId();
            var cartItems = await _addCartRepository.GetUserCartAsync(id);

            // Find the item to remove
            var itemToRemove = cartItems.FirstOrDefault(c =>
                (c.IsCustom && c.CustomProduct.ProductCode == productCode) ||
                (!c.IsCustom && c.Product.ProductCode == productCode));

            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                await _addCartRepository.UpdateCartAsync(id, cartItems);
            }
        }
        public async Task ClearCartAsync(int? userId)
        {
            int id = userId ?? GetOrCreateGuestId();
            // Clear the user's cart
            await _addCartRepository.RemoveAllFromCartAsync(id);
        }


        public async Task DecreaseQuantityAsync(int? userId, string productCode)
        {
            int id = userId ?? GetOrCreateGuestId();
            var cartItems = await _addCartRepository.GetUserCartAsync(id);
            var item = cartItems.FirstOrDefault(c => (c.IsCustom && c.CustomProduct.ProductCode == productCode) ||
                                                      (!c.IsCustom && c.Product.ProductCode == productCode));
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }

            await _addCartRepository.UpdateCartAsync(id, cartItems);
        }

        public async Task IncreaseQuantityAsync(int? userId, string productCode)
        {
            int id = userId ?? GetOrCreateGuestId();
            var cartItems = await _addCartRepository.GetUserCartAsync(id);
            var item = cartItems.FirstOrDefault(c => (c.IsCustom && c.CustomProduct.ProductCode == productCode) ||
                                                      (!c.IsCustom && c.Product.ProductCode == productCode));
            if (item != null)
            {
                item.Quantity++;
            }

            await _addCartRepository.UpdateCartAsync(id, cartItems);
        }

        public async Task ConfirmOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, decimal? deposit, decimal? shippingFee, string? deliverymethod, int? voucherId , int? storeId)

        {
            int id = userId ?? GetOrCreateGuestId();
            var cartItems = await _addCartRepository.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }

            foreach (var item in cartItems)
            {
                // Kiểm tra nếu sản phẩm là tùy chỉnh (IsCustom = true)
                if (item.IsCustom)
                {
                    
                    var customProduct = item.CustomProduct;

                    var productToAdd = new ProductModel
                    {
                        ProductCode = customProduct.ProductCode, 
                        CategoryID = customProduct.CategoryID,
                        FabricID = customProduct.FabricID,
                        LiningID = customProduct.LiningID,
                        MeasurementID = customProduct.MeasurementID,
                        
                        Price = item.Price, 
                        IsCustom = true 
                    };

                    // Lưu sản phẩm vào bảng sản phẩm
                    var productId= await _productService.AddProductAsync(productToAdd);
                    item.Product = new ProductModel { ProductID = productId };
                    //await _productService.AddProductAsync(productToAdd);
                    foreach (var pickedOption in customProduct.PickedStyleOptions)
                    {
                        var productStyleOption = new
                        {
                            ProductId = productId,  // This should be the ID of the product just added
                            StyleOptionId = pickedOption.StyleOptionID // Assuming pickedOption has the StyleOptionId
                        };

                        // Add the entry directly into the join table
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
        INSERT INTO ProductStyleOption (ProductId, StyleOptionId) 
        VALUES ({productStyleOption.ProductId}, {productStyleOption.StyleOptionId})
    ");
                    }

                }
            }
            await _orderService.ConfirmCartOrderAsync(id,guestName,guestEmail,guestAddress,deposit,shippingFee,deliverymethod,voucherId,storeId);
            await _addCartRepository.RemoveAllFromCartAsync(id);
        }

        public async Task<decimal> GetTotalPriceAsync(int? userId)
        {
            int id = userId ?? GetOrCreateGuestId();
            var cart = await GetUserCartAsync(id);
            return cart.CartItems.Sum(item => item.Price * item.Quantity);
        }

        private async Task<decimal> CalculatePrice(CustomProductModel customProduct)
        {
            decimal basePrice = 100;
            decimal customizationCost = 0;

            decimal? fabricPrice = await _fabricRepository.GetFabricPriceByIdAsync(customProduct.FabricID);
            if (fabricPrice.HasValue)
            {
                customizationCost += fabricPrice.Value;
            }
            customizationCost += customProduct.PickedStyleOptions.Count * 5;

            return basePrice + customizationCost;
        }
    }
}
