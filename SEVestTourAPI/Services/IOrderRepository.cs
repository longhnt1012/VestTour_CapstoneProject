﻿using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public interface IOrderRepository
    {
        public Task<List<OrderModel>> GetAllOrderAsync();

        public Task<OrderModel> GetOrderByIdAsync(int orderID);

        public Task<int> AddOrderAsync (OrderModel order);

        public Task UpdateOrderAsync(int id, OrderModel order);



    }
}
