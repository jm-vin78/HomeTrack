using System;
using System.Security.Cryptography;
using System.Text;

namespace HomeTrack.Helpers
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Calculate control sum MD5 algorithm
        /// </summary>
        public static string ComputeHashMD5(this string text)
        {
            return MD5.Create().ComputeHash(Encoding.Default.GetBytes(text)).ToHexString();
        }

        /// <summary>
        /// Bytes to hex string
        /// </summary>
        private static string ToHexString(this byte[] bytes)
        {
            string hex = String.Empty;
            foreach (byte b in bytes)
            {
                hex += String.Format("{0:x2}", b);
            }
            return hex;
        }
    }
}
