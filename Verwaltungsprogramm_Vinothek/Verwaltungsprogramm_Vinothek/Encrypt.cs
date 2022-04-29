using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltungsprogramm_Vinothek
{
    public static class Encrypt
    {
        private const string salt = "081c721a82cc9412921ca7a0a476c7273dbe8f9f5a2b61cb2e5471f2b349527a";
        public static string getHash(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text + salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            
        }
    }
}
