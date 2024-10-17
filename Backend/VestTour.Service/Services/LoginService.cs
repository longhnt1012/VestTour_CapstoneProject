using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        // Thay thế các thuộc tính này bằng các giá trị cụ thể
        private const string JWTSecretKey = "3713ab7f791c1991d3a210c5fa68c3aa"; // Đảm bảo rằng đây là một khóa bí mật an toàn
        private const string Issuer = "http://localhost:7194"; // Địa chỉ của bạn
        private const string Audience = "http://localhost"; // Địa chỉ của bạn
        private const int AccessTokenDurationInMinutes = 60; // Thời gian sống của token

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginModel login)
        {
            // Xác thực người dùng
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(login.Email, login.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            // Kiểm tra các thuộc tính của user
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(JWTSecretKey))
            {
                throw new ArgumentNullException(nameof(JWTSecretKey), "JWT secret key cannot be null or empty.");
            }

            if (user.Role == null || string.IsNullOrEmpty(user.Role.RoleName))
            {
                throw new ArgumentNullException(nameof(user.Role), "User role cannot be null or empty.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(AccessTokenDurationInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
