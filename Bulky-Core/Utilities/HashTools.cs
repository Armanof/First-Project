using Bulky_Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bulky_Core.Utilities
{
    public static class HashTools
    {
        public static (string hash, string salt) GetHashRfc2898(string password, string? salt = null)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            if (salt == null)
            {
                salt = Convert.ToBase64String(saltBytes);
            }
            else
            {
                saltBytes = Convert.FromBase64String(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);

            string hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }
    }
}
