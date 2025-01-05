using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
       // [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> GetMeasurements()
        {
            var response = await _measurementService.GetAllMeasurementsAsync();
            return response.Success ? Ok(response.Data) : StatusCode(500, response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            var response = await _measurementService.GetMeasurementByIdAsync(id);
            return response.Success ? Ok(response.Data) : (response.Data == null ? NotFound(response.Message) : StatusCode(500, response.Message));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeasurement(MeasurementModel measurementModel)
        {
            var response = await _measurementService.AddMeasurementAsync(measurementModel);
            return response.Success ? CreatedAtAction(nameof(GetMeasurement), new { id = response.Data }, response.Data) : StatusCode(500, response.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, MeasurementModel measurementModel)
        {
            if (id != measurementModel.MeasurementId)
                return BadRequest("Measurement ID mismatch.");

            var response = await _measurementService.UpdateMeasurementAsync(id, measurementModel);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin,store manager,staff")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            var response = await _measurementService.DeleteMeasurementAsync(id);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMeasurementByUserId(int userId)
        {
            var response = await _measurementService.GetMeasurementByUserIdAsync(userId);
            return response.Success ? Ok(response.Data) : (response.Data == null ? NotFound(response.Message) : StatusCode(500, response.Message));
        }
    }
}
