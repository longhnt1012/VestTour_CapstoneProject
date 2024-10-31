using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Helpers;
using VestTour.Repository.Implementation;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;

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
            // Thực hiện logic xử lý trước khi thêm đơn hàng
            var newOrder = new OrderModel
            {
                UserID = order.UserID,
                PaymentId = order.PaymentId,
                StoreId = order.StoreId,
                VoucherId = order.VoucherId,
                ShipperPartnerId = order.ShipperPartnerId,
                OrderDate = order.OrderDate,
                ShippedDate = order.ShippedDate,
                Note = order.Note,
                Paid = order.Paid,
                Status = order.Status ?? "pending",
                BalancePayment = order.BalancePayment,
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
            body.AppendLine($"- Payment ID: {order.PaymentId}");
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
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
                throw new KeyNotFoundException("Order not found.");
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
        

    }
}
