using UnityEngine;
using System.Security.Cryptography;
using System.IO;

namespace SaveSystem.Utils
{
	public interface IEncryptor
	{
		public byte[] Encrypt(byte[] input);
	}

	public interface IDecryptor
	{
		public byte[] Decrypt(byte[] input);
	}

	public sealed class EncryptionUtils : IEncryptor, IDecryptor
	{
		private readonly string KEY  = SystemInfo.deviceUniqueIdentifier + "_";
		private readonly byte[] SALT = new byte[] { 0x43, 0x87, 0x23, 0x72 };

		public byte[] Encrypt(byte[] input)
    	{
        	var pdb = new PasswordDeriveBytes(KEY, SALT); 
        	var ms  = new MemoryStream();
        	var aes = new AesManaged();
        	aes.Key = pdb.GetBytes(aes.KeySize / 8);
        	aes.IV  = pdb.GetBytes(aes.BlockSize / 8);
        	var cs  = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

        	cs.Write(input, 0, input.Length);
        	cs.Close();

        	return ms.ToArray();
    	}

    	public byte[] Decrypt(byte[] input)
    	{
        	var pdb = new PasswordDeriveBytes(KEY, SALT);
        	var ms  = new MemoryStream();
        	var aes = new AesManaged();
        	aes.Key = pdb.GetBytes(aes.KeySize / 8);
    	    aes.IV  = pdb.GetBytes(aes.BlockSize / 8);
	        var cs  = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

    	    cs.Write(input, 0, input.Length);
	        cs.Close();

        	return ms.ToArray();
    	}
	}
}