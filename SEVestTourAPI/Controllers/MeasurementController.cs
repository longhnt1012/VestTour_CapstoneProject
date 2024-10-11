using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementController(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        // GET: api/Measurement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementModel>>> GetMeasurements()
        {
            var measurements = await _measurementRepository.GetAllMeasurementsAsync();
            return Ok(measurements);
        }

        // GET: api/Measurement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementModel>> GetMeasurement(int id)
        {
            var measurement = await _measurementRepository.GetMeasurementByIdAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return Ok(measurement);
        }

        // POST: api/Measurement
        [HttpPost]
        public async Task<ActionResult<int>> CreateMeasurement(MeasurementModel measurementModel)
        {
            var newMeasurementId = await _measurementRepository.AddMeasurementAsync(measurementModel);
            return CreatedAtAction(nameof(GetMeasurement), new { id = newMeasurementId }, newMeasurementId);
        }

        // PUT: api/Measurement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, MeasurementModel measurementModel)
        {
            if (id != measurementModel.MeasurementId)
            {
                return BadRequest("Measurement ID mismatch.");
            }

            var existingMeasurement = await _measurementRepository.GetMeasurementByIdAsync(id);
            if (existingMeasurement == null)
            {
                return NotFound();
            }

            await _measurementRepository.UpdateMeasurementAsync(id, measurementModel);
            return NoContent();
        }

        // DELETE: api/Measurement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            var measurement = await _measurementRepository.GetMeasurementByIdAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            await _measurementRepository.DeleteMeasurementAsync(id);
            return NoContent();
        }
    }
}
