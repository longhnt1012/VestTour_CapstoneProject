using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);  // For login
        Task<bool> IsEmailTakenAsync(string email);  // Check if email is already taken
        Task<UserModel?> GetUserByIdAsync(int userId);          // Get user by ID
        Task<List<UserModel>> GetAllUsersAsync();                // Get all users
        Task<int> AddUserAsync(UserModel user);                  // Add a new user
        Task UpdateUserAsync(int id, UpdateUserModel user);
        Task UpdateUserStatusAsync(int userId, string status);// Update an existing user
        Task DeleteUserAsync(int userId);                         // Delete a user by ID
        Task<string?> GetUserRoleAsync(int userId);
<<<<<<< Updated upstream
=======

        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
>>>>>>> Stashed changes

    }
}
