using Google.Apis.Storage.v1.Data;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Helpers;
using VestTour.Repository.Repositories;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;
using VestTour.Repository.Constants;
using VestTour.Repository.Interfaces;
using VestTour.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VestTour.Repository.Data;

namespace VestTour.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailHelper _emailHelper;
        private readonly IUserService _userService;
        private readonly IAddCartRepository _cartRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPaymentService _paymentService;
        private readonly IVoucherService _voucherService;
        private readonly IStoreService _storeService;
        private readonly IProductRepository _productRepository;
        private readonly IFabricRepository _fabricRepository;
        private readonly VestTourDbContext _context;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMeasurementService _measurementService;
        IProductInStoreService _productInStoreService;
        public OrderService(IProductInStoreService productInStoreService,IMeasurementService measurementService,IMeasurementRepository measurementRepository,VestTourDbContext context, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IEmailHelper emailHelper, IUserService userService, IAddCartRepository cartRepo, IPaymentService paymentService, IVoucherService voucherService, IStoreService storeService, IProductRepository productRepository, IFabricRepository fabricRepository)
        {
            _context = context;
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _emailHelper = emailHelper;
            _userService = userService;
            _cartRepo = cartRepo;
            _voucherService = voucherService;
            _storeService = storeService;
            _productRepository = productRepository;
            _fabricRepository = fabricRepository;
            _measurementRepository = measurementRepository;
            _measurementService = measurementService;
            _productInStoreService = productInStoreService;
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrderAsync();
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Order not found.");
            return order;
        }

        public async Task<int> CreateOrderAsync(OrderModel order)
        {
            // Validate the order status
            if (!OrderStatusValidate.IsValidOrderStatus(order.Status ?? "Pending"))
            {
                throw new ArgumentException($"Invalid order status: {order.Status}. Allowed values are Pending, Processing, Finish, Cancel and Ready.");
            }
            User? user = order.UserID != null ? await _userService.GetUserByIdAsync(order.UserID.Value) : null;
            if (!DeliveryMethodValidate.IsValidDeliveryMethod(order.DeliveryMethod))
            {
                throw new ArgumentException($"Invalid delivery method: {order.DeliveryMethod}. Allowed values are 'Pick up' and 'Delivery'.");
            }

            var newOrder = new OrderModel
            {
                UserID = order.UserID,
                StoreId = order.StoreId,
                VoucherId = order.VoucherId,
                ShipperPartnerId = order.ShipperPartnerId,
                OrderDate = order.OrderDate,
                ShippedDate = order.ShippedDate,
                Note = order.Note,
                Paid = order.Paid,
                Status = string.IsNullOrEmpty(order.Status) ? "Pending" : order.Status,
                TotalPrice = order.TotalPrice,
                RevenueShare = order.TotalPrice * 0.3m,
                Deposit = order.Deposit,
                ShippingFee = order.ShippingFee,
                GuestName = string.IsNullOrEmpty(order.GuestName) ? user?.Name : order.GuestName,
                GuestEmail = string.IsNullOrEmpty(order.GuestEmail) ? user?.Email : order.GuestEmail,
                GuestAddress = string.IsNullOrEmpty(order.GuestAddress) ? user?.Address : order.GuestAddress,
                DeliveryMethod = order.DeliveryMethod
            };

            var subject = "Order Confirmation";
            var body = new StringBuilder();
            body.AppendLine($"Dear Customer,");
            body.AppendLine();
            body.AppendLine($"Thank you for your order! Your Order ID is: {order.OrderId}.");
            body.AppendLine();
            body.AppendLine($"Order Details:");

            body.AppendLine($"- User ID: {order.UserID}");
            body.AppendLine($"- Store ID: {order.StoreId}");
            body.AppendLine($"- Voucher ID: {order.VoucherId}");
            body.AppendLine($"- Shipper Partner ID: {order.ShipperPartnerId}");
            body.AppendLine($"- Order Date: {order.OrderDate?.ToString("d")}");
            body.AppendLine($"- Shipped Date: {order.ShippedDate?.ToString("d")}");

            body.AppendLine();

            // List the products in the order
            body.AppendLine($"\nProducts in your order:");
            foreach (var product in newOrder.Products)
            {

                body.AppendLine($"-- Product Code: {product.ProductCode}");
                body.AppendLine($"-- Size: {product.Size}");
                body.AppendLine($"-- Custom: {(product.IsCustom ? "Yes" : "No")}");
                body.AppendLine($"-- Price: {product.Price:C}");
                if (!string.IsNullOrEmpty(product.ImgURL))
                {
                    body.AppendLine($"-- Image: {product.ImgURL}");
                }
                body.AppendLine(); // Add a blank line for spacing
            }
            body.AppendLine($"- Note: {order.Note}");
            body.AppendLine($"- Paid: {(order.Paid ? "Yes" : "No")}");
            body.AppendLine($"- Status: {order.Status}");
            body.AppendLine($"- Total Price: {newOrder.TotalPrice:C}");
            body.AppendLine($"-Shipping Fee:{order.ShippingFee}");
            body.AppendLine($"-Deposit:{order.Deposit}");
            body.AppendLine($"-Balance Payment: {order.BalancePayment}");
            body.AppendLine($"-Delivery Method: {order.DeliveryMethod}");
            body.AppendLine("We appreciate your business and look forward to serving you again!");
            body.AppendLine("Best regards,");
            body.AppendLine("Matcha VestTailor Team");

            string? userEmail = await _userService.GetEmailByUserIdAsync(order.UserID);

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new Exception("User email not found.");
            }

            // Now `userEmail` contains the email address as a string


            // Create an email request object
            var emailRequest = new EmailRequest
            {
                To = userEmail,
                Subject = subject,
                Content = body.ToString(),
                // IsHtml = true
            };


            // Send confirmation email
            await _emailHelper.SendEmailAsync(emailRequest);

            return await _orderRepository.AddOrderAsync(newOrder);
        }

        public async Task UpdateOrderAsync(int id, OrderModel order)
        {
            if (!OrderStatusValidate.IsValidOrderStatus(order.Status ?? "Pending"))
            {
                throw new ArgumentException($"Invalid order status: {order.Status}. Allowed values are Pending, Processing, Finish, and Cancel.");
            }

            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }
            await _orderRepository.UpdateOrderAsync(id, order);
        }
        public async Task<int> GetTotalOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();
            return orders.Count;
        }

        public async Task<int> GetTotalOrdersByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return order != null ? 1 : 0;
        }

        public async Task<List<OrderModel>> GetOrdersByStoreIdAsync(int storeId)
        {
            var allOrders = await _orderRepository.GetAllOrderAsync();
            return allOrders.FindAll(o => o.StoreId == storeId);
        }
        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(int userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<OrderModel?> GetOrderDetailByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderDetailByIdAsync(orderId);
        }
        public async Task ConfirmCartOrderAsync(int? userId, string? guestName , string? guestEmail , string? guestAddress, decimal deposit , decimal shippingFee, string? deliveryMethod, int storeId, int? voucherId )
        {
            // Generate a guest ID if userId is null
            int id = userId ?? GenerateGuestId();

            // Retrieve cart items for the user or guest
            var cartItems = await _cartRepo.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }
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

            // Validate delivery method
            //if (!DeliveryMethodValidate.IsValidDeliveryMethod(deliveryMethod))
            //{
            //    throw new ArgumentException($"Invalid delivery method: {deliveryMethod}. Allowed values are 'Pick up' and 'Delivery'.");
            //}

            // Calculate total price
            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            // Apply voucher discount if voucherId is provided
            if (voucherId.HasValue)
            {
                var voucher = await _voucherService.GetVoucherByIdAsync(voucherId.Value);
                if (voucher == null || !voucher.Success || voucher.Data == null)
                {
                    throw new ArgumentException("Invalid or expired voucher.");
                }

                // Calculate the discount
                decimal discountAmount = voucher.Data.DiscountNumber ?? 0;
                var newshippingFee = shippingFee - shippingFee * discountAmount;
                shippingFee = newshippingFee;
                if (shippingFee < 0)
                {
                    shippingFee = 0; // Ensure total price is non-negative
                }
            }
            // Retrieve user details if applicable
            User? user = userId.HasValue ? await _userService.GetUserByIdAsync(userId.Value) : null;

            // Create new order
            var newOrder = new OrderModel
            {
                UserID = user?.UserId,
                VoucherId = voucherId,
                StoreId = storeId,
                GuestName = string.IsNullOrEmpty(guestName) ? user?.Name : guestName,
                GuestEmail = string.IsNullOrEmpty(guestEmail) ? user?.Email : guestEmail,
                GuestAddress = string.IsNullOrEmpty(guestAddress) ? user?.Address : guestAddress,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                TotalPrice = Math.Round(totalPrice, 2),
                RevenueShare = totalPrice * 0.3m,
                Deposit = deposit,
                ShippingFee = shippingFee,
                Paid = false,
                Status = "Pending",
                DeliveryMethod = deliveryMethod ??= "Pick up",
                Note = surchargeNote
            };


            // Save the order to the database
            int orderId = await _orderRepository.AddOrderAsync(newOrder);

            // Retrieve PaymentId from session using IHttpContextAccessor
            //var paymentId = _httpContextAccessor.HttpContext?.Session.GetInt32("PaymentId");

            //if (paymentId == null)
            //{
            //    throw new InvalidOperationException("Please come back to payment.");
            //}

            //await _paymentService.UpdatePaymentOrderIdAsync(paymentId.Value, orderId);
            //_httpContextAccessor.HttpContext?.Session.Remove("PaymentId");
            //_httpContextAccessor.HttpContext?.Session.Remove("tongtien");

            // Add order details from cart items
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
                    .AppendLine($"- Note: {newOrder.Note:C}")
                    .AppendLine($"- Shipping Fee: {newOrder.ShippingFee:C}")
                    .AppendLine($"- Deposit: {newOrder.Deposit:C}")
                    .AppendLine($"- Balance Payment: {newOrder.BalancePayment:C}")
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
            await _cartRepo.RemoveAllFromCartAsync(id);
        }


        private int GenerateGuestId()
        {
            // Logic to generate a guest ID, similar to what's in AddCartService
            return new Random().Next(1000000, 9999999);
        }
        public async Task<ServiceResponse> ChangeOrderStatusAsync(int orderId, string newStatus)
        {
            var response = new ServiceResponse();

            if (orderId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidOrderId;
                return response;
            }

            if (!OrderStatusValidate.IsValidOrderStatus(newStatus))
            {
                response.Success = false;
                response.Message = Error.InvalidOrderStatus;
                return response;
            }

            try
            {
                var existingOrder = await _orderRepository.GetOrderByIdAsync(orderId);

                if (existingOrder == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.OrderNotFound}: {orderId}";
                    return response;
                }
                await _orderRepository.ChangeStatusAsync(orderId, newStatus);

                response.Success = true;
                response.Message = Success.OrderStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
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

      


        public async Task<int> CreateOrderForCustomerAsync(AddOrderForCustomer orderRequest)
        {
            if ((orderRequest.Products == null || !orderRequest.Products.Any()) &&
     (orderRequest.CustomProducts == null || !orderRequest.CustomProducts.Any()))
            {
                throw new ArgumentException("At least one product (custom or non-custom) must be provided.");
            }

            decimal totalPrice = 0;
            var customProductIds = new List<int>(); // List to store custom product IDs

            // Process custom products
            foreach (var customProduct in orderRequest.CustomProducts)
            {
                string productCode = await customProduct.GenerateProductCodeAsync(_fabricRepository);
                customProduct.SetProductCode(productCode);
                decimal price = await CalculatePrice(customProduct);
                var customProductEntity = new ProductModel
                {
                    ProductCode = productCode,
                    CategoryID = 5,
                    FabricID = customProduct.FabricID,
                    LiningID = customProduct.LiningID,
                    MeasurementID = customProduct.MeasurementID,
                    IsCustom = true,
                    Price = price*customProduct.Quantity // Total price of customizations
                };
                
                totalPrice += (customProductEntity.Price ?? 0) * customProduct.Quantity; // Multiply by quantity

                var productId = await _productRepository.AddProductAsync(customProductEntity);
                customProductIds.Add(productId); // Save the product ID to the list

                foreach (var styleOption in customProduct.PickedStyleOptions)
                {
                    var productStyleOption = new
                    {
                        ProductId = productId,
                        StyleOptionId = styleOption.StyleOptionID
                    };

                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO ProductStyleOption (ProductId, StyleOptionId) 
                VALUES ({productStyleOption.ProductId}, {productStyleOption.StyleOptionId})
            ");
                }
            }

            // Process non-custom products
            foreach (var product in orderRequest.Products)
            {
                totalPrice += (product.Price ?? 0) * product.Quantity; // Multiply by quantity
            }

            // Insert Order
            var orderEntity = new OrderModel
            {
                UserID = orderRequest.UserID,
                StoreId = orderRequest.StoreId,
                VoucherId = orderRequest.VoucherId,
                ShipperPartnerId = 4,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                ShippedDate = orderRequest.ShippedDate,
                Note = orderRequest.Note,
                Paid = orderRequest.Paid,
                Status = "Pending",
                GuestName = orderRequest.GuestName,
                GuestEmail = orderRequest.GuestEmail,
                GuestAddress = orderRequest.GuestAddress,
                TotalPrice = totalPrice,
                RevenueShare = totalPrice * 0.7m,
                Deposit = orderRequest.Deposit,
                ShippingFee = orderRequest.ShippingFee,
                DeliveryMethod = orderRequest.DeliveryMethod
            };

            int orderId = await _orderRepository.AddOrderAsync(orderEntity);

            // Insert Order Details for non-custom products
            foreach (var product in orderRequest.Products)
            {
                var productInStore = await _productInStoreService.GetProductInStoreAsync(orderRequest.StoreId ?? 0, product.ProductID);
                if (productInStore.Data == null || productInStore.Data.Quantity < product.Quantity)
                {
                    throw new InvalidOperationException($"Product with ID {product.ProductID} does not have enough stock.");
                }

                // Cập nhật số lượng sản phẩm trong kho
                var updatedQuantity = productInStore.Data.Quantity - product.Quantity;
                await _productInStoreService.UpdateQuantityAsync(orderRequest.StoreId ?? 0, product.ProductID, updatedQuantity);

                var storeProduct = await _productRepository.GetProductByIdAsync(product.ProductID);
                var orderDetail = new OrderDetailModel
                {
                    OrderId = orderId,
                    ProductId = product.ProductID,
                    Quantity = product.Quantity,
                    Price = storeProduct.Price.Value * product.Quantity // Multiply by quantity
                };

                await _orderRepository.AddOrderDetailAsync(orderDetail.OrderId,orderDetail.ProductId,orderDetail.Quantity,orderDetail.Price);
            }

            // Insert Order Details for custom products
            foreach (var customProductId in customProductIds)
            {
               // var productCode = await _productRepository.GetProductCodeByIdAsync(customProductId);
                var productcustom = await _productRepository.GetProductByIdAsync(customProductId);

                var customOrderDetail = new OrderDetailModel
                {
                    OrderId = orderId,
                    ProductId = customProductId,
                    Quantity = orderRequest.CustomProducts
                        .First(cp => cp.ProductCode == productcustom.ProductCode).Quantity, // Use the quantity from the request
                    Price = (productcustom.Price ?? 0) * orderRequest.CustomProducts
                        .First(cp => cp.ProductCode == productcustom.ProductCode).Quantity // Multiply by quantity
                };

                await _orderRepository.AddOrderDetailAsync(customOrderDetail.OrderId, customOrderDetail.ProductId, customOrderDetail.Quantity, customOrderDetail.Price);
            }

            // Send email confirmation
            var emailContent = new StringBuilder();
            emailContent.AppendLine("Thank you for your order!");
            emailContent.AppendLine($"Order ID: {orderId}");
            emailContent.AppendLine($"Order Date: {DateTime.Now}");
            emailContent.AppendLine($"Total Price: {totalPrice:C}");
            emailContent.AppendLine();
            emailContent.AppendLine("Order Details:");
            emailContent.AppendLine("--------------------------------------------------");

            // Add non-custom products to email
            if (orderRequest.Products != null && orderRequest.Products.Any())
            {
                emailContent.AppendLine("Regular Products:");
                foreach (var product in orderRequest.Products)
                {
                    emailContent.AppendLine($"- Product ID: {product.ProductID}");
                    emailContent.AppendLine($"  Quantity: {product.Quantity}");
                    emailContent.AppendLine($"  Price per Unit: {product.Price:C}");
                    emailContent.AppendLine($"  Subtotal: {(product.Price ?? 0) * product.Quantity:C}");
                    emailContent.AppendLine("--------------------------------------------------");
                }
            }

            // Add custom products to email
            if (orderRequest.CustomProducts != null && orderRequest.CustomProducts.Any())
            {
                emailContent.AppendLine("Custom Products:");
                foreach (var customProduct in orderRequest.CustomProducts)
                {
                    var productcustom = await _productRepository.GetProductByCodeAsync(customProduct.ProductCode);
                    emailContent.AppendLine($"- Product Code: {customProduct.ProductCode}");
                    emailContent.AppendLine($"  Fabric ID: {customProduct.FabricID}");
                    emailContent.AppendLine($"  Lining ID: {customProduct.LiningID}");
                    emailContent.AppendLine($"  Quantity: {customProduct.Quantity}");
                    emailContent.AppendLine($"  Customization Price per Unit: {productcustom.Price:C}");
                    emailContent.AppendLine($"  Subtotal: {(productcustom.Price ?? 0) * customProduct.Quantity:C}");
                    emailContent.AppendLine($"  Picked Style Options: {string.Join(", ", customProduct.PickedStyleOptions.Select(o => o.StyleOptionID))}");
                    emailContent.AppendLine("--------------------------------------------------");
                }
            }

            // Include summary and footer
            emailContent.AppendLine();
            emailContent.AppendLine("Thank you for shopping with us!");
            emailContent.AppendLine("If you have any questions, please contact our support team.");

            // Send email
            await _emailHelper.SendEmailAsync(new EmailRequest
            {
                To = orderRequest.GuestEmail ?? await _userService.GetEmailByUserIdAsync(orderRequest.UserID ?? 0),
                Subject = "Order Confirmation",
                Content = emailContent.ToString()
            });

            return orderId;
        }



    }

}
