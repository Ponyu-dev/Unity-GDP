using UnityEngine;
using System.IO;
using System.Security.Cryptography;

namespace SaveSystem.Utils
{
    internal class EncryptionUtils
    {
        private readonly string _key = SystemInfo.deviceUniqueIdentifier;
        private readonly byte[] _salt = { 0x43, 0x23, 0x50, 0x6F, 0x6E, 0x59, 0x75 };
        
        public byte[] Encrypt(byte[] input)
        {
            using var aes = CreateAes();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(input, 0, input.Length);
            cs.Close();

            return ms.ToArray();
        }

        public byte[] Decrypt(byte[] input)
        {
            using var aes = CreateAes();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(input, 0, input.Length);
            cs.Close();

            return ms.ToArray();
        }

        private AesManaged CreateAes()
        {
            var aes = new AesManaged();
            var pdb = new PasswordDeriveBytes(_key, _salt);

            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);

            return aes;
        }
    }
}