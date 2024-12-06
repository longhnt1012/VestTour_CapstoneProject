using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Repository.Constants;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using Microsoft.AspNetCore.Cors;
using VestTour.API.FileHandle;
using VestTour.Service.Services;


namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
     

        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        // GET: api/user
        [HttpGet]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        //[Authorize(Roles = "admin,store manager")]
        public async Task<ActionResult<int>> AddUser([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Error.InvalidModelState);
            }

            var newUserId = await _userService.AddUserAsync(userModel);
            return CreatedAtAction(nameof(GetUserById), new { id = newUserId }, newUserId);
        }

       
        [HttpPut("{id}")]
         [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Error.InvalidModelState);
            }

            try
            {
                await _userService.UpdateUserAsync(id, userModel);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(Error.UserNotFound);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }


        // PUT: api/user/{id}/status
        [HttpPut("{id}/status")]
        //[Authorize]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(Error.InvalidUserStatus);
            }

            try
            {
                await _userService.UpdateUserStatusAsync(id, status);
                return Ok(Success.StatusUpdated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(Error.UserNotFound);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET: api/user/role/4
        [HttpGet("role/{roleId:int}")]
        //[Authorize(Roles = "admin, store manager")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersByRole(int roleId)
        {
            var users = await _userService.GetUsersByRoleAsync(roleId);
            return Ok(users);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var result = await _userService.ForgotPassword(model.Email);

            if (result == Success.ResetEmailSent)
            {
                return Ok(new { Message = "Password reset email sent successfully." });
            }

            return BadRequest(new { Message = result });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var result = await _userService.ResetPasswordAsync(model.Token, model.NewPassword);

            if (result == Success.PasswordResetSuccess)
            {
                return Ok(new { Message = "Password has been reset successfully." });
            }

            return BadRequest(new { Message = result });
        }
        //[HttpPut("{userId}/avatar")]
        //public async Task<IActionResult> UpdateAvatar(int userId, [FromBody] string avatarUrl)
        //{
        //    try
        //    {
        //        await _userService.UpdateUserAvatarAsync(userId, avatarUrl);
        //        return Ok(new { message = "Avatar updated successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}
        // POST: api/user/{userId}/avatar/upload
        [HttpPost("{userId}/avatar/upload")]
        public async Task<IActionResult> UploadAvatar(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded or file is empty." });
            }

            try
            {
                // Handle file upload
                var uploadHandler = new UploadHandle();
                string result = uploadHandler.Upload(file);

                // Check if upload was successful
                if (result.StartsWith("Extension is not valid") || result.StartsWith("Maximum size can be"))
                {
                    return BadRequest(new { message = result });
                }

                // Update avatar URL in the database
                string avatarUrl = $"/Uploads/{result}"; // Assuming uploads are served from this path
                await _userService.UpdateUserAvatarAsync(userId, avatarUrl);

                return Ok(new { message = "Avatar updated successfully", avatarUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("get-zodiac")]
        public IActionResult GetZodiacAndColors([FromQuery] DateOnly birthDate)
        {
            var zodiacService = new ZodiacService();
            try
            {
                var (zodiacSign, colors) = zodiacService.GetZodiacSignAndColors(birthDate);
                return Ok(new
                {
                    ZodiacSign = zodiacSign,
                    SuggestedColors = colors
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
