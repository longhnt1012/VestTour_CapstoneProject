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
        public OrderService(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IEmailHelper emailHelper, IUserService userService, IAddCartRepository cartRepo, IPaymentService paymentService, IVoucherService voucherService, IStoreService storeService)
        {
            _paymentService = paymentService;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _emailHelper = emailHelper;
            _userService = userService;
            _cartRepo = cartRepo;
            _voucherService = voucherService;
            _storeService = storeService;
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
                throw new ArgumentException($"Invalid order status: {order.Status}. Allowed values are Pending, Processing, Finish, and Cancel.");
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
        public async Task ConfirmCartOrderAsync(int? userId, string guestName , string guestEmail , string guestAddress, decimal deposit , decimal shippingFee, string deliveryMethod, int storeId, int? voucherId )
        {
            // Generate a guest ID if userId is null
            int id = userId ?? GenerateGuestId();

            // Retrieve cart items for the user or guest
            var cartItems = await _cartRepo.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
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
                Deposit = deposit,
                ShippingFee = shippingFee,
                Paid = false,
                Status = "Pending",
                DeliveryMethod = "Pick up"
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
    }
}
