using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<int>> CreateStore(StoreModel storeModel)
        {
            var newStoreId = await _storeService.CreateStoreAsync(storeModel);
            return CreatedAtAction(nameof(GetStore), new { id = newStoreId }, newStoreId);
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
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
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}
