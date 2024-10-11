using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VestTour.Models;
using VestTour.Services;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricsController : ControllerBase
    {
        private readonly IFabricRepository _fabricRepo;

        public FabricsController(IFabricRepository repo) 
        {
            _fabricRepo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFabrics()
        {
            try
            {
                return Ok(await _fabricRepo.GetAllFabricsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFabricById(int id)
        {
            try
            {
                var fabric = await _fabricRepo.GetFabricByIdAsync(id);
                return fabric ==null ? NotFound() : Ok(fabric);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewFabric(FabricModel model)
        {
            var newFabricID = await _fabricRepo.AddFabricAsync(model);
            var fabric = await _fabricRepo.GetFabricByIdAsync(newFabricID);
            return fabric ==null ? NotFound() : Ok(fabric);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFabric(int id, FabricModel model)
        {
            try
            {
                if (id != model.FabricId) { 
                return NotFound();
                }
                if(id != null) 
                {
                    await _fabricRepo.UpdateFabricAsync(id, model);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabric(int id)
        {
            try
            {

                if (id != null)
                {
                    await _fabricRepo.DeleteFabricAsync(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
