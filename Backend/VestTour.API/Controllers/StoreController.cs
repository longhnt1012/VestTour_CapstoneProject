using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreModel>>> GetStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreModel>> GetStore(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        // POST: api/Store
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> CreateStore(StoreModel storeModel)
        {
            var newStoreId = await _storeService.CreateStoreAsync(storeModel);
            return CreatedAtAction(nameof(GetStore), new { id = newStoreId }, newStoreId);
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager")]
        public async Task<IActionResult> UpdateStore(int id, StoreModel storeModel)
        {
            if (id != storeModel.StoreId)
            {
                return BadRequest("Store ID mismatch.");
            }
            await _storeService.UpdateStoreAsync(id, storeModel);
            return NoContent();
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
        [HttpGet("{storeId}/staff")]
        public async Task<IActionResult> GetStaffByStoreId(int storeId)
        {
            var staff = await _storeService.GetStaffByStoreIdAsync(storeId);
            return Ok(staff);
        }
    }
}
