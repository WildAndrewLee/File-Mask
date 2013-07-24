using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

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
	class Crypt
	{
		public const int SHA256 = 1;
		public const int SHA512 = 2;
		public const int MD5 = 3;

		/**
		 * Creates a hash based on the specified string and hashing
		 * algorithm.
		 */
		public static string hash(string str, int mode)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);

			HashAlgorithm algorithm = null;

			switch (mode)
			{
				case Crypt.SHA256:
					algorithm = new SHA256Managed();
					break;

				case Crypt.SHA512:
					algorithm = new SHA512Managed();
					break;

				case Crypt.MD5:
					algorithm = new MD5CryptoServiceProvider();
					break;
			}

			bytes = algorithm.ComputeHash(bytes);

			string hash = "";

			foreach(byte b in bytes) hash += String.Format("{0:x2}", b);

			return hash;
		}

		/**
		 * Encrypts a specified string using AES encryption and returns
		 * the encrypted string and key used.
		 */
		public static string[] encrypt(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);

			AesManaged manager = new AesManaged();
			manager.KeySize = 256;
			manager.Padding = PaddingMode.PKCS7;
			manager.GenerateKey();
			manager.GenerateIV();

			ICryptoTransform c = manager.CreateEncryptor();

			bytes = c.TransformFinalBlock(bytes, 0, bytes.Length);

			string hash = BitConverter.ToString(manager.IV).Replace("-", "");
			hash += BitConverter.ToString(bytes).Replace("-", "");
			string key = BitConverter.ToString(manager.Key).Replace("-", "");

			return new string[]{hash, key};
		}

		/**
		 * Decrypts an AES hash using the specified key.
		 */
		public static string decrypt(string hash, string key)
		{
			try
			{
				AesManaged manager = new AesManaged();
				manager.KeySize = 256;
				manager.Padding = PaddingMode.PKCS7;
				manager.Key = Util.HexToBytes(key);
				manager.IV = Util.HexToBytes(hash.Substring(0, 32));

				ICryptoTransform c = manager.CreateDecryptor();

				byte[] bytes = Util.HexToBytes(hash.Substring(32));
				bytes = c.TransformFinalBlock(bytes, 0, bytes.Length);

				return Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("Invalid hash/key specified.", "Error");
				return null;
			}
		}
	}
}