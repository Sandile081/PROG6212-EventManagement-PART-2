using System.Security.Cryptography;
using System.Text;

namespace EventManagementAPI.Helpers
{
    public static class PasswordHelper
    {
        private const string Salt = "RaceDay081";

      public static string Hash(string plainPassword)
     {
            var combined = plainPassword + Salt;
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(combined));
            return Convert.ToBase64String(bytes);
        }

        public static bool Verify(string plainPassword, string storedHash)
        {
           return Hash(plainPassword) == storedHash;
        }
    }
}