using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using VestTour.Domain.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;
using VestTour.Service.Interface;

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
            var response = await _fabricService.GetAllFabricsAsync();
            return response.Success ? Ok(response.Data) : StatusCode(500, response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFabricById(int id)
        {
            var response = await _fabricService.GetFabricByIdAsync(id);
            return response.Success ? Ok(response.Data) : (response.Data == null ? NotFound(response.Message) : StatusCode(500, response.Message));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddNewFabric(FabricModel model)
        {
            var response = await _fabricService.AddFabricAsync(model);
            return response.Success ? CreatedAtAction(nameof(GetFabricById), new { id = response.Data }, response.Data) : StatusCode(500, response.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager")]
        public async Task<IActionResult> UpdateFabric(int id, FabricModel model)
        {
            if (id != model.FabricID)
                return BadRequest("Fabric ID mismatch.");

            var response = await _fabricService.UpdateFabricAsync(id, model);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteFabric(int id)
        {
            var response = await _fabricService.DeleteFabricAsync(id);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpGet("tag/{tag}")]
        public async Task<IActionResult> GetFabricByTag(FabricEnums? tag)
        {
            if (tag == null)
                return BadRequest("Tag parameter is required.");

            var response = await _fabricService.GetFabricByTagAsync(tag);
            return response.Success ? Ok(response.Data) : StatusCode(500, response.Message);
        }
    }
}
