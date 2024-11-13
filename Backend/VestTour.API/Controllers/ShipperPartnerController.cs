using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperPartnerController : ControllerBase
    {
        private readonly IShipperPartnerService _shipperPartnerService;

        public ShipperPartnerController(IShipperPartnerService shipperPartnerService)
        {
            _shipperPartnerService = shipperPartnerService;
        }

        // GET: api/ShipperPartner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperPartnerModel>>> GetShipperPartners()
        {
            var shipperPartners = await _shipperPartnerService.GetAllShipperPartnersAsync();
            return Ok(shipperPartners);
        }

        // GET: api/ShipperPartner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipperPartnerModel>> GetShipperPartner(int id)
        {
            var shipperPartner = await _shipperPartnerService.GetShipperPartnerByIdAsync(id);
            if (shipperPartner == null)
            {
                return NotFound();
            }
            return Ok(shipperPartner);
        }

        // POST: api/ShipperPartner
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> CreateShipperPartner(ShipperPartnerModel shipperPartnerModel)
        {
            var newShipperPartnerId = await _shipperPartnerService.CreateShipperPartnerAsync(shipperPartnerModel);
            return CreatedAtAction(nameof(GetShipperPartner), new { id = newShipperPartnerId }, newShipperPartnerId);
        }

        // PUT: api/ShipperPartner/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateShipperPartner(int id, ShipperPartnerModel shipperPartnerModel)
        {
            if (id != shipperPartnerModel.ShipperPartnerId)
            {
                return BadRequest("Shipper Partner ID mismatch.");
            }
            await _shipperPartnerService.UpdateShipperPartnerAsync(id, shipperPartnerModel);
            return NoContent();
        }

        // DELETE: api/ShipperPartner/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteShipperPartner(int id)
        {
            await _shipperPartnerService.DeleteShipperPartnerAsync(id);
            return NoContent();
        }
    }
}
