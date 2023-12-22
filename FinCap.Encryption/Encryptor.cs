using System.Security.Cryptography;
using System.Text;

namespace FinCap.Encryption
{
    public static class Encryptor
    {
        public static string Encrypt(string hash, string value)
        {
            string stringToHash = string.Concat(value, hash);
            byte[] bytes = Encoding.UTF8.GetBytes(stringToHash);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    stringBuilder.Append(hashBytes[i].ToString("x2"));

                return stringBuilder.ToString();
            }
        }
    }
}
