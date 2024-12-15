using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.ValidationHelpers;
using VestTour.Repository.Constants;
using VestTour.Service.Interface;
using VestTour.Service.Helpers;
using VestTour.Repository.Helpers;
using VestTour.Domain.Entities;

namespace VestTour.Service.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailHelper _emailHelper;
        private readonly OtpService _otpService;
        private readonly IMeasurementRepository _measurementRepository;

        public RegisterService(IUserRepository userRepository, IEmailHelper emailHelper, OtpService otpService,IMeasurementRepository measurementRepository)
        {
            _userRepository = userRepository;
            _emailHelper = emailHelper;
            _otpService = otpService;
            _measurementRepository = measurementRepository;
        }


        public async Task<string> RegisterUserAsync(RegisterModel registerModel)
        {
            if (string.IsNullOrWhiteSpace(registerModel.Name) || registerModel.Name.Length < 5 || registerModel.Name.Length > 25)
            {
                return Error.InvalidName;
            }

            if (!UserValidate.IsValidGender(registerModel.Gender))
            {
                return Error.InvalidGender;
            }

            if (!UserValidate.IsValidEmail(registerModel.Email))
            {
                return Error.InvalidEmail;
            }

            if (!UserValidate.IsValidPassword(registerModel.Password))
            {
                return Error.InvalidPassword;
            }

            if (await _userRepository.IsEmailTakenAsync(registerModel.Email))
            {
                return Error.EmailTaken;
            }
            if (!UserValidate.IsValidPhone(registerModel.Phone))
            {
                return Error.InvalidPhone;
            }
            // Hash the password before storing it
            var hashedPassword = PasswordHelper.HashPassword(registerModel.Password);
            // Generate OTP
            var otp = _otpService.GenerateAndStoreOtp(registerModel.Email);
            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.Dob,
                Email = registerModel.Email,
                Phone=registerModel.Phone,
                Password = hashedPassword,  // Store the hashed password
                Status = "Deactive",
                IsConfirmed = false,
                RoleId = 3
            };

            var result = await _userRepository.AddUserAsync(newUser);

            if (result > 0)
            {
                await _emailHelper.SendEmailAsync(new EmailRequest
                {
                    To = registerModel.Email,
                    Subject = "Confirm your registration",
                    Content = $"Your OTP for registration confirmation is: {otp}"
                });

                return Success.RegistrationSuccess;
            }
            return Error.RegistrationFailed;
        }
        public async Task<bool> ConfirmEmailAsync(string email, string otp)
        {
            // Validate OTP
            bool isOtpValid = _otpService.ValidateOtp(email, otp);

            if (isOtpValid)
            {
                await _userRepository.UpdateEmailConfirmStatusAsync(email, "Active", true);
                var user = await _userRepository.GetUserByEmailAsync(email);
                var newUserMeasure = new MeasurementModel
                {
                    UserId = user.UserId
                };
                await _measurementRepository.AddMeasurementAsync(newUserMeasure);
            }

            return isOtpValid;
        }
        public async Task<string> ResendOtpAsync(string email)
        {
            // Check if the email exists in the repository
            var userExists = await _userRepository.IsEmailTakenAsync(email);
            if (!userExists)
            {
                return Error.EmailNotFound;
            }

            // Regenerate OTP
            var otp = _otpService.GenerateAndStoreOtp(email);

            // Send the OTP via email
            await _emailHelper.SendEmailAsync(new EmailRequest
            {
                To = email,
                Subject = "Resend OTP for Registration Confirmation",
                Content = $"Your new OTP for registration confirmation is: {otp}"
            });

            return Success.OtpResentSuccess;
        }
    }
}
