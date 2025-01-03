using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Helpers;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Repository.Repositories;
using VestTour.Service.Implementation;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;

namespace VestTour.Services
{
    public class AddCartService : IAddCartService
    {
        private readonly VestTourDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IAddCartRepository _addCartRepository;
        private readonly IOrderService _orderService;
        private readonly IEmailHelper _emailHelper;
        private readonly IFabricRepository _fabricRepository;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
       private readonly IMeasurementRepository _measurementRepository;
        private readonly IMeasurementService _measurementService;
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherService _voucherService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly IProductInStoreService _productInStoreService;

        public AddCartService(
            IProductInStoreService productInStoreService,
            IEmailHelper emailHelper,
             IMeasurementService measurementService,
             VestTourDbContext context,
             IProductRepository productRepository,
             IAddCartRepository addCartRepository,
             IOrderService orderService,
            IMeasurementRepository measurementRepository,
             IFabricRepository fabricRepository,
             IProductService productService,
             IHttpContextAccessor httpContextAccessor,
             IOrderRepository orderRepository,
             IVoucherService voucherService,
             IUserService userService,
             IPaymentService paymentService)
        {
            _productInStoreService = productInStoreService;
            _userService = userService;
            _emailHelper = emailHelper; 
            _paymentService = paymentService;
            _orderRepository = orderRepository;
            _voucherService = voucherService;
            _measurementService = measurementService;
            _context = context;
            _productRepository = productRepository;
            _orderService = orderService;
            _addCartRepository = addCartRepository;
            _measurementRepository = measurementRepository;
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

            // Retrieve measurements and calculate surcharge
            decimal subFee = 0;
            string surchargeNote = string.Empty;
            if (userId.HasValue)
            {
                var measurementResponse = await _measurementRepository.GetMeasurementByUserIdAsync(userId.Value);
                if (measurementResponse != null)
                {
                    subFee = _measurementService.CalculateMeasurementSurcharge(measurementResponse);
                    if (subFee > 0)
                    {
                        surchargeNote = "An additional fee has been applied due to exceeding standard measurements.";
                    }
                }
            }

            if (isCustom && customProduct != null)
            {
                if (string.IsNullOrEmpty(customProduct.ProductCode))
                {
                    customProduct.ProductCode = await customProduct.GenerateProductCodeAsync(_fabricRepository);
                }

                decimal price = await CalculatePrice(customProduct);
                cartItem = new CartItemModel
                {
                    CustomProduct = customProduct,
                    Price = price + subFee,
                    Quantity = 1,
                    IsCustom = true,
                    Note = surchargeNote // Add the surcharge note if applicable
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
                    IsCustom = false,
                    Note = surchargeNote // Add the surcharge note if applicable
                };
            }
            else
            {
                throw new ArgumentException("Invalid product information");
            }

            await _addCartRepository.AddToCartAsync(id, cartItem);
        }
        public async Task<int> ConfirmOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, decimal deposit, decimal shippingFee, string? deliveryMethod, int storeId, int? voucherId)
        {
            // Generate a guest ID if userId is null
            int id = userId ?? GenerateGuestId();

            // Retrieve cart items for the user or guest
            var cartItems = await _addCartRepository.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }

            // Process custom products and save them to the database
            foreach (var item in cartItems)
            {
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

                    // Save custom product and retrieve its ID
                    var productId = await _productService.AddProductAsync(productToAdd);
                    item.Product = new ProductModel { ProductID = productId.Data };

                    // Save product style options to the join table
                    foreach (var pickedOption in customProduct.PickedStyleOptions)
                    {
                        await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    INSERT INTO ProductStyleOption (ProductId, StyleOptionId) 
                    VALUES ({productId}, {pickedOption.StyleOptionID})");
                    }
                }
                else
                {
                    var productInStore = await _productInStoreService.GetProductInStoreAsync(storeId, item.ProductID.Value);
                    if (productInStore.Data == null || productInStore.Data.Quantity < item.Quantity)
                    {
                        throw new InvalidOperationException($"Product with ID {item.ProductID} does not have enough stock.");
                    }

                    // Cập nhật số lượng sản phẩm trong kho
                    var updatedQuantity = productInStore.Data.Quantity - item.Quantity;
                    await _productInStoreService.UpdateQuantityAsync(storeId, item.ProductID.Value, updatedQuantity);
                }
            }

            User? user = null;
            decimal subFee = 0;
            string surchargeNote = string.Empty;

            if (userId.HasValue)
            {
                user = await _userService.GetUserByIdAsync(userId.Value);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with ID {userId.Value} not found.");
                }

                var measurementResponse = await _measurementRepository.GetMeasurementByUserIdAsync(userId.Value);
                if (measurementResponse != null)
                {
                    subFee = _measurementService.CalculateMeasurementSurcharge(measurementResponse);
                    if (subFee > 0)
                    {
                        surchargeNote = "An additional fee has been applied due to exceeding standard measurements.";
                    }
                }
            }
           if (voucherId.HasValue)
            {
                var voucher = await _voucherService.GetVoucherByIdAsync(voucherId.Value);
                if (voucher == null || !voucher.Success || voucher.Data == null)
                {
                    throw new ArgumentException("Invalid or expired voucher.");
                }

                decimal discountAmount = voucher.Data.DiscountNumber ?? 0;
                shippingFee -= shippingFee * discountAmount;
                if (shippingFee < 0) shippingFee = 0;
            }

            // Calculate total price
            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity) + shippingFee;

            var newOrder = new OrderModel
            {
                UserID = user?.UserId,
                VoucherId = voucherId,
                StoreId = storeId,
                ShipperPartnerId = 4,
                GuestName = string.IsNullOrEmpty(guestName) ? user?.Name : guestName,
                GuestEmail = string.IsNullOrEmpty(guestEmail) ? user?.Email : guestEmail,
                GuestAddress = string.IsNullOrEmpty(guestAddress) ? user?.Address : guestAddress,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                ShippedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                TotalPrice = Math.Round(totalPrice, 2),
                RevenueShare = totalPrice * 0.3m,
                Deposit = deposit,
                ShippingFee = shippingFee,
                Paid = true,
                Status = "Pending",
                DeliveryMethod = deliveryMethod ?? "Pick up",
                ShipStatus = "Confirming",
                Note = surchargeNote
            };
            // Save the order to the database
            int orderId = await _orderRepository.AddOrderAsync(newOrder);

            // Save order details
            await _orderRepository.AddOrderDetailsAsync(orderId, cartItems);

            // Send confirmation email
            string recipientEmail = user?.Email ?? guestEmail;
            if (!string.IsNullOrEmpty(recipientEmail))
            {
                var subject = "Order Confirmation";
                var body = new StringBuilder();
                body.AppendLine("Dear Customer,")
                    .AppendLine()
                    .AppendLine($"Thank you for your order! Your Order ID is: {orderId}.")
                    .AppendLine($"- Order Date: {newOrder.OrderDate?.ToString("d")}")
                    .AppendLine($"- Total Price: {newOrder.TotalPrice:C}")
                    .AppendLine($"- Note: {newOrder.Note}")
                    .AppendLine($"- Shipping Fee: {newOrder.ShippingFee:C}")
                    .AppendLine($"- Deposit: {newOrder.Deposit:C}")
                    .AppendLine($"- Delivery Method: {newOrder.DeliveryMethod}")
                    .AppendLine()
                    .AppendLine("Order Details:");

                foreach (var item in cartItems)
                {
                    string productCode = item.CustomProduct?.ProductCode ?? item.Product?.ProductCode ?? "Unknown Product";
                    body.AppendLine($"- Product: {productCode}")
                        .AppendLine($"  Price: {item.Price:C}")
                        .AppendLine($"  Quantity: {item.Quantity}")
                        .AppendLine($"  Subtotal: {(item.Price * item.Quantity):C}")
                        .AppendLine();
                }

                body.AppendLine("We appreciate your business and look forward to serving you again!")
                    .AppendLine()
                    .AppendLine("Best regards,")
                    .AppendLine("Matcha VestTailor Team");

                var emailRequest = new EmailRequest
                {
                    To = recipientEmail,
                    Subject = subject,
                    Content = body.ToString()
                };
                await _emailHelper.SendEmailAsync(emailRequest);
            }

            // Clear the cart
            await _addCartRepository.RemoveAllFromCartAsync(id);

            return orderId;
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
