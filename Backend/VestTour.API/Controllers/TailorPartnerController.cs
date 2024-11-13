using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailorPartnerController : ControllerBase
    {
        private readonly ITailorPartnerService _tailorPartnerService;

        public TailorPartnerController(ITailorPartnerService tailorPartnerService)
        {
            _tailorPartnerService = tailorPartnerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTailorPartnerById(int id)
        {
            var response = await _tailorPartnerService.GetTailorPartnerByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTailorPartners()
        {
            var response = await _tailorPartnerService.GetAllTailorPartnersAsync();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTailorPartner(TailorPartnerModel tailorPartner)
        {
            var response = await _tailorPartnerService.AddTailorPartnerAsync(tailorPartner);
            return response.Success ? CreatedAtAction(nameof(GetTailorPartnerById), new { id = response.Data }, response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTailorPartner(int id, TailorPartnerModel tailorPartner)
        {
            var response = await _tailorPartnerService.UpdateTailorPartnerAsync(id, tailorPartner);
            return response.Success ? NoContent() : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTailorPartner(int id)
        {
            var response = await _tailorPartnerService.DeleteTailorPartnerAsync(id);
            return response.Success ? NoContent() : BadRequest(response);
        }
    }
}
