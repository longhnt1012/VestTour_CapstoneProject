using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VestTour.Models;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly VestTourDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(VestTourDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = GenerateJwtTokenAsync(user);
            return Ok(new { Token = token });
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register( RegisterModel registerModel)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerModel.Email))
            {
                return BadRequest("Email is already taken.");
            }

            // Check if RoleID is valid
            var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == registerModel.RoleID);
            if (!roleExists)
            {
                return BadRequest("Invalid RoleID.");
            }

            // Create a new user
            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.DOB,
                RoleId = registerModel.RoleID,
                Email = registerModel.Email,
                Password = registerModel.Password,  // Consider hashing passwords in production!
                IsConfirmed = true  // Set isConfirmed to true by default
            };

            _context.Users!.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        private async Task<string> GenerateJwtTokenAsync(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == user.RoleId);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, role.RoleName),  // Include the role name here
        new Claim(ClaimTypes.Name,user.UserId.ToString()),
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
}
