﻿using System.Text.RegularExpressions;

namespace SEVestTourAPI.ValidationHelpers
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
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 5 && name.Length <= 25;
        }

         public static bool IsValidGender(string gender)
        {
            return gender == "Nam" || gender == "Nu";
        }
    }
}
