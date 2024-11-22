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

namespace VestTour.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailHelper _emailHelper;
        private readonly IUserService _userService;
        private readonly IAddCartRepository _cartRepo;

        public OrderService(IOrderRepository orderRepository, IEmailHelper emailHelper, IUserService userService, IAddCartRepository cartRepo)
        {
            _orderRepository = orderRepository;
            _emailHelper = emailHelper;
            _userService = userService;
            _cartRepo= cartRepo;
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
                Status = order.Status ?? "Pending",
                TotalPrice = order.TotalPrice,
                Deposit= order.Deposit,
                ShippingFee= order.ShippingFee,
                GuestName =  order.GuestName ?? user?.Name,
                GuestEmail = order.GuestEmail ?? user?.Email,
                GuestAddress = order.GuestAddress ?? user?.Address,
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
                Content = body.ToString() ,
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
        public async Task ConfirmCartOrderAsync(int? userId, string? guestName = null, string? guestEmail = null, string? guestAddress = null,decimal? deposit=null,decimal? shippingFee=null, string? deliverymethod=null)
        {
            // Generate a guest ID if userId is null
            int id = userId ?? GenerateGuestId();

            // Retrieve cart items for the user or guest
            var cartItems = await _cartRepo.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }
            if (!DeliveryMethodValidate.IsValidDeliveryMethod(deliverymethod))
            {
                throw new ArgumentException($"Invalid delivery method: {deliverymethod}. Allowed values are 'Pick up' and 'Delivery'.");
            }

            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            User? user = userId != null ? await _userService.GetUserByIdAsync(userId.Value) : null;

            var newOrder = new OrderModel
            {
                UserID = user?.UserId,
                GuestName = guestName ?? user?.Name,
                GuestEmail = guestEmail ?? user?.Email,
                GuestAddress = guestAddress ?? user?.Address,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                TotalPrice = Math.Round(totalPrice, 2),
                Deposit = deposit,
                ShippingFee = shippingFee,
                Paid = false,
                Status = "Pending",
                DeliveryMethod = deliverymethod
            };

            // Add the order to the database and retrieve its generated ID
            int orderId = await _orderRepository.AddOrderAsync(newOrder);

            // Add order details from cart items
            await _orderRepository.AddOrderDetailsAsync(orderId, cartItems);
            // Send a confirmation email to the guest or user
            string recipientEmail = user != null ? user.Email : guestEmail;
            if (!string.IsNullOrEmpty(recipientEmail))
            {
                var subject = "Order Confirmation";
                var body = new StringBuilder();
                body.AppendLine("Dear Customer,");
                body.AppendLine();
                body.AppendLine($"Thank you for your order! Your Order ID is: {orderId}.");
                
                body.AppendLine($"- Order Date: {newOrder.OrderDate?.ToString("d")}");
                body.AppendLine($"- Shipped Date: {newOrder.ShippedDate?.ToString("d")}");

                body.AppendLine();
                body.AppendLine("Order Details:");
                // List the products in the order
                body.AppendLine($"\nProducts in your order:");
                body.AppendLine($"\nProducts in your order:");
                foreach (var item in cartItems)
                {
                    body.AppendLine($"- Product: {item.CustomProduct.ProductCode}");
                    body.AppendLine($"  Price: {item.Price:C}");
                    body.AppendLine($"  Quantity: {item.Quantity}");
                    body.AppendLine($"  Subtotal: {(item.Price * item.Quantity):C}");
                    body.AppendLine(); // Add a blank line between products for readability
                }
                body.AppendLine($"- Note: {newOrder.Note}");
                body.AppendLine($"- Paid: {(newOrder.Paid ? "Yes" : "No")}");
                body.AppendLine($"- Status: {newOrder.Status}");
                body.AppendLine($"- Total Price: {newOrder.TotalPrice:C}");
                body.AppendLine($"-Shipping Fee:{newOrder.ShippingFee}");
                body.AppendLine($"-Deposit:{newOrder.Deposit}");
                body.AppendLine($"-Balance Payment: {newOrder.BalancePayment}");
                body.AppendLine($"-Delivery Method: {newOrder.DeliveryMethod}");
                body.AppendLine("We appreciate your business and look forward to serving you again!");
                body.AppendLine("Best regards,");
                body.AppendLine("Matcha VestTailor Team");
                var emailRequest = new EmailRequest
                {
                    To = recipientEmail,
                    Subject = subject,
                    Content = body.ToString()
                };
                await _emailHelper.SendEmailAsync(emailRequest);
                // Clear the cart after the order is confirmed
                 await _cartRepo.RemoveAllFromCartAsync(id);
            }
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
