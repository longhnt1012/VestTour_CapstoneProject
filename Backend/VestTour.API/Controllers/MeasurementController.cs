using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        // GET: api/Measurement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementModel>>> GetMeasurements()
        {
            var measurements = await _measurementService.GetAllMeasurementsAsync();
            return Ok(measurements);
        }

        // GET: api/Measurement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementModel>> GetMeasurement(int id)
        {
            try
            {
                var measurement = await _measurementService.GetMeasurementByIdAsync(id);
                return Ok(measurement);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Measurement not found.");
            }
        }

        // POST: api/Measurement
        [HttpPost]
        public async Task<ActionResult<int>> CreateMeasurement(MeasurementModel measurementModel)
        {
            var newMeasurementId = await _measurementService.AddMeasurementAsync(measurementModel);
            return CreatedAtAction(nameof(GetMeasurement), new { id = newMeasurementId }, newMeasurementId);
        }

        // PUT: api/Measurement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, MeasurementModel measurementModel)
        {
            try
            {
                await _measurementService.UpdateMeasurementAsync(id, measurementModel);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Measurement not found.");
            }
        }

        // DELETE: api/Measurement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            try
            {
                await _measurementService.DeleteMeasurementAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Measurement not found.");
            }
        }
    }
}
