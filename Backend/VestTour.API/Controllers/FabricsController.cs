using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Repository.Constants;
using VestTour.Service.Interface;
using VestTour.Domain.Enums;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricsController : ControllerBase
    {
        private readonly IFabricService _fabricService;

        public FabricsController(IFabricService fabricService)
        {
            _fabricService = fabricService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFabrics()
        {
            try
            {
                return Ok(await _fabricService.GetAllFabricsAsync());
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
                var fabric = await _fabricService.GetFabricByIdAsync(id);
                if (fabric == null)
                {
                    return NotFound("Fabric not found.");
                }
                return Ok(fabric);
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine($"Error retrieving fabric with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddNewFabric(FabricModel model)
        {
            try
            {
                var newFabricID = await _fabricService.AddFabricAsync(model);
                var fabric = await _fabricService.GetFabricByIdAsync(newFabricID);
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
                if (id != model.FabricID)
                {
                    return BadRequest(Error.FabricNotFound);
                }

                await _fabricService.UpdateFabricAsync(id, model);
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
                await _fabricService.DeleteFabricAsync(id);
                return Ok(Success.FabricDeleted);
            }
            catch
            {
                return BadRequest(Error.FabricDeleteFailed);
            }
        }

        //[HttpGet("tag/{tag?}")]
        //public async Task<IActionResult> GetFabricsByTag(FabricEnums? tag)
        //{
        //    try
        //    {
        //        var fabrics = await _fabricService.GetFabricByTagAsync(tag);
        //        return fabrics == null || fabrics.Count == 0 ? NotFound(Error.FabricNotFound) : Ok(fabrics);
        //    }
        //    catch
        //    {
        //        return BadRequest(Error.FabricNotFound);
        //    }
        //}
    }
}
