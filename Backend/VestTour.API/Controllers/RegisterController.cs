using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;

namespace VestTourApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registerService.RegisterUserAsync(registerModel);

            if (result == Success.RegistrationSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
