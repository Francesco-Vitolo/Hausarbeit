using System;
using System.Security.Cryptography;
using System.Text;

namespace Verwaltungsprogramm_Vinothek
{
    public static class Encrypt
    {
        private const string salt = "081c721a82cc9412921ca7a0a476c7273dbe8f9f5a2b61cb2e5471f2b349527a";
        public static string GetHash(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text + salt));
                string hash = BitConverter.ToString(hashedBytes);
                return hash.Replace("-", "").ToLower();
            }
        }
    }
}
