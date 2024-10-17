using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;

namespace VestTour.Repository.Implementation
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

        public async Task<List<OrderModel>> GetAllOrderAsync()
        {
            var orders = await _context.Orders!.ToListAsync();
            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<OrderModel?> GetOrderByIdAsync(int orderID)
        {
            var order = await _context.Orders!.FindAsync(orderID);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task UpdateOrderAsync(int id, OrderModel order)
        {
            var updateOrder = _mapper.Map<Order>(order);
            _context.Orders!.Update(updateOrder);
            await _context.SaveChangesAsync();
        }
    }
}
