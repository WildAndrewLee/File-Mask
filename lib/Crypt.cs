using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.0
 */

namespace File_Mask.lib
{
	/**
	 * Contains utilities to handle security operations:
	 * Hashing, encryption, and decryption.
	 */

	internal class Crypt
	{
		public const int SHA256 = 1;
		public const int SHA512 = 2;
		public const int MD5 = 3;

		/**
		 * Creates a hash based on the specified string and hashing
		 * algorithm.
		 */

		public static string Hash(string str, int mode)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);

			HashAlgorithm algorithm = null;

			switch (mode)
			{
				case SHA256:
					algorithm = new SHA256Managed();
					break;

				case SHA512:
					algorithm = new SHA512Managed();
					break;

				case MD5:
					algorithm = new MD5CryptoServiceProvider();
					break;
			}

			Debug.Assert(algorithm != null, "algorithm != null");
			bytes = algorithm.ComputeHash(bytes);

			return bytes.Aggregate("", (current, b) => current + String.Format("{0:x2}", b));
		}

		/**
		 * Encrypts a specified string using AES encryption and returns
		 * the encrypted string and key used.
		 */

		public static string[] Encrypt(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);

			var manager = new AesManaged
			{
				KeySize = 256,
				Padding = PaddingMode.PKCS7
			};

			manager.GenerateKey();
			manager.GenerateIV();

			ICryptoTransform c = manager.CreateEncryptor();

			bytes = c.TransformFinalBlock(bytes, 0, bytes.Length);

			string hash = BitConverter.ToString(manager.IV).Replace("-", "");
			hash += BitConverter.ToString(bytes).Replace("-", "");
			string key = BitConverter.ToString(manager.Key).Replace("-", "");

			return new[] {hash, key};
		}

		/**
		 * Decrypts an AES hash using the specified key.
		 */

		public static string Decrypt(string hash, string key)
		{
			var manager = new AesManaged
			{
				KeySize = 256,
				Padding = PaddingMode.PKCS7,
				Key = Util.HexToBytes(key),
				IV = Util.HexToBytes(hash.Substring(0, 32))
			};

			ICryptoTransform c = manager.CreateDecryptor();

			byte[] bytes = Util.HexToBytes(hash.Substring(32));
			bytes = c.TransformFinalBlock(bytes, 0, bytes.Length);

			return Encoding.UTF8.GetString(bytes);
		}
	}
}