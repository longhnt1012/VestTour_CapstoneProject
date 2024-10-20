﻿using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Repository.Constants;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
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
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
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
        //[Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<int>> AddUser([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Error.InvalidModelState);
            }

            var newUserId = await _userService.AddUserAsync(userModel);
            return CreatedAtAction(nameof(GetUserById), new { id = newUserId }, newUserId);
        }

        // PUT: api/user/{id}
        // PUT: api/user/{id}
        [HttpPut("{id}")]
       // [Authorize(Roles = "customer")]
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
        //[Authorize(Roles = "admin")]
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

        [HttpPut("{id}/status")]
       // [Authorize(Roles = "admin")]
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


    }
}
