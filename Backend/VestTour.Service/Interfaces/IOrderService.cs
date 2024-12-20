﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetAllOrdersAsync();
        Task<OrderModel?> GetOrderByIdAsync(int id);
        Task<int> CreateOrderAsync(OrderModel order);
        Task ConfirmCartOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, decimal deposit, decimal shippingFee, string? deliveryMethod, int storeId, int? voucherId);
        Task UpdateOrderAsync(int id, OrderModel order);
        Task<int> GetTotalOrdersAsync();
        Task<int> GetTotalOrdersByIdAsync(int id);
        Task<List<OrderModel>> GetOrdersByStoreIdAsync(int storeId);
        Task<List<OrderModel>> GetOrdersByUserIdAsync(int userId);
        Task<OrderModel?> GetOrderDetailByIdAsync(int orderId);
        Task<ServiceResponse> ChangeOrderStatusAsync(int orderId, string newStatus);

        Task<int> CreateOrderForCustomerAsync(AddOrderForCustomer orderRequest);
    }
}
