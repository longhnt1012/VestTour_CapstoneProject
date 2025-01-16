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
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;
using VestTour.ValidationHelpers;

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
            

            if (isCustom && customProduct != null)
            {
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
                            surchargeNote = $"An additional fee of {subFee:C} per unit has been applied due to exceeding standard measurements.";
                        }
                    }
                }
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
                    IsCustom = false
                };
            }
            else
            {
                throw new ArgumentException("Invalid product information");
            }

            await _addCartRepository.AddToCartAsync(id, cartItem);
        }
        public async Task<ServiceResponse<int>> ConfirmOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, string? guestPhone, decimal deposit, decimal shippingFee, string? deliveryMethod, int storeId, int? voucherId,string? note)
        {
            try
            {
                // Generate a guest ID if userId is null
                int id = userId ?? GenerateGuestId();

                // Retrieve cart items for the user or guest
                var cartItems = await _addCartRepository.GetUserCartAsync(id);
                if (cartItems == null || cartItems.Count == 0)
                {
                    return new ServiceResponse<int> { Success = false, Message = "No items in cart to confirm." };
                }

                // Process cart items
                decimal subFee = 0;
                string surchargeNote = string.Empty;
                foreach (var item in cartItems)
                {
                    if (item.IsCustom)
                    {
                        var customProduct = item.CustomProduct;

                        var measurement = await _measurementRepository.GetMeasurementByUserIdAsync(userId.Value);
                        if (measurement != null)
                        {
                            subFee = _measurementService.CalculateMeasurementSurcharge(measurement);
                            if (subFee > 0)
                            {
                                surchargeNote = $"An additional fee of {subFee:C} per unit has been applied due to exceeding standard measurements.";
                            }
                        }
                        var productToAdd = new ProductModel
                        {
                            ProductCode = customProduct.ProductCode,
                            CategoryID = customProduct.CategoryID,
                            FabricID = customProduct.FabricID,
                            LiningID = customProduct.LiningID,
                            MeasurementID = customProduct.MeasurementID,
                            Price = item.Price+subFee,
                            IsCustom = true
                        };
                        
                       
                        var productIdResponse = await _productService.AddProductAsync(productToAdd);
                        if (!productIdResponse.Success)
                        {
                            return new ServiceResponse<int> { Success = false, Message = "Failed to add custom product." };
                        }

                        item.Product = new ProductModel { ProductID = productIdResponse.Data };

                        // Save product style options using EF Core
                        foreach (var pickedOption in customProduct.PickedStyleOptions)
                        {
                            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO ProductStyleOption (ProductId, StyleOptionId) 
                VALUES ({productIdResponse.Data}, {pickedOption.StyleOptionID})");
                        }
                    }
                
                    else
                    {
                        // Validate stock for regular products
                        var productInStoreResponse = await _productInStoreService.GetProductInStoreAsync(storeId, item.ProductID.Value);
                        if (!productInStoreResponse.Success || productInStoreResponse.Data == null || productInStoreResponse.Data.Quantity < item.Quantity)
                        {
                            return new ServiceResponse<int> { Success = false, Message = $"Product with ID {item.ProductID} does not have enough stock." };
                        }

                        // Update product stock
                        var updatedQuantity = productInStoreResponse.Data.Quantity - item.Quantity;
                        await _productInStoreService.UpdateQuantityAsync(storeId, item.ProductID.Value, updatedQuantity);
                    }
                }

                // Calculate additional charges and validate voucher
               

                if (userId.HasValue)
                {
                    var user = await _userService.GetUserByIdAsync(userId.Value);
                    guestName ??= user.Name;
                    guestEmail ??= user.Email;
                    guestAddress ??= user.Address;
                    guestPhone ??= user.Phone;
                    if (user == null)
                    {
                        return new ServiceResponse<int> { Success = false, Message = $"User with ID {userId.Value} not found." };
                    }

                    
                }
                decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);
                decimal newShippingFee = shippingFee;
                decimal newPrice = totalPrice;

                if (voucherId.HasValue)
                {
                    var voucherResponse = await _voucherService.GetVoucherByIdAsync(voucherId.Value);
                    if (!voucherResponse.Success || voucherResponse.Data == null)
                    {
                        return new ServiceResponse<int> { Success = false, Message = "Invalid voucher." };
                    }

                    string voucherCode = voucherResponse.Data.VoucherCode;

                    if (StringValidate.IsValidVoucherCode(voucherCode))
                    {
                        decimal discount = voucherResponse.Data.DiscountNumber.Value; // Convert discount number to percentage

                        if (voucherCode.StartsWith("FREESHIP", StringComparison.OrdinalIgnoreCase))
                        {
                            newShippingFee = shippingFee * (1 - discount);
                        }
                        else if (voucherCode.StartsWith("BIGSALE", StringComparison.OrdinalIgnoreCase))
                        {
                            newPrice = totalPrice * (1 - discount);
                        }
                    }
                    else
                    {
                        return new ServiceResponse<int> { Success = false, Message = "Voucher code is invalid." };
                    }
                }

                // Calculate the new total price
                decimal newTotalPrice = Math.Round(newPrice + newShippingFee, 2); // Round to 2 decimal places

                var formattedNote = note ?? string.Empty;
                if (!string.IsNullOrEmpty(surchargeNote) )
                {
                    formattedNote +="  |  " + surchargeNote;
                }
                // Create order
                var newOrder = new OrderModel
                {
                    UserID = userId,
                    VoucherId = voucherId,
                    StoreId = storeId,
                    ShipperPartnerId = 4,
                    GuestName = guestName,
                    GuestEmail = guestEmail,
                    GuestAddress = guestAddress,
                    GuestPhone = guestPhone,
                    OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    ShippedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5)),
                    TotalPrice = Math.Round(newTotalPrice, 2),
                    RevenueShare = newTotalPrice * 0.3m,
                    Deposit = deposit,
                    ShippingFee = newShippingFee,
                    Paid = false,
                    Status = "Pending",
                    DeliveryMethod = deliveryMethod ?? "Pick up",
                    ShipStatus = "Confirming",
                    Note = formattedNote
                };

                var orderResponse = await _orderService.AddOrderAsync(newOrder);
                if (!orderResponse.Success)
                {
                    return new ServiceResponse<int> { Success = false, Message = "Failed to save the order." };
                }

                int orderId = orderResponse.Data;

                // Save order details
                await _orderRepository.AddOrderDetailsAsync(orderId, cartItems);

                // Send confirmation email
                // Generate email content
                var emailContent = new StringBuilder();
                emailContent.AppendLine("Thank you for your order!");
                emailContent.AppendLine($"Order ID: {orderId}");
                emailContent.AppendLine($"Order Date: {DateTime.Now}");
                emailContent.AppendLine($"Total Price: {newOrder.TotalPrice:C}");
                emailContent.AppendLine();
                emailContent.AppendLine("Order Details:");
                emailContent.AppendLine("--------------------------------------------------");

                // Add regular products to email
                if (cartItems.Any(item => !item.IsCustom))
                {
                    emailContent.AppendLine("Regular Products:");
                    foreach (var item in cartItems.Where(i => !i.IsCustom))
                    {
                        emailContent.AppendLine($"- Product ID: {item.Product.ProductID}");
                        emailContent.AppendLine($"  Product Name: {item.Product.ProductCode}");
                        emailContent.AppendLine($"  Quantity: {item.Quantity}");
                        emailContent.AppendLine($"  Price per Unit: {item.Product.Price:C}");
                        emailContent.AppendLine($"  Subtotal: {item.Quantity * (item.Product.Price ?? 0):C}");
                        emailContent.AppendLine("--------------------------------------------------");
                    }
                }

                // Add custom products to email
                if (cartItems.Any(item => item.IsCustom))
                {
                    emailContent.AppendLine("Custom Products:");
                    foreach (var item in cartItems.Where(i => i.IsCustom))
                    {
                        var customProduct = item.CustomProduct;
                        emailContent.AppendLine($"- Product Code: {customProduct.ProductCode}");
                        emailContent.AppendLine($"  Fabric ID: {customProduct.FabricID}");
                        emailContent.AppendLine($"  Lining ID: {customProduct.LiningID}");
                        emailContent.AppendLine($"  Quantity: {item.Quantity}");
                        emailContent.AppendLine($"  Customization Price per Unit: {item.Price:C}");
                        emailContent.AppendLine($"  Subtotal: {item.Quantity * item.Price:C}");
                        emailContent.AppendLine($"  Picked Style Options: {string.Join(", ", customProduct.PickedStyleOptions.Select(o => o.StyleOptionID))}");
                        emailContent.AppendLine("--------------------------------------------------");
                    }

                    if (subFee > 0)
                    {
                        emailContent.AppendLine("--------------------------------------------------");
                        emailContent.AppendLine("Additional Surcharge:");
                        emailContent.AppendLine(surchargeNote);
                        emailContent.AppendLine($"Surcharge Amount: {subFee:C} per unit for custom products exceeding standard measurements.");
                        emailContent.AppendLine("--------------------------------------------------");
                    }
                }

                // Include footer
                emailContent.AppendLine();
                emailContent.AppendLine("Thank you for shopping with us!");
                emailContent.AppendLine("If you have any questions, please contact our support team.");

                // Send the email
                if (!string.IsNullOrEmpty(guestEmail))
                {
                    var emailRequest = new EmailRequest
                    {
                        To = guestEmail,
                        Subject = "Order Confirmation",
                        Content = emailContent.ToString()
                    };
                    await _emailHelper.SendEmailAsync(emailRequest);
                }
                // Clear the cart
                await _addCartRepository.RemoveAllFromCartAsync(id);

                return new ServiceResponse<int> { Success = true, Data = orderId, Message = "Order confirmed successfully." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int> { Success = false, Message = ex.Message };
            }
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
            decimal basePrice = 0;
            decimal customizationCost = 0;

            decimal? fabricPrice = await _fabricRepository.GetFabricPriceByIdAsync(customProduct.FabricID);
            if (fabricPrice.HasValue)
            {
                customizationCost += fabricPrice.Value;
            }

            return basePrice + customizationCost;
        }
    }
}
