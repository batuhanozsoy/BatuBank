using System.Security.Cryptography;

namespace BatuBankClassLibary
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Salt oluşturma
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Rfc2898DeriveBytes kullanarak şifre hashleme
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Hash ve salt'ın birleştirilmesi
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Hash'in Base64 formatına dönüştürülmesi
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Base64 formatındaki hash'in geri dönüştürülmesi
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Salt'ın ayrıştırılması
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Rfc2898DeriveBytes kullanarak hash'in yeniden hesaplanması
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] enteredHash = pbkdf2.GetBytes(20);

            // Girilen hash ile kaydedilen hash'in karşılaştırılması
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != enteredHash[i])
                    return false;
            }

            return true;
        }
    }
}
