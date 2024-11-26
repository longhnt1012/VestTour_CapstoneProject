using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Helpers;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IMeasurementRepository _measurementRepository;
        public LoginService(IUserRepository userRepository, ITokenService tokenService, IUserService userService,IMeasurementRepository measurementRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userService = userService;
            _measurementRepository = measurementRepository;
        }

        public async Task<AuthenticationResponseModel> LoginAsync(LoginModel login)
        {
            var hashedPassword = PasswordHelper.HashPassword(login.Password);
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(login.Email, hashedPassword);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");
            var measurement = await _measurementRepository.GetMeasurementByUserIdAsync(user.UserId);
            int measurementId = 0;
            if (measurement != null)
            {
                measurementId = measurement.MeasurementId;
            }
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim("measurementID", measurementId.ToString()) 
};

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Store the refresh token and expiration in the database
            await _userService.UpdateRefreshTokenAsync(user.UserId, refreshToken, DateTime.Now.AddDays(7));

            return new AuthenticationResponseModel
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }


        private async Task<string> GenerateJwtTokenAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (user.Role == null || string.IsNullOrEmpty(user.Role.RoleName))
            {
                throw new ArgumentNullException(nameof(user.Role), "User role cannot be null or empty.");
            }

            var measurement = await _measurementRepository.GetMeasurementByUserIdAsync(user.UserId);
            int measurementId = 0;
            if (measurement != null)
            {
                measurementId = measurement.MeasurementId;
            }
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim("measurementID", measurementId.ToString())
            };

            return _tokenService.GenerateAccessToken(claims);
        }
    }
}
