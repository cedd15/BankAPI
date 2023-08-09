using System.Security.Cryptography;
using System.Text;

namespace BankAPI.Helper
{
    public static class AccountHelper
    {
        public static string HashPassword(string password)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
