using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Services;
using Microsoft.AspNetCore.Authorization;
using SEVestTourAPI.Models;


namespace VestTourApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email is taken
            if (await _userRepository.IsEmailTakenAsync(registerModel.Email))
            {
                return BadRequest("Email is already taken.");
            }

            // Create new user and register
            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.Dob,
                RoleId = registerModel.RoleID,
                Email = registerModel.Email,
                Password = registerModel.Password,
                IsConfirmed = true
            };

            await _userRepository.AddUserAsync(newUser);

            return Ok("User registered successfully.");
        }
    }
}
