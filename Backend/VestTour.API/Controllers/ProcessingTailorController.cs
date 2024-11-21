using Microsoft.AspNetCore.Mvc;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;
using VestTour.Service.Services;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingTailorController : ControllerBase
    {
        private readonly IProcessingTailorService _processingTailorService;

        public ProcessingTailorController(IProcessingTailorService processingTailorService)
        {
            _processingTailorService = processingTailorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcessingTailorById(int id)
        {
            var response = await _processingTailorService.GetProcessingTailorByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProcessingTailors()
        {
            var response = await _processingTailorService.GetAllProcessingTailorsAsync();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProcessingTailor(ProcessingTailorModel processingTailor)
        {
            var response = await _processingTailorService.AddProcessingTailorAsync(processingTailor);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetProcessingTailorById), new { id = response.Data }, response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProcessingTailor(int id, ProcessingTailorModel processingTailor)
        {
            var response = await _processingTailorService.UpdateProcessingTailorAsync(id, processingTailor);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessingTailor(int id)
        {
            var response = await _processingTailorService.DeleteProcessingTailorAsync(id);
            return response.Success ? NoContent() : NotFound(response);
        }
        [HttpGet("assigned-to/{tailorPartnerId}")]
        public async Task<IActionResult> GetProcessesByTailorPartnerId(int tailorPartnerId)
        {
            var result = await _processingTailorService.GetProcessAssignedByTailorPartnerIdAsync(tailorPartnerId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("process/status/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] string newStatus)
        {
           
            var response = await _processingTailorService.ChangeStatusAsync(id, newStatus);
            if (!response.Success)
            {
                return BadRequest(response);
            }  

            return Ok(response);
        }
        [HttpPatch("sample/status/{id}")]
        public async Task<IActionResult> ChangeSampleStatus(int id, [FromBody] string newStatus)
        {

            var response = await _processingTailorService.ChangeSampleStatusAsync(id, newStatus);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPatch("fix/status/{id}")]
        public async Task<IActionResult> ChangeFixStatus(int id, [FromBody] string newStatus)
        {

            var response = await _processingTailorService.ChangeFixStatusAsync(id, newStatus);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPatch("delivery/status/{id}")]
        public async Task<IActionResult> ChangeDeliveryStatus(int id, [FromBody] string newStatus)
        {

            var response = await _processingTailorService.ChangeDeliveryStatusAsync(id, newStatus);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPatch("stagename/{id}")]
        public async Task<IActionResult> ChangeStageName(int id, [FromBody] string newStage)
        {

            var response = await _processingTailorService.ChangeStageNameAsync(id, newStage);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}