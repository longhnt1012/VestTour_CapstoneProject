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
            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Define expiration time (e.g., 5 minutes)
            var otpExpiry = TimeSpan.FromMinutes(5);

            // Store OTP in cache with expiry time, using email as key
            _cache.Set(email, otp, otpExpiry);

            return otp;
        }

        public bool ValidateOtp(string email, string otp)
        {
            // Try to retrieve the OTP from cache
            if (_cache.TryGetValue(email, out string cachedOtp))
            {
                // Check if provided OTP matches the cached OTP
                if (cachedOtp == otp)
                {
                    // Remove OTP from cache after successful validation
                    _cache.Remove(email);
                    return true;
                }
            }

            return false; // OTP is either expired or doesn't match
        }
    }
}
