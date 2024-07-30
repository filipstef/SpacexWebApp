using System.Security.Cryptography;
using System.Text;

namespace SpacexWebApp.Server.Services
{
    public class PasswordService
    {
        public static string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }

        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // Size based on security requirements
                rng.GetBytes(salt);
                return salt;
            }
        }

        public static bool VerifyPassword(string password, string storedPasswordWithSalt)
        {
            byte[] salt = new byte[16];

            byte[] storedBytes = Convert.FromBase64String(storedPasswordWithSalt);

            // Extract the salt
            byte[] storedSalt = new byte[salt.Length];
            Buffer.BlockCopy(storedBytes, 0, storedSalt, 0, storedSalt.Length);

            // Extract the hashed password
            byte[] storedHash = new byte[storedBytes.Length - salt.Length];
            Buffer.BlockCopy(storedBytes, salt.Length, storedHash, 0, storedHash.Length);

            // Hash the input password with the extracted salt
            string hashedInputPassword = HashPassword(password, storedSalt);

            // Compare the stored hash with the hash of the input password
            return storedPasswordWithSalt == hashedInputPassword;
        }
    }
}
