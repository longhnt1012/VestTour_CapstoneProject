using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrdersController(IOrderRepository repo) 
        {
        _orderRepo= repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                return Ok(await _orderRepo.GetAllOrderAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByID(int id)
        {
            try
            {
                var order= await _orderRepo.GetOrderByIdAsync(id);
                return order==null? NotFound() : Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrder(OrderModel order)
        {
            try
            {
                var newOrder = new OrderModel
                {
                    PaymentId = order.PaymentId,
                    StoreId = order.StoreId,
                    VoucherId = order.VoucherId,
                    ShipperPartnerId = order.ShipperPartnerId,
                    OrderDate = order.OrderDate,
                    ShippedDate = order.ShippedDate,
                    Note = order.Note,
                    Paid = order.Paid,
                    Status = order.Status ?? "pending"
                };
                var newOrderID= await _orderRepo.AddOrderAsync(newOrder);
                var Findorder = await _orderRepo.GetOrderByIdAsync(newOrderID);
                return Findorder == null? NotFound() : Ok(Findorder);
            
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
