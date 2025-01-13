using System.Security.Cryptography;
using System.Text;

namespace OurSolarSystemAPI.Auth;

public class PasswordHasher
{
    public static string HashPasswordWithSalt(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
        Array.Copy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Array.Copy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(saltedPassword);

            var hashWithSalt = new byte[hashBytes.Length + saltBytes.Length];
            Array.Copy(hashBytes, 0, hashWithSalt, 0, hashBytes.Length);
            Array.Copy(saltBytes, 0, hashWithSalt, hashBytes.Length, saltBytes.Length);

            return Convert.ToBase64String(hashWithSalt);
        }
    }


    public static (byte[] bytes, string salt) GenerateSalt(int length = 16)
    {
        var saltBytes = new byte[length];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        return (saltBytes, Convert.ToBase64String(saltBytes));
    }
}