using System.IO;
using System.Security.Cryptography;

namespace Zicore.Security.Cryptography
{
    public class RijndaelSimple
    {
        private const int PasswordIterations = 1000;

        private readonly byte[] _initVector = new byte[]
            {
                2, 5, 6, 1, 51, 55, 5, 1, 66, 1, 5, 231, 5, 1, 7, 5
            };

        private readonly byte[] _saltBuffer = new byte[]
            {
                2, 5, 6, 1, 51, 55, 5, 1, 66, 1, 5, 231, 5, 66, 1, 1, 55, 55, 3, 5, 61, 5, 6, 15, 56, 2, 45, 139, 50, 24
            };

        private byte[] _passwordBuffer;

        public RijndaelSimple(byte[] passwordBuffer)
        {
            _passwordBuffer = passwordBuffer;
        }

        public byte[] Password
        {
            get { return _passwordBuffer; }
            set { _passwordBuffer = value; }
        }

        public byte[] Encrypt(byte[] buffer, int keySize)
        {
            byte[] plainTextBytes = buffer;

            var password = new Rfc2898DeriveBytes(_passwordBuffer, _saltBuffer, PasswordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, _initVector);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherTextBytes = memoryStream.ToArray();
                    return cipherTextBytes;
                }
            }
        }

        public byte[] Decrypt(byte[] buffer, int keySize)
        {
            byte[] cipherTextBytes = buffer;

            var password = new Rfc2898DeriveBytes(_passwordBuffer, _saltBuffer, PasswordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, _initVector);
            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    var plainTextBytes = new byte[cipherTextBytes.Length];
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    byte[] plainFile = plainTextBytes;
                    return plainFile;
                }
            }
        }
    }
}