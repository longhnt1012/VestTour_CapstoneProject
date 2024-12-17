using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
using VestTour.Service.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipmentById(int id)
        {
            var response = await _shipmentService.GetShipmentByIdAsync(id);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            var response = await _shipmentService.GetAllShipmentsAsync();
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] ShipmentModel shipmentModel)
        {
            if (shipmentModel == null)
                return BadRequest(Error.InvalidShipmentData);

            var response = await _shipmentService.AddShipmentAsync(shipmentModel);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(new { Message = response.Message, ShipmentId = response.Data });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] ShipmentModel shipmentModel)
        {
            if (shipmentModel == null)
                return BadRequest(Error.InvalidShipmentData);

            var response = await _shipmentService.UpdateShipmentAsync(id, shipmentModel);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var response = await _shipmentService.DeleteShipmentAsync(id);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetShipmentsByStatus(string status)
        {
            var response = await _shipmentService.GetShipmentsByStatusAsync(status);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateShipmentStatus(int id, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status))
                return BadRequest(Error.InvalidShipmentStatus);

            var response = await _shipmentService.UpdateShipmentStatusAsync(id, status);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpGet("recipient/{name}")]
        public async Task<IActionResult> GetShipmentsByRecipientName(string name)
        {
            var response = await _shipmentService.GetShipmentsByRecipientNameAsync(name);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Data);
        }
    }
}
