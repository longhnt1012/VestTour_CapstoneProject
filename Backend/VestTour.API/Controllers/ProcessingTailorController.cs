using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

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
            return response.Success ? CreatedAtAction(nameof(GetProcessingTailorById), new { id = response.Data }, response) : BadRequest(response);
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
    }
}