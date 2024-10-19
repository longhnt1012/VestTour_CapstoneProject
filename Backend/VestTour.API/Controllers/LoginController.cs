using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Services;
using System.Threading.Tasks;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var token = await _loginService.LoginAsync(login);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}
