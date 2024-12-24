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
        Task AddOrderDetailsAsync(int orderId, List<CartItemModel> cartItems);
        Task ChangeStatusAsync(int orderId, string newStatus);
        Task AddOrderDetailAsync(OrderDetailModel orderDetailModel);
        Task AddOrderDetailAsync(int orderId, int productId, int quantity, decimal price);
        Task<List<OrderModel>> GetOrdersByStatusAsync(string status);
        Task<decimal> GetTotalRevenueShareByStatusAsync(string status);
    }
}
