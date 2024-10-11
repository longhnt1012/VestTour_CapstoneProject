using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities; 
using SEVestTourAPI.Models;   
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SEVestTourAPI.Services
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
        // Login: Check email and password
        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        // Check if email is already taken during registration
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Register new user
       

        // Add a new user
        public async Task<int> AddUserAsync(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            _context.Users!.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.UserId;
        }

        // Delete a user by ID
        public async Task DeleteUserAsync(int userId)
        {
            var deleteUser = await _context.Users!.SingleOrDefaultAsync(u => u.UserId == userId);
            if (deleteUser != null)
            {
                _context.Users!.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
        }

        // Get all users
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var users = await _context.Users!.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        // Get user by ID
        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users!.FindAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        // Update user details
        public async Task UpdateUserAsync(int id, UserModel user)
        {
            if (id == user.UserId)
            {
                var updateUser = _mapper.Map<User>(user);
                _context.Users!.Update(updateUser);
                await _context.SaveChangesAsync();
            }
        }
        // Get user role
        public async Task<string?> GetUserRoleAsync(int userId)
        {
            
            var user = await _context.Users
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Role?.RoleName; 
        }
    }
}
