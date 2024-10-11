using SEVestTourAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);  // For login
        public Task<bool> IsEmailTakenAsync(string email);  // Check if email is already taken
        public Task<UserModel?> GetUserByIdAsync(int userId);          // Get user by ID
        public Task<List<UserModel>> GetAllUsersAsync();               // Get all users
        public Task<int> AddUserAsync(UserModel user);                 // Add new user
        public Task UpdateUserAsync(int id, UserModel user);           // Update user
        public Task DeleteUserAsync(int userId);                       // Delete user
        public Task<string?> GetUserRoleAsync(int userId);            // Get user role by user ID
    }
}
