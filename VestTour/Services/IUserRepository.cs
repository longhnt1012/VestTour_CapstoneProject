using VestTour.Models;

namespace VestTour.Services
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllUserAsync();
        public Task<UserModel?> GetUserByIdAsync(int userId);

        public Task<int> AddUserAsync(UserModel user);

        public Task UpdateUserAsync(int id, UserModel user);

        
    }
}
