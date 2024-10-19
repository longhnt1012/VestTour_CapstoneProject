using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<<< Updated upstream:SEVestTourAPI/Controllers/FabricsController.cs
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using SEVestTourAPI.Message;
========
using VestTour.Repository.Models;
<<<<<<< HEAD
using VestTour.Repository.Constants;
=======
using VestTour.Constants;
>>>>>>> bcd41fa4de15cca8d11df9b24c9fe030759871d1
using VestTour.Repository.Interface;
using VestTour.Domain.Enums; // Add this using directive
>>>>>>>> Stashed changes:Backend/VestTour.API/Controllers/FabricsController.cs

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
        [HttpGet("tag/{tag?}")]
        public async Task<IActionResult> GetFabricsByTag(FabricEnums? tag) // Tag is now nullable
        {
            try
            {
                var fabrics = await _fabricRepo.GetFabricByTagAsync(tag); // Pass the nullable tag
                return fabrics == null || fabrics.Count == 0 ? NotFound(Error.FabricNotFound) : Ok(fabrics);
            }
            catch
            {
                return BadRequest(Error.FabricNotFound);
            }
        }

    }
}
