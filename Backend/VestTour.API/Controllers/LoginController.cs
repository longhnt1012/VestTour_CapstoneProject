using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using VestTour.Repository.Interface;
using Google.Apis.Auth.OAuth2.Requests;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, ITokenService tokenService, IUserService userService, IUserRepository userRepository, IConfiguration configuration)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _userService = userService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var response = await _loginService.LoginAsync(loginModel);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenApiModel request)
        {
            // Validate access token and get principal
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid access token.");

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            // Generate new tokens
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update refresh token and expiry in database
            await _userService.UpdateRefreshTokenAsync(user.UserId, newRefreshToken, DateTime.UtcNow.AddDays(7));

            var response = new LoginResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenDurationInMinutes"]))
            };

            return Ok(response);
        }

    }
}
