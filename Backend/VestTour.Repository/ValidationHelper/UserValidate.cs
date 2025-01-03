using System.Text.RegularExpressions;

namespace VestTour.ValidationHelpers
{
    public static class UserValidate
    {
        public static bool IsValidEmail(string email)
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        public static bool IsValidPassword(string password)
        {
            return password.Length >= 6 && password.Length <= 18;
        }
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 4 && name.Length <= 25;
        }

         public static bool IsValidGender(string gender)
        {
            return gender == "Male" || gender == "Female";
        }
        public static bool IsValidPhone(string? phone)
        {
            if (phone == null)
            {
                return true; // Hoặc false tùy vào yêu cầu của bạn
            }

            string phoneRegex = @"^(\+?\d{1,3})?(\d{9,15})$";
            return Regex.IsMatch(phone, phoneRegex);
        }

    }
}
