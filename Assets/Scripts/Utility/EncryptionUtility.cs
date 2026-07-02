using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public static class EncryptionUtility
    {
        // Ensure your key and IV are secure and 16, 24, or 32 bytes long
        private static readonly string SecretKey = "MySecretKey123456MySecretKey123456";
        private static readonly string SecretIv = "CustomInitVector";

        public static string Encrypt(string plainText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
            byte[] ivBytes = Encoding.UTF8.GetBytes(SecretIv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
            byte[] ivBytes = Encoding.UTF8.GetBytes(SecretIv);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(buffer);
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}