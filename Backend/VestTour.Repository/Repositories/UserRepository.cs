using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserModel>> GetUsersByRoleIdAsync(int roleId)
        {
            var users = await _context.Users
                .Where(u => u.RoleId == roleId && u.Status == "active" && u.IsConfirmed == true)
                .ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }
        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string hashedPassword)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword && u.Status == "active" && u.IsConfirmed == true);
        }




        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<int> AddUserAsync(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Status = "Active"; // Default status for new users
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.UserId;
        }
       
        public async Task UpdateUserAsync(int id, UpdateUserModel userModel)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the fields in the existing user
            _mapper.Map(userModel, existingUser);

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var deleteUser = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            if (deleteUser != null)
            {
                _context.Users.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<string?> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Role?.RoleName;
        }
        public async Task UpdateEmailConfirmStatusAsync(string email, string status, bool Isconfirm)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Status = status;
                user.IsConfirmed = Isconfirm;
                await _context.SaveChangesAsync();
            }

        }
        public async Task UpdateUserStatusAsync(int userId, string status)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the user's status based on the passed string (already validated in service)
            existingUser.Status = status;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = expiryTime;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateUserStatusAsync(string email, string status, bool isConfirmed)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Status = status;
                user.IsConfirmed = isConfirmed;
                await _context.SaveChangesAsync();
            }
        }
        //public async Task ClearRefreshTokenAsync(int userId)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user != null)
        //    {
        //        user.RefreshToken = null;
        //        user.RefreshTokenExpiryTime = null;
        //        _context.Users.Update(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<UserModel> GetUserByResetTokenAsync(string resetToken)
        {
            var token = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == resetToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);

            return _mapper.Map<UserModel>(token);
        }

        public async Task UpdatePasswordUser(int userId, UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (userId != user.UserId)
            {
                throw new ArgumentException("User ID mismatch");
            }

            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Chỉ cập nhật các thuộc tính cần thiết thay vì thay thế toàn bộ bản thể
            existingUser.Password = user.Password; // Giả sử bạn chỉ cần cập nhật mật khẩu
                                                   // Cập nhật các trường khác nếu cần thiết

            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetEmailByUserIdAsync(int? userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.Email;
        }
        public async Task UpdateUserAvatarAsync(int userId, string avatarUrl)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.AvtUrl = avatarUrl; // Assuming `Avatar` is the property in the `User` entity
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
