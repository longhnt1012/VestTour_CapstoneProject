using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsersByRoleIdAsync(int roleId);
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);  // For login
        Task<bool> IsEmailTakenAsync(string email);  // Check if email is already taken
        //Task<UserModel?> GetUserByIdAsync(int userId);          // Get user by ID
        Task<List<UserModel>> GetAllUsersAsync();                // Get all users
        Task<int> AddUserAsync(UserModel user);                  // Add a new user
        Task UpdateUserAsync(int id, UpdateUserModel user);
        Task UpdateUserStatusAsync(int userId, string status);// Update an existing user
        Task DeleteUserAsync(int userId);                         // Delete a user by ID
        Task<string?> GetUserRoleAsync(int userId);
        public Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        public Task UpdateEmailConfirmStatusAsync(string email, string status, bool Isconfirm);
        Task<UserModel> GetUserByResetTokenAsync(string resetToken);
        Task<int> CountOnlineUsersAsync();
        public Task UpdatePasswordUser(int userId, UserModel user);
        Task<string?> GetEmailByUserIdAsync(int? userId);
        Task UpdateUserAvatarAsync(int userId, string avatarUrl);
        Task UpdateUserActivityAsync(int userId, bool isOnline);
        Task UpdateUserPassAsync(int userId, string password);

    }
}
