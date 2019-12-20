using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AndroidBinderator
{
    internal static class Util
    {
        internal static string HashMd5(Stream value)
        {
            using (var md5 = MD5.Create())
                return BitConverter.ToString(md5.ComputeHash(value)).Replace("-", "").ToLowerInvariant();
        }

        internal static string HashSha256(string value)
        {
            return HashSha256(Encoding.UTF8.GetBytes(value));
        }

        internal static string HashSha256(byte[] value)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hash = new StringBuilder();
                var hashData = sha256.ComputeHash(value);
                foreach (var b in hashData)
                    hash.Append(b.ToString("x2"));

                return hash.ToString();
            }
        }

        internal static string HashSha256(Stream value)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hash = new StringBuilder();
                var hashData = sha256.ComputeHash(value);
                foreach (var b in hashData)
                    hash.Append(b.ToString("x2"));

                return hash.ToString();
            }
        }
    }
}
