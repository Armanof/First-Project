using Bulky_Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bulky_Core.Utilities
{
    public static class PasswordHelper
    {
        public static PasswordStrengthEnum CheckPasswordStrength(string password)
        {
            int score = 0;
            if (password.Length >= 8)
                score++;
            if (Regex.IsMatch(password, @"[a-z]"))
                score++;
            if (Regex.IsMatch(password, @"[A-Z]"))
                score++;
            if (Regex.IsMatch(password, @"[0-9]"))
                score++;
            if (Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                score++;

            switch (score)
            {
                case 5: return PasswordStrengthEnum.Strong;
                case 4: return PasswordStrengthEnum.Good;
                case 3: return PasswordStrengthEnum.Medium;
                default: return PasswordStrengthEnum.Weak;
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            string enteredHash = Convert.ToBase64String(hashBytes);

            return enteredHash == storedHash;
        }
    }
}
