using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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
	 * Contains various utilities to make development easier.
	 */
	class Util
	{
		/**
		 * Mimic implicit type conversion.
		 */
		public static bool IntToBool(int n)
		{
			return n != 0;
		}

		/**
		 * Returns true all if textboxes are empty else false.
		 */
		public static bool IsEmpty(params TextBox[] boxes)
		{
			return boxes.All(box => !IntToBool(box.Text.Trim().Length));
		}

		/**
		 * Returns true if all textboxes are filled else false.
		 */
		public static bool IsFilled(params TextBox[] boxes)
		{
			return boxes.All(box => IntToBool(box.Text.Trim().Length));
		}

		/**
		 * Returns true if all booleans in the array are true else false.
		 */
		public static bool All(bool[] bools)
		{
			return bools.All(test => test);
		}

		/**
		 * Converts a 32-bit integer into a Color object.
		 */
		public static Color BitsToColor(int bits)
		{
			int alpha = Convert.ToInt32(bits >> 24 & 0xFF);
			int red = (bits >> 16) & 0xFF;
			int green = (bits >> 8) & 0xFF;
			int blue = bits & 0xFF;

			Color color = Color.FromArgb(alpha, red, green, blue);

			return color;
		}

		/**
		 * Creates a color represented as a 32-bit integer
		 * from the given RGB channels.
		 */
		public static int BitsToColor(int r, int g, int b, int a = 0xFF)
		{
			return (a << 24) | (r << 16) | (g << 8) | b;
		}

		/**
		 * Convert a Color object into a 32-bit integer based on color channels.
		 */
		public static int ColorToBits(Color color)
		{
			return BitsToColor(color.R, color.G, color.B, color.A);
		}

		/**
		 * Converts a hex string into a byte array.
		 */
		public static byte[] HexToBytes(string hex)
		{
			var bytes = new byte[hex.Length / 2];

			const string charset = "0123456789ABCDEF";

			for (int n = 0, length = hex.Length / 2; n < length; n++)
			{
				var data = charset.IndexOf(hex.ElementAt(n)) * 16;
				data += charset.IndexOf(hex.ElementAt(n + 1));

				bytes[n] = (byte)data;
			}

			return bytes;
		}
	}
}