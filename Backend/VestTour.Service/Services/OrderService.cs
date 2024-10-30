using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
