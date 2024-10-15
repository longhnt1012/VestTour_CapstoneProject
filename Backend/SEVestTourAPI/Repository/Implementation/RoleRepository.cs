using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all roles
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _context.Roles!.ToListAsync();
            return _mapper.Map<List<RoleModel>>(roles);
        }

        // Get role by ID
        public async Task<RoleModel?> GetRoleByIdAsync(int roleId)
        {
            var role = await _context.Roles!.FindAsync(roleId);
            return _mapper.Map<RoleModel>(role);
        }

        // Add a new role
        public async Task<int> AddRoleAsync(RoleModel roleModel)
        {
            var newRole = _mapper.Map<Role>(roleModel);
            _context.Roles!.Add(newRole);
            await _context.SaveChangesAsync();
            return newRole.RoleId;
        }

        // Update an existing role
        public async Task UpdateRoleAsync(int id, RoleModel roleModel)
        {
            if (id == roleModel.RoleId)
            {
                var updatedRole = _mapper.Map<Role>(roleModel);
                _context.Roles!.Update(updatedRole);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a role by ID
        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles!.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles!.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
