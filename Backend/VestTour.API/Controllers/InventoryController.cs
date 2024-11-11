using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

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
            var inventories = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductID(int productId)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(productId);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        [HttpPost]
        [Authorize(Roles = "admin,manager,staff")]
        public async Task<IActionResult> Add(InventoryModel inventory)
        {
            await _inventoryService.AddInventoryAsync(inventory);
            return CreatedAtAction(nameof(GetByProductID), new { productId = inventory.ProductID }, inventory);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "admin,manager,staff")]
        public async Task<IActionResult> Update(int productId, InventoryModel inventory)
        {
            if (productId != inventory.ProductID)
            {
                return BadRequest();
            }

            await _inventoryService.UpdateInventoryAsync(productId,inventory);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "admin,manager,staff")]
        public async Task<IActionResult> Delete(int productId)
        {
            await _inventoryService.DeleteInventoryAsync(productId);
            return NoContent();
        }
    }

}
