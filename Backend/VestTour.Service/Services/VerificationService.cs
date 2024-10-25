using System;
using Microsoft.Extensions.Caching.Memory;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IMemoryCache _cache;

        public VerificationService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool VerifyCode(string emailOrPhone, string code)
        {
            return _cache.TryGetValue(emailOrPhone, out string cachedCode) && cachedCode == code;
        }

        public void SendVerificationCode(string emailOrPhone)
        {
            var code = GenerateVerificationCode();
            _cache.Set(emailOrPhone, code, TimeSpan.FromMinutes(10));
        }

        private string GenerateVerificationCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
