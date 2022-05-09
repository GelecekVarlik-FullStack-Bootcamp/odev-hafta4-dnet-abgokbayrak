using System.Security.Cryptography;
using System.Text;

namespace BMS.Bll.Helper
{
    internal static class MD5
    {
        public static string Encrypt(string value)
        {
            MD5CryptoServiceProvider md5 = new();
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            buffer = md5.ComputeHash(buffer);
            var sb = new StringBuilder();
            foreach (byte ba in buffer)
                sb.Append(ba.ToString("x2").ToLower());
            return sb.ToString();
        }
    }
}