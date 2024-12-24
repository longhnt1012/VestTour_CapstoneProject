using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Service.Services;
using Microsoft.AspNetCore.Cors;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        //[Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
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
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Order not found.");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("staffcreateorder")]
        public async Task<IActionResult> StaffCreateOrder([FromBody] AddOrderForCustomer orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("Order request cannot be null.");
            }

            try
            {
                int orderId = await _orderService.CreateOrderForCustomerAsync(orderRequest);
                return Ok(new
                {
                    Success = true,
                    Message = "Order created successfully.",
                    OrderId = orderId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "An error occurred while creating the order.",
                    Error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewOrder(OrderModel order)
        {
            try
            {
                var newOrderID = await _orderService.CreateOrderAsync(order);
                var newOrder = await _orderService.GetOrderByIdAsync(newOrderID);
                return CreatedAtAction(nameof(GetOrderByID), new { id = newOrderID }, newOrder);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> UpdateOrder(int id, OrderModel order)
        {
            try
            {
                await _orderService.UpdateOrderAsync(id, order);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Order not found.");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("orderstatus/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            try
            {
                var orders = await _orderService.GetOrdersByStatusAsync(status);
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{orderId}/details")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            try
            {
                var orderDetails = await _orderService.GetOrderDetailByIdAsync(orderId);
                if (orderDetails == null)
                    return NotFound("Order not found.");
                return Ok(orderDetails);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Thêm các phương thức mới
        [HttpGet("total")]
        //[Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> GetTotalOrders()
        {
            try
            {
                var totalOrders = await _orderService.GetTotalOrdersAsync();
                return Ok(totalOrders);
            }
            catch
            {
                return BadRequest();
            }
        }

        

        [HttpGet("store/{storeId}")]
        //[Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> GetOrdersByStoreId(int storeId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByStoreIdAsync(storeId);
                return Ok(orders);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPatch("updatestatus/{id}")]
       // [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> ChangeStatusName(int id, [FromBody] string newStatus)
        {

            var response = await _orderService.ChangeOrderStatusAsync(id, newStatus);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("total-revenue-share/{status}")]
        public async Task<IActionResult> GetTotalRevenueShare(string status)
        {
            var totalRevenueShare = await _orderService.GetTotalRevenueShareByStatusAsync(status);
            return Ok(new { TotalRevenueShare = totalRevenueShare });
        }
    }
}
