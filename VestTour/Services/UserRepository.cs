using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Models;

namespace VestTour.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(VestTourDbContext context, IMapper mapper) 
        {
            _context =context;
            _mapper = mapper;
        }

        public async Task<int> AddUserAsync(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            _context.Users!.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser.UserId;
        }

      

        public async Task<List<UserModel>> GetAllUserAsync()
        {
            var users= await _context.Users!.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var userID = await _context.Users!.FirstOrDefaultAsync(us => us.UserId == id);
            return _mapper.Map<UserModel>(userID);
        }

        public async Task UpdateUserAsync(int id, UserModel user)
        {
            if(id == user.UserId)
            {
                var updateUser = _mapper.Map<User>(user);
                _context.Users!.Update(updateUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
