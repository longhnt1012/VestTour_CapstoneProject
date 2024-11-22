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

            // Thực hiện logic xử lý trước khi thêm đơn hàng
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
                ShippingFee= order.ShippingFee
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
            body.AppendLine($"-Balance Payment{order.BalancePayment}");
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
                To = userEmail, // Assign the retrieved email
                Subject = subject,
                Content = body.ToString() // Set the email body
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
        public async Task ConfirmCartOrderAsync(int? userId, string? guestName = null, string? guestEmail = null, string? guestAddress = null,decimal? deposit=null,decimal? shippingFee=null)
        {
            // Generate a guest ID if userId is null
            int id = userId ?? GenerateGuestId();

            // Retrieve cart items for the user or guest
            var cartItems = await _cartRepo.GetUserCartAsync(id);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("No items in cart to confirm.");
            }

            // Calculate the total price based on cart items
            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            // Fetch the user if userId is not null
            User? user = userId != null ? await _userService.GetUserByIdAsync(userId.Value) : null;

            // Create a new order with relevant details
            var newOrder = new OrderModel
            {
                UserID = user?.UserId, // Set UserID if user is found, otherwise leave as null
                GuestName = user == null ? guestName : null, // Only set GuestName if user is not found
                GuestEmail = user == null ? guestEmail : null, // Only set GuestEmail if user is not found
                GuestAddress = user == null ? guestAddress : null, // Only set GuestAddress if user is not found
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                TotalPrice = Math.Round(totalPrice, 2),
                Deposit = deposit,
                ShippingFee = shippingFee,
                Paid = false,
                Status = "Pending"
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
                
                body.AppendLine($"- Note: {newOrder.Note}");
                body.AppendLine($"- Paid: {(newOrder.Paid ? "Yes" : "No")}");
                body.AppendLine($"- Status: {newOrder.Status}");
                body.AppendLine($"- Total Price: {newOrder.TotalPrice:C}");
                body.AppendLine($"-Shipping Fee:{newOrder.ShippingFee}");
                body.AppendLine($"-Deposit:{newOrder.Deposit}");
                body.AppendLine($"-Balance Payment{newOrder.BalancePayment}");
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
                // await _cartRepo.RemoveAllFromCartAsync(id);
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
