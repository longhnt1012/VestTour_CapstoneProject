using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetUserByIdAsync(int userId);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<int> AddUserAsync(UserModel user);
        Task UpdateUserAsync(int id, UpdateUserModel user);
        Task DeleteUserAsync(int userId);
        Task<string?> GetUserRoleAsync(int userId);
        Task UpdateUserStatusAsync(int userId, string status);

    }
}
