using Microsoft.AspNetCore.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace user_management_api.Helpers
{
    public class PasswordHelper

    {
        private static string hash(string password, int salt)
        {
            string combinedPassword = string.Concat(password, salt);
            byte[] passwordByte = Encoding.UTF8.GetBytes(combinedPassword);
            byte[] hashBytes = SHA256.HashData(passwordByte);
            string hashed = Convert.ToBase64String(hashBytes);
            return hashed;
        }
        public static (string hashedPassword, int salt) HashPassword(string password)
        {
            try
            {
                int salt = RandomNumberGenerator.GetInt32(1000);
                return (hash(password, salt), salt);
            }catch (Exception ex)
            {
                return (null, -1);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword, int salt) {
            try
            {
                var hashPasswordReq = hash(password, salt);
                if (hashPasswordReq != null)
                {
                    return string.Equals(hashedPassword, hashPasswordReq);
                }
                return false;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
