using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Implementation
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

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users
                .Include(u => u.Role) // Đảm bảo lấy thông tin vai trò
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
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

        public async Task UpdateUserAsync(int id, UserModel user)
        {
            var updateUser = _mapper.Map<User>(user);
            _context.Users.Update(updateUser);
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

        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<string?> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Role?.RoleName;
        }
       

    }
}
