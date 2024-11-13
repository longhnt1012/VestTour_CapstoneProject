using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _inventoryService.GetAllInventoriesAsync();
            return response.Success ? Ok(response.Data) : StatusCode(500, response.Message);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductID(int productId)
        {
            var response = await _inventoryService.GetInventoryByIdAsync(productId);
            return response.Success ? Ok(response.Data) : (response.Data == null ? NotFound(response.Message) : StatusCode(500, response.Message));
        }

        [HttpPost]
        [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> Add(InventoryModel inventory)
        {
            var response = await _inventoryService.AddInventoryAsync(inventory);
            return response.Success ? CreatedAtAction(nameof(GetByProductID), new { productId = inventory.ProductID }, inventory) : StatusCode(500, response.Message);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> Update(int productId, InventoryModel inventory)
        {
            if (productId != inventory.ProductID)
                return BadRequest("Product ID mismatch.");

            var response = await _inventoryService.UpdateInventoryAsync(productId, inventory);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> Delete(int productId)
        {
            var response = await _inventoryService.DeleteInventoryAsync(productId);
            return response.Success ? NoContent() : BadRequest(response);
        }
    }
}
