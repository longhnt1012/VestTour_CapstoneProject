using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Repository.Constants;
using VestTour.Repository.Interface;
using VestTour.Domain.Enums; // Add this using directive

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
                return BadRequest(Error.FabricNotFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFabricById(int id)
        {
            try
            {
                var fabric = await _fabricRepo.GetFabricModelByIdAsync(id);
                return fabric == null ? NotFound(Error.FabricNotFound) : Ok(fabric);
            }
            catch
            {
                return BadRequest(Error.FabricNotFound);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFabric(FabricModel model)
        {
            try
            {
                var newFabricID = await _fabricRepo.AddFabricAsync(model);
                var fabric = await _fabricRepo.GetFabricModelByIdAsync(newFabricID);
                return fabric == null ? NotFound(Error.FabricAddFailed) : Ok(fabric);
            }
            catch
            {
                return BadRequest(Error.FabricAddFailed);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFabric(int id, FabricModel model)
        {
            try
            {
                if (id != model.FabricId)
                {
                    return BadRequest(Error.FabricNotFound);
                }

                await _fabricRepo.UpdateFabricAsync(id, model);
                return Ok(Success.FabricUpdated);
            }
            catch
            {
                return BadRequest(Error.FabricUpdateFailed);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabric(int id)
        {
            try
            {
                await _fabricRepo.DeleteFabricAsync(id);
                return Ok(Success.FabricDeleted);
            }
            catch
            {
                return BadRequest(Error.FabricDeleteFailed);
            }
        }

        // New method to get fabrics by tag
        [HttpGet("tag/{tag}")]
        public async Task<IActionResult> GetFabricsByTag(string tag)
        {
            try
            {
                // Convert string to enum
                if (!Enum.TryParse<FabricEnums>(tag, true, out var fabricTag))
                {
                    return BadRequest("Invalid tag value.");
                }

                var fabrics = await _fabricRepo.GetFabricByTagAsync(fabricTag);
                return fabrics == null || fabrics.Count == 0 ? NotFound(Error.FabricNotFound) : Ok(fabrics);
            }
            catch
            {
                return BadRequest(Error.FabricNotFound);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllFabricByDescription([FromQuery] string description)
        {
            try
            {
                var fabrics = await _fabricRepo.GetFabricsByDescriptionAsync(description);
                return fabrics == null || fabrics.Count == 0 ? NotFound(Error.FabricNotFound) : Ok(fabrics);
            }
            catch
            {
                return BadRequest(Error.FabricNotFound);
            }
        }


    }
}
