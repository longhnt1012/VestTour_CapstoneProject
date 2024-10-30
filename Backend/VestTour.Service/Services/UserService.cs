using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using VestTour.ValidationHelpers;
using VestTour.Domain.Enums;
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
=======
using VestTour.Repository.Models;
>>>>>>> Stashed changes

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
            // Validate the status by checking if it's a valid enum value
            if (!Enum.TryParse<StatusEnums>(status, true, out var parsedStatus))
            {
                throw new ArgumentException(Error.InvalidUserStatus);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the user's status
            user.Status = parsedStatus.ToString(); // Convert enum to string
            await _userRepository.UpdateUserStatusAsync(userId, user.Status);
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

            if (!Enum.TryParse<GenderEnums>(user.Gender, true, out var gender) || gender == GenderEnums.Unknown)
            {
                throw new ArgumentException(Error.InvalidGender);
            }

            if (!UserValidate.IsValidPhone(user.Phone))
            {
                throw new ArgumentException(Error.InvalidPhone);
            }
        }

        // Validation for updating user fields
        private void ValidateUpdateUserFields(UpdateUserModel user)
        {
            if (!UserValidate.IsValidEmail(user.Email))
            {
                throw new ArgumentException(Error.InvalidEmail);
            }

            if (!UserValidate.IsValidName(user.Name))
            {
                throw new ArgumentException(Error.InvalidName);
            }

            if (!Enum.TryParse<GenderEnums>(user.Gender, true, out var gender) || gender == GenderEnums.Unknown)
            {
                throw new ArgumentException(Error.InvalidGender);
            }

            if (!UserValidate.IsValidPhone(user.Phone))
            {
                throw new ArgumentException(Error.InvalidPhone);
            }
        }


        public async Task UpdateUserAsync(int id, UpdateUserModel updateUserModel)
        {
            ValidateUpdateUserFields(updateUserModel);

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.UpdateUserAsync(id, updateUserModel);
<<<<<<< Updated upstream
=======
        }
        public async Task<List<UserModel>> GetUsersByRoleAsync(int roleId)
        {
            return await _userRepository.GetUsersByRoleIdAsync(roleId);
>>>>>>> Stashed changes
        }



<<<<<<< Updated upstream
=======
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
        }

        public async Task ClearRefreshTokenAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userRepository.UpdateUserAsync(user);
            }
        }

>>>>>>> Stashed changes
    }
}
