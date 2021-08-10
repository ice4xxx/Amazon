using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Encryption
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt value with sha256.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Sha256(string value)
        {
            using (SHA256 mySha256 = SHA256.Create())
            {
                var bytes = mySha256.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder sb = new StringBuilder();

                foreach (var @byte in bytes)
                {
                    sb.Append(@byte.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
