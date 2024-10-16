using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreModel>>> GetStores()
        {
            var stores = await _storeRepository.GetAllStoresAsync();
            return Ok(stores);
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreModel>> GetStore(int id)
        {
            var store = await _storeRepository.GetStoreByIdAsync(id);

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
            var newStoreId = await _storeRepository.AddStoreAsync(storeModel);
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

            var existingStore = await _storeRepository.GetStoreByIdAsync(id);
            if (existingStore == null)
            {
                return NotFound();
            }

            await _storeRepository.UpdateStoreAsync(id, storeModel);
            return NoContent();
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _storeRepository.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            await _storeRepository.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}
