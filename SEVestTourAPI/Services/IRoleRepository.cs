﻿using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/IRoleRepository.cs
namespace SEVestTourAPI.Services
========
namespace VestTour.Repository.Interface
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Interfaces/IRoleRepository.cs
{
    public interface IRoleRepository
    {
        Task<List<RoleModel>> GetAllRolesAsync();       // Get all roles
        Task<RoleModel?> GetRoleByIdAsync(int roleId);  // Get role by ID
        Task<int> AddRoleAsync(RoleModel role);         // Add a new role
        Task UpdateRoleAsync(int id, RoleModel role);   // Update an existing role
        Task DeleteRoleAsync(int roleId);               // Delete a role by ID
    }
}
