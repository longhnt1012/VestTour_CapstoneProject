using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Services;
using SEVestTourAPI.Models;
using SEVestTourAPI.ValidationHelpers;
using SEVestTourAPI.Message;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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

            if (string.IsNullOrWhiteSpace(registerModel.Name) || registerModel.Name.Length < 5 || registerModel.Name.Length > 25)
            {
                return BadRequest(Error.InvalidName);
            }

            if (!UserValidate.IsValidGender(registerModel.Gender))
            {
                return BadRequest(Error.InvalidGender);
            }

            if (!UserValidate.IsValidEmail(registerModel.Email))
            {
                return BadRequest(Error.InvalidEmail);
            }

            if (!UserValidate.IsValidPassword(registerModel.Password))
            {
                return BadRequest(Error.InvalidPassword);
            }

            if (await _userRepository.IsEmailTakenAsync(registerModel.Email))
            {
                return BadRequest(Error.EmailTaken);
            }

            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.Dob,
                RoleId = registerModel.RoleID,
                Email = registerModel.Email,
                Password = registerModel.Password,
                Status = "active", // Setting default status
                IsConfirmed = true
            };

            var result = await _userRepository.AddUserAsync(newUser);

            if (result > 0)
            {
                return Ok(Success.RegistrationSuccess);
            }

            return BadRequest(Error.RegistrationFailed);
        }
    }
}
