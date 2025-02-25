﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsersByRoleAsync(int roleId);
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<int> AddUserAsync(UserModel user);
        Task UpdateUserAsync(int id, UpdateUserModel user);
        Task DeleteUserAsync(int userId);
        Task<string?> GetUserRoleAsync(int userId);
        Task UpdateUserStatusAsync(int userId, string status);
        Task ClearRefreshTokenAsync(int userId);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime refreshTokenExpiryTime);
       // Task UpdateLastActiveAndSetOnline(int userId, bool isonline, DateTime lastActive);
        Task<string> ForgotPassword(string email);
        Task<string?> GetEmailByUserIdAsync(int? userId);
        Task<string> ResetPasswordAsync(string token, string newPassword);
        Task UpdateUserAvatarAsync(int userId, string avatarUrl);
        Task UpdateUserActivityAsync(int userId, bool isOnline);
        Task<ServiceResponse> UpdateUserPassAsync(string email, string oldPassword, string newPassword);
    }
}
