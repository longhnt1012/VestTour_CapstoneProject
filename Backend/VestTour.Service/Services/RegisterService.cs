using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.ValidationHelpers;
using VestTour.Repository.Constants;
using VestTour.Service.Interface;
using VestTour.Service.Helpers;

namespace VestTour.Service.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;

        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            // Hash the password before storing it
            var hashedPassword = PasswordHelper.HashPassword(registerModel.Password);

            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.Dob,
                Email = registerModel.Email,
                Phone=registerModel.Phone,
                Password = hashedPassword,  // Store the hashed password
                Status = "active",
                IsConfirmed = true,
                RoleId = 3
            };

            var result = await _userRepository.AddUserAsync(newUser);

            return result > 0 ? Success.RegistrationSuccess : Error.RegistrationFailed;
        }
    }
}
