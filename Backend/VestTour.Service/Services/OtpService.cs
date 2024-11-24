using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Service.Services
{
    public class OtpService
    {
        private readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateAndStoreOtp(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            var otpExpiry = TimeSpan.FromMinutes(1);

            _cache.Set(email, otp, otpExpiry);

            return otp;
        }

        public bool ValidateOtp(string email, string otp)
        {
            if (_cache.TryGetValue(email, out string cachedOtp))
            {
                if (cachedOtp == otp)
                {
                    _cache.Remove(email);
                    return true;
                }
            }

            return false; 
        }
    }
}