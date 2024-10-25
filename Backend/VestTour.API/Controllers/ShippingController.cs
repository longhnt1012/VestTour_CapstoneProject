using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models.ShippingModel;
using VestTour.Service.Services;

namespace VestTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly ShippingService _shippingService;

        public ShippingController(ShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            var provinces = await _shippingService.GetProvincesAsync();
            return Ok(provinces);
        }

        [HttpPost("districts")]
        public async Task<IActionResult> GetDistricts([FromBody] int provinceId)
        {
            var districts = await _shippingService.GetDistrictsAsync(provinceId);
            return Ok(districts);
        }
        [HttpPost("wards")]
        public async Task<IActionResult> GetWards([FromBody] int districtId)
        {
            var wards = await _shippingService.GetWardsAsync(districtId);
            return Ok(wards);
        }
        [HttpPost("available-services")]
        public async Task<IActionResult> GetAvailableServices([FromBody] ShippingRequestModel request)
        {
            var services = await _shippingService.GetAvailableServicesAsync(request.ShopId, request.FromDistrict, request.ToDistrict);
            return Ok(services);
        }

    }
}
