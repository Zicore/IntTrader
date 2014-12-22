using System;
using System.Security.Cryptography;
using System.Text;

namespace Zicore.Security.Cryptography
{
    public class Hash
    {
        /// <summary>
        /// Returns hash of string as hexadecimal string (lower case)
        /// </summary>
        /// <param name="textToHash">string to hash</param>
        /// <param name="crypto">Crypto algorithm</param>
        /// <param name="iterations">Amount of iterations</param>
        /// <returns>the lowercase hashed hexadecimal string.</returns>
        public static string TextToHexStringHash(string textToHash, HashAlgorithm crypto, int iterations)
        {
            if (string.IsNullOrEmpty(textToHash))
            {
                throw new ArgumentException("Hash is null or empty", "textToHash");
            }

            if (crypto == null)
                throw new ArgumentNullException("crypto");

            byte[] textToHashBytes = Encoding.Default.GetBytes(textToHash);

            byte[] result = textToHashBytes;
            for (int i = 0; i < iterations; i++)
            {
                result = crypto.ComputeHash(result);
            }
            return ToHexString(result);
        }

        public static Byte[] GetHashBytes(String t, HashAlgorithm crypto)
        {
            return crypto.ComputeHash(Encoding.UTF8.GetBytes(t));
        }

        public static Byte[] GetHashBytes(char[] t, HashAlgorithm crypto, int position)
        {
            return crypto.ComputeHash(Encoding.Default.GetBytes(t, 0, position));
        }

        public static Byte[] GetHashBytes(byte[] t, HashAlgorithm crypto)
        {
            return crypto.ComputeHash(t);
        }

        public static String ToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        }
    }
}
