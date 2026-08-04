using System.Security.Cryptography;
using System.Text;

namespace Registration.Application.Common
{
    public static class PasswordHasher
    {
        public static string GenerateSalt()
        {
            byte[] bytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var computedHash = HashPassword(enteredPassword, storedSalt);
            return computedHash == storedHash;
        }
    }
}
