using Microsoft.AspNetCore.Mvc;
<<<<<<<< Updated upstream:SEVestTourAPI/Controllers/RoleController.cs
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
========
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
>>>>>>>> Stashed changes:Backend/VestTour.API/Controllers/RoleController.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return Ok(roles);
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetRole(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<int>> CreateRole(RoleModel roleModel)
        {
            var newRoleId = await _roleRepository.AddRoleAsync(roleModel);
            return CreatedAtAction(nameof(GetRole), new { id = newRoleId }, newRoleId);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleModel roleModel)
        {
            if (id != roleModel.RoleId)
            {
                return BadRequest("Role ID mismatch.");
            }

            var existingRole = await _roleRepository.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound();
            }

            await _roleRepository.UpdateRoleAsync(id, roleModel);
            return NoContent();
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleRepository.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
