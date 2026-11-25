using System.Security.Cryptography;

namespace DonationHub.Application.Authentications.Helpers
{
    public static class GenerateTemporaryPassword
    {
        public static string GeneratePassword(int length = 12)
        {
            const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
            const string lower = "abcdefghijkmnopqrstuvwxyz";
            const string digits = "23456789";
            const string special = "!@#$%&*";
            const string all = upper + lower + digits + special;
            var password = new char[length];
            password[0] = upper[RandomNumberGenerator.GetInt32(upper.Length)];
            password[1] = upper[RandomNumberGenerator.GetInt32(lower.Length)];
            password[2] = upper[RandomNumberGenerator.GetInt32(digits.Length)];
            password[3] = upper[RandomNumberGenerator.GetInt32(special.Length)];
            for (int i = 4; i < length; i++)
            {
                password[i] = all[RandomNumberGenerator.GetInt32(all.Length)];
            }
            return new string(password);
        }
    }
}
