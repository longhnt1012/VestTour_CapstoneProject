using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SEVestTourAPI.Message;
using SEVestTourAPI.Repository.Interface;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/user
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
       // [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<int>> AddUser([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Error.InvalidModelState);
            }

            var newUserId = await _userRepository.AddUserAsync(userModel);
            return CreatedAtAction(nameof(GetUserById), new { id = newUserId }, newUserId);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "customer")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (id != userModel.UserId)
            {
                return BadRequest(Error.UserIdMismatch);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(Error.InvalidModelState);
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }

            await _userRepository.UpdateUserAsync(id, userModel);
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        // PUT: api/user/{id}/status
        [HttpPut("{id}/status")]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] string status)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(Error.UserNotFound);
            }

            await _userRepository.UpdateUserStatusAsync(id, status);
            return Ok(Success.StatusUpdated);
        }
    }
}
