﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
<<<<<<<< Updated upstream:SEVestTourAPI/Services/UserRepository.cs
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
========
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Implementation
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/UserRepository.cs
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

<<<<<<<< Updated upstream:SEVestTourAPI/Services/UserRepository.cs
        // Add a new user
========
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/UserRepository.cs
        public async Task<int> AddUserAsync(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Status = "Active"; // Setting the default status during registration
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.UserId;
        }

<<<<<<<< Updated upstream:SEVestTourAPI/Services/UserRepository.cs
        // Delete a user by ID
========
<<<<<<< HEAD
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
=======
        public async Task UpdateUserAsync(int id, UserModel user)
        {
            var updateUser = _mapper.Map<User>(user);
            _context.Users.Update(updateUser);
>>>>>>> bcd41fa4de15cca8d11df9b24c9fe030759871d1
            await _context.SaveChangesAsync();
        }

>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/UserRepository.cs
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

<<<<<<<< Updated upstream:SEVestTourAPI/Services/UserRepository.cs
        // Update user details
        public async Task UpdateUserAsync(int id, UserModel user)
        {
            if (id == user.UserId)
            {
                var updateUser = _mapper.Map<User>(user);
                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
            }
        }

        // Get user role
========
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/UserRepository.cs
        public async Task<string?> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.Role?.RoleName;
        }
<<<<<<< HEAD
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

=======
       
>>>>>>> bcd41fa4de15cca8d11df9b24c9fe030759871d1

    }
}