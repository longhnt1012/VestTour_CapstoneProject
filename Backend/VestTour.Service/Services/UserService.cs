using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Service.Interfaces;
using VestTour.ValidationHelpers;
using VestTour.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using VestTour.Repository.Models;
using VestTour.Service.Helpers;
using VestTour.Repository.Helpers;
using Microsoft.AspNetCore.Http;

namespace VestTour.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailHelper _emailHelper;

        public UserService(IUserRepository userRepository, IEmailHelper emailHelper)
        {
            _userRepository = userRepository;
            _emailHelper = emailHelper;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
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
            var hashedPassword = PasswordHelper.HashPassword(user.Password);
            var newUser = new UserModel
            {
                Name = user.Name,
                Gender = user.Gender,
                Address = user.Address,
                Dob = user.Dob,
                Email = user.Email,
                Phone = user.Phone,
                Password = hashedPassword,  // Store the hashed password
                Status = "active",
                IsConfirmed = true,
                RoleId = user.RoleId,
            };
            return await _userRepository.AddUserAsync(newUser);
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
        }
        public async Task<List<UserModel>> GetUsersByRoleAsync(int roleId)
        {
            return await _userRepository.GetUsersByRoleIdAsync(roleId);
        }

        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new ArgumentNullException("User not found.");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

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
        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return Error.UserNotFound;
            }

            var resetToken = Guid.NewGuid().ToString();
            user.RefreshToken = resetToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateUserAsync(user);

            var resetLink = $"http://localhost:5173/new-password?token={resetToken}";
            var emailContent = $"Please reset your password by clicking here: {resetLink}";
            await _emailHelper.SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = "Password Reset Request",
                Content = emailContent
            });
            return Success.ResetEmailSent;
        }
        public async Task<string> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetUserByResetTokenAsync(token);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Error.InvalidToken; // Handle invalid token case
            }

            // Hash the new password
            user.Password = PasswordHelper.HashPassword(newPassword);
            user.RefreshToken = null; // Clear the reset token
            user.RefreshTokenExpiryTime = null; // Clear the expiration time

            await _userRepository.UpdatePasswordUser(user.UserId, user); // Ensure this method updates the user

            return Success.PasswordResetSuccess; // Success message
        }
        public async Task<string?> GetEmailByUserIdAsync(int? userId)
        {
            return await _userRepository.GetEmailByUserIdAsync(userId);
        }
        public async Task UpdateUserAvatarAsync(int userId, string avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
            {
                throw new ArgumentException("Avatar URL cannot be empty.");
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.AvtUrl = avatarUrl; // Assuming `Avatar` is the property in the `User` entity
            await _userRepository.UpdateUserAsync(user);
        }




    }
}
