using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperPartnerController : ControllerBase
    {
        private readonly IShipperPartnerRepository _shipperPartnerRepository;

        public ShipperPartnerController(IShipperPartnerRepository shipperPartnerRepository)
        {
            _shipperPartnerRepository = shipperPartnerRepository;
        }

        // GET: api/ShipperPartner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperPartnerModel>>> GetShipperPartners()
        {
            var shipperPartners = await _shipperPartnerRepository.GetAllShipperPartnersAsync();
            return Ok(shipperPartners);
        }

        // GET: api/ShipperPartner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipperPartnerModel>> GetShipperPartner(int id)
        {
            var shipperPartner = await _shipperPartnerRepository.GetShipperPartnerByIdAsync(id);

            if (shipperPartner == null)
            {
                return NotFound();
            }

            return Ok(shipperPartner);
        }

        // POST: api/ShipperPartner
        [HttpPost]
        public async Task<ActionResult<int>> CreateShipperPartner(ShipperPartnerModel shipperPartnerModel)
        {
            var newShipperPartnerId = await _shipperPartnerRepository.AddShipperPartnerAsync(shipperPartnerModel);
            return CreatedAtAction(nameof(GetShipperPartner), new { id = newShipperPartnerId }, newShipperPartnerId);
        }

        // PUT: api/ShipperPartner/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipperPartner(int id, ShipperPartnerModel shipperPartnerModel)
        {
            if (id != shipperPartnerModel.ShipperPartnerId)
            {
                return BadRequest("Shipper Partner ID mismatch.");
            }

            var existingShipperPartner = await _shipperPartnerRepository.GetShipperPartnerByIdAsync(id);
            if (existingShipperPartner == null)
            {
                return NotFound();
            }

            await _shipperPartnerRepository.UpdateShipperPartnerAsync(id, shipperPartnerModel);
            return NoContent();
        }

        // DELETE: api/ShipperPartner/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipperPartner(int id)
        {
            var shipperPartner = await _shipperPartnerRepository.GetShipperPartnerByIdAsync(id);
            if (shipperPartner == null)
            {
                return NotFound();
            }

            await _shipperPartnerRepository.DeleteShipperPartnerAsync(id);
            return NoContent();
        }
    }
}
