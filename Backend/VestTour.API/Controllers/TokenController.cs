using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenController(IUserService userService, ITokenService tokenService)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [Route("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null || string.IsNullOrEmpty(tokenApiModel.RefreshToken))
                return BadRequest("Invalid client request");

            // Find the user by refresh token
            var user = await _userService.GetUserByRefreshTokenAsync(tokenApiModel.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
                return Unauthorized("Invalid or expired refresh token");

            // Generate new tokens
            var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, user.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.RoleName)
    };

            var newAccessToken = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update the refresh token in the database
            await _userService.UpdateRefreshTokenAsync(user.UserId, newRefreshToken, DateTime.Now.AddDays(7));

            return Ok(new TokenApiModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            await _userService.ClearRefreshTokenAsync(userId);
            return Ok("Refresh token revoked.");
        }

    }
}
