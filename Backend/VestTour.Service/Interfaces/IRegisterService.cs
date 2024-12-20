﻿using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interface
{
    public interface IRegisterService
    {
        Task<string?> RegisterUserAsync(RegisterModel registerModel);
        Task<bool> ConfirmEmailAsync(string email, string otp);
        Task<string> ResendOtpAsync(string email);
    }
}
