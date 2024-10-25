using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IOrderRepository
    {
        public Task<List<OrderModel>> GetAllOrderAsync();

        public Task<OrderModel> GetOrderByIdAsync(int orderID);

        public Task<int> AddOrderAsync(OrderModel order);

        public Task UpdateOrderAsync(int id, OrderModel order);
        public Task<OrderModel?> GetOrderDetailByIdAsync(int orderId);
        public Task<List<OrderModel>> GetOrdersByUserIdAsync(int userId);


    }
}
