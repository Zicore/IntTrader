using System;
using System.Security.Cryptography;
using System.Text;
using Zicore.Security.Cryptography;

namespace Zicore.Settings.Json
{
    public class JsonSettingsEncrypted : JsonSettings
    {
        public JsonSettingsEncrypted(String key)
        {
            SetKey(key);
        }

        public JsonSettingsEncrypted()
        {

        }

        private readonly SHA256 _sha256 = new SHA256Managed();
        private byte[] _key;

        public void SetKey(String stringKey)
        {
            if (String.IsNullOrWhiteSpace(stringKey))
                throw new ArgumentException("Argument must be not null and not whitespace", "stringKey");
            stringKey = stringKey.Trim();
            _key = _sha256.ComputeHash(Encoding.UTF8.GetBytes(stringKey));
        }

        protected override byte[] LoadFilter(byte[] data)
        {
            if (_key == null || _key.Length == 0)
                throw new CryptographicException("Key not set");

            var aes = new RijndaelSimple(_key);
            data = aes.Decrypt(data, 256);
            return base.LoadFilter(data);
        }

        protected override byte[] SaveFilter(byte[] data)
        {
            if (_key == null || _key.Length == 0)
                throw new CryptographicException("Key not set");

            var aes = new RijndaelSimple(_key);
            data = aes.Encrypt(data, 256);
            return base.SaveFilter(data);
        }
    }
}
