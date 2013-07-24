using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			foreach (TextBox box in boxes)
				if (IntToBool(box.Text.Trim().Length)) return false;

			return true;
		}

		/**
		 * Returns true if all textboxes are filled else false.
		 */
		public static bool IsFilled(params TextBox[] boxes)
		{
			foreach (TextBox box in boxes)
				if (!IntToBool(box.Text.Trim().Length)) return false;

			return true;
		}

		/**
		 * Returns true if all booleans in the array are true else false.
		 */
		public static bool all(bool[] bools)
		{
			foreach (bool test in bools) if (!test) return false;
			return true;
		}

		/**
		 * Converts a 32-bit integer into a Color object.
		 */
		public static Color BitsToColor(int bits)
		{
			int alpha = Convert.ToInt32((bits >> 24) & 0xFF);

			int red = (bits >> 16) & 0xFF;
			int green = (bits >> 8) & 0xFF;
			int blue = bits & 0xFF;

			Color color = Color.FromArgb(255, red, green, blue);

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
			byte[] bytes = new byte[hex.Length / 2];

			string charset = "0123456789ABCDEF";

			for (int n = 0, length = hex.Length / 2; n < length; n++)
			{
				int data = charset.IndexOf(hex.ElementAt(n)) * 16;
				data += charset.IndexOf(hex.ElementAt(n + 1));

				bytes[n] = (byte)data;
			}

			return bytes;
		}
	}
}