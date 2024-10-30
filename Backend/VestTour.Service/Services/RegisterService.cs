using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using System.Threading.Tasks;
using VestTour.ValidationHelpers;
using VestTour.Repository.Constants;
using VestTour.Service.Interface;
<<<<<<< Updated upstream
=======
using VestTour.Service.Helpers;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
=======
            // Hash the password before storing it
            var hashedPassword = PasswordHelper.HashPassword(registerModel.Password);

>>>>>>> Stashed changes
            var newUser = new UserModel
            {
                Name = registerModel.Name,
                Gender = registerModel.Gender,
                Address = registerModel.Address,
                Dob = registerModel.Dob,
                Email = registerModel.Email,
<<<<<<< Updated upstream
                Password = registerModel.Password,
                Status = "active",  // Set default status
                IsConfirmed = true,
                RoleId = 3       // Set default role as Customer (RoleID = 2)
=======
                Phone=registerModel.Phone,
                Password = hashedPassword,  // Store the hashed password
                Status = "active",
                IsConfirmed = true,
                RoleId = 3
>>>>>>> Stashed changes
            };

            var result = await _userRepository.AddUserAsync(newUser);

<<<<<<< Updated upstream
            if (result > 0)
            {
                return Success.RegistrationSuccess;
            }

            return Error.RegistrationFailed;
=======
            return result > 0 ? Success.RegistrationSuccess : Error.RegistrationFailed;
>>>>>>> Stashed changes
        }
    }
}
