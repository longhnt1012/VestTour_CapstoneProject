using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Constants;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using VestTour.ValidationHelpers;

namespace VestTour.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<int> AddUserAsync(UserModel user)
        {
            ValidateUserFields(user); // Add validation here
            return await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(int id, UserModel user)
        {
            if (id != user.UserId)
            {
                throw new ArgumentException("User ID mismatch.");
            }

            ValidateUserFields(user); // Add validation here
            await _userRepository.UpdateUserAsync(id, user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<string?> GetUserRoleAsync(int userId)
        {
            return await _userRepository.GetUserRoleAsync(userId);
        }

        public async Task UpdateUserStatusAsync(int userId, string status)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            user.Status = status;
            await _userRepository.UpdateUserAsync(userId, user);
        }

        // Helper method to validate user fields
        private void ValidateUserFields(UserModel user)
        {
            if (!UserValidate.IsValidEmail(user.Email))
            {
                throw new ArgumentException(Error.InvalidEmail);
            }

            if (!UserValidate.IsValidPassword(user.Password))
            {
                throw new ArgumentException(Error.InvalidPassword);
            }

            if (!UserValidate.IsValidName(user.Name))
            {
                throw new ArgumentException(Error.InvalidName);
            }

            if (!string.IsNullOrEmpty(user.Gender) && !UserValidate.IsValidGender(user.Gender))
            {
                throw new ArgumentException(Error.InvalidGender);
            }

            if (!UserValidate.IsValidPhone(user.Phone))
            {
                throw new ArgumentException(Error.InvalidPhone);
            }
        }
    }
}
