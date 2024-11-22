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

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts([FromQuery] int provinceId)
        {
            if (provinceId <= 0)
            {
                return BadRequest("Invalid province ID.");
            }

            var districts = await _shippingService.GetDistrictsAsync(provinceId);
            return Ok(districts);
        }

        [HttpGet("wards")]
        public async Task<IActionResult> GetWards([FromQuery] int districtId)
        {
            if (districtId <= 0)
            {
                return BadRequest("Invalid district ID.");
            }

            var wards = await _shippingService.GetWardsAsync(districtId);
            return Ok(wards);
        }

        //xem dich vu co san
        [HttpPost("available-services")]
        public async Task<IActionResult> GetAvailableServices([FromBody] ShippingRequestModel request)
        {
            var services = await _shippingService.GetAvailableServicesAsync(request.ShopId, request.FromDistrict, request.ToDistrict);
            return Ok(services);
        }
        [HttpPost("calculate-fee")]
        public async Task<IActionResult> CalculateShippingFee([FromBody] ShippingFeeRequestModel request)
        {
            var shippingFee = await _shippingService.CalculateShippingFeeAsync(request);
            return Ok(shippingFee);
        }



    }
}
