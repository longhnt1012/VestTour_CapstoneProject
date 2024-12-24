using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;

namespace VestTour.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddOrderAsync(OrderModel order)
        {
            var newOrder = _mapper.Map<Order>(order);
            _context.Orders!.Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder.OrderId;
        }
        public async Task AddOrderDetailsAsync(int orderId, List<CartItemModel> cartItems)
        {
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.Product?.ProductID ?? 0, // Use ProductID from the cart item
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();
        }
        public async Task AddOrderDetailAsync(int orderId, int productId, int quantity, decimal price)
        {
           
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = productId ,// Use ProductID from the cart item
                    Quantity = quantity,
                    Price = price
                };

                _context.OrderDetails.Add(orderDetail);
            

            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderModel>> GetAllOrderAsync()
        {
            var orders = await _context.Orders!.ToListAsync();
            return _mapper.Map<List<OrderModel>>(orders);
        }

       
        public async Task UpdateOrderAsync(int id, OrderModel order)
        {
            var updateOrder = _mapper.Map<Order>(order);
            _context.Orders!.Update(updateOrder);
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderModel>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders!
                //.Include(o => o.Products) 
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel?> GetOrderDetailByIdAsync(int orderId)
        {
            // Lấy Order từ database
            var orderEntity = await _context.Orders
                .Include(o => o.OrderDetails) // Bao gồm OrderDetails
                .ThenInclude(od => od.Product) // Bao gồm thông tin sản phẩm trong OrderDetails
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (orderEntity == null)
            {
                // Nếu không tìm thấy đơn hàng, trả về null
                return null;
            }

            // Map từ Order entity sang OrderModel
            var orderModel = new OrderModel
            {
                OrderId = orderEntity.OrderId,
                UserID = orderEntity.UserId,
                StoreId = orderEntity.StoreId,
                VoucherId = orderEntity.VoucherId,
                OrderDate = orderEntity.OrderDate,
                ShippedDate = orderEntity.ShippedDate,
                Note = orderEntity.Note,
                Paid = orderEntity.Paid,
                Status = orderEntity.Status,
                GuestName = orderEntity.GuestName,
                GuestEmail = orderEntity.GuestEmail,
                GuestAddress = orderEntity.GuestAddress,
                TotalPrice = orderEntity.TotalPrice,
                Deposit = orderEntity.Deposit,
                ShippingFee = orderEntity.ShippingFee,
                DeliveryMethod = orderEntity.DeliveryMethod,
                //Products = orderEntity.OrderDetails.Select(od => new ProductModel
                //{
                //    ProductID = od.ProductId,
                //    ProductCode = od.Product?.ProductCode,
                //    FabricID  = od.Product?.FabricId,
                //    LiningID = od.Product?.LiningId,

                //    Price = od.Product?.Price ?? 0
                    
                //}).ToList(),
                OrderDetails = orderEntity.OrderDetails.Select(od => new OrderDetailModel
                {
                    OrderId = orderEntity.OrderId,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity.Value,
                    Price = od.Price.Value
                }).ToList()
            };

            // Tính BalancePayment
            //orderModel.BalancePayment = (orderModel.TotalPrice ?? 0) - (orderModel.Deposit ?? 0) + (orderModel.ShippingFee ?? 0);

            return orderModel;
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders!
               // .Include(o => o.Products) // Make sure to include the products
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return _mapper.Map<OrderModel>(order);
        }
        public async Task ChangeStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                order.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddOrderDetailAsync(OrderDetailModel orderDetailModel)
        {
            var orderDetailEntity = _mapper.Map<OrderDetail>(orderDetailModel);
            _context.OrderDetails.Add(orderDetailEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderModel>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _context.Orders!
                .Where(o => o.Status == status)
                .ToListAsync();

            return _mapper.Map<List<OrderModel>>(orders);
        }
        public async Task<decimal> GetTotalRevenueShareByStatusAsync(string status)
        {
            return await _context.Orders!
                .Where(o => o.Status == status)
                .SumAsync(o => o.RevenueShare ?? 0); // Assuming RevenueShare is nullable
        }
    }
}
