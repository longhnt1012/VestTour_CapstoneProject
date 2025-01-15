using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using VestTour.Service.Interface;
using VestTour.Services.Interfaces;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class LiningsController : ControllerBase
    {
        private readonly ILiningService _liningService;

        // Constructor to inject LiningService
        public LiningsController(ILiningService liningService)
        {
            _liningService = liningService;
        }

        // Get all linings
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<LiningModel>>>> GetAllLiningsAsync()
        {
            var response = await _liningService.GetAllLiningsAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Get lining by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<LiningModel>>> GetLiningByIdAsync(int id)
        {
            var response = await _liningService.GetLiningByIdAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // Add new lining
        [HttpPost]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<LiningModel>>> AddLiningAsync([FromBody] LiningModel model)
        {
            var response = await _liningService.AddLiningAsync(model);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetLiningByIdAsync), new { id = response.Data.LiningId }, response);
            }
            return BadRequest(response);
        }

        // Update existing lining
        [HttpPut("{id}")]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse>> UpdateLiningAsync(int id, [FromBody] LiningModel model)
        {
            var response = await _liningService.UpdateLiningAsync(id, model);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Delete a lining
        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse>> DeleteLiningAsync(int id)
        {
            var response = await _liningService.DeleteLiningAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Update status of a lining
        [HttpPatch("{id}/status")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse>> UpdateStatusAsync(int id, [FromBody] string newStatus)
        {
            var response = await _liningService.UpdateStatusAsync(id, newStatus);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
