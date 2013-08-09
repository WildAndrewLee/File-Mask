using System.Drawing;
using File_Mask.lib;
using System.IO;
using System.Collections;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.0
 */
namespace File_Mask
{
	/**
	 * Masks and unmasks files.
	 */
	class Masker
	{
		/**
		 * Returns every pixel in a Bitmap image as a Color object.
		 */
		private static Color[] GetPixels(Bitmap img)
		{
			int width = img.Width;
			int height = img.Height;
			var pixels = new Color[width * height];

			//Put pixels in the array from left to right.
			for(int y = 0, h = img.Height; y < h; y++)
			{
				for(int x = 0, w = img.Width; x < w; x++)
				{
					pixels[y * width + x] = img.GetPixel(x, y);
				}
			}

			return pixels;
		}

		/**
		 * Creates a new Bitmap image from an array of Color objects given a width and height.
		 */
		private static Bitmap CreateImage(int width, int height, Color[] pixels)
		{
			var img = new Bitmap(width, height);

			for (int y = 0, h = height; y < h; y++)
			{
				for (int x = 0, w = width; x < w; x++)
				{
					img.SetPixel(x, y, pixels[y * width + x]);
				}
			}

			return img;
		}

		/**
		 * Masks a file with an image given a file and image.
		 */
		public static string[] Mask(string file, string image)
		{
			//Create a new Bitmap objects and map pixels/bits.
			var img = new Bitmap(image);

			var pixels = GetPixels(img);
			var pixelBits = new int[pixels.Length];

			for(int n = 0, length = pixels.Length; n < length; n++)
				pixelBits[n] = Util.ColorToBits(pixels[n]);

			//Read all bits from the file to mask.
			byte[] bytes = File.ReadAllBytes(file);
			var bits = new BitArray(bytes);

			//Merge bits.
			pixelBits = Merge(bits, pixelBits);

			//Generate pixel colors based on each the merged bits.
			for (int n = 0, length = pixelBits.Length; n < length; n++)
				pixels[n] = Util.BitsToColor(pixelBits[n]);

			var maskedImage = CreateImage(img.Width, img.Height, pixels);

			//Save the new image.
			var dir = Path.GetDirectoryName(image);
			var name = Path.GetFileNameWithoutExtension(image);
			var extension = Path.GetExtension(image);

			maskedImage.Save(dir + "\\" + name + " - Masked" + extension);

			//Generate AES keys and return.
			var hash = Crypt.Encrypt(Path.GetFileName(file) + ":" + bits.Length);

			return hash;
		}

		/**
		 * Merges colors represented as 32-bit integers with an array of bits
		 * from a given file.
		 */
		private static int[] Merge(BitArray bits, int[] pixels)
		{
			//Clone pixels to prevent destruction.
			var newPixels = (int[])pixels.Clone();

			for (int n = 0, length = bits.Length; n < length; n += 3)
			{
				//Calculate the bit depth to use.
				var depth = n / newPixels.Length;

				//Select the pixel to operate on and get the color channels.
				var index = (n / 3) % newPixels.Length;
				var pixel = newPixels[index];

				//Get channel values and create a bool representation.
				var r = (pixel >> 16) & 0xFF;
				var g = (pixel >> 8) & 0xFF;
				var b = pixel & 0xFF;

				var red = bits[n];
				var green = n + 1 < length && bits[n + 1];
				var blue = n + 2 < length && bits[n + 2];

				//Set pixel channel values based on the above bools.
				if (red) r = r | 1 << depth;
				else r = r & ~(1 << depth);

				if (green) g = g | 1 << depth;
				else g = g & ~(1 << depth);

				if (blue) b = b | 1 << depth;
				else b = b & ~(1 << depth);

				newPixels[index] = Util.BitsToColor(r, g, b);
			}

			return newPixels;
		}

		/**
		 * Creates a file given an image and a valid hash and key.
		 */
		public static void Unmask(string image, string hash, string key)
		{
			//Create a new Bitmap object and decrypt the AES hash.
			var img = new Bitmap(image);
			string info = Crypt.Decrypt(hash, key);

			if (info != null)
			{
				var name = info.Split(':')[0];
				var size = int.Parse(info.Split(':')[1]);

				//Get pixel data and extract bits from the data.
				var pixels = GetPixels(img);
				var pixelBits = new int[pixels.Length];

				for (int n = 0, length = pixels.Length; n < length; n++)
					pixelBits[n] = Util.ColorToBits(pixels[n]);

				var file = new byte[size / 8];

				Extract(pixelBits, size).CopyTo(file, 0);

				//Write the bits to a file.
				File.WriteAllBytes(Path.GetDirectoryName(image) + "\\" + name, file);
			}
		}

		/**
		 * Returns a BitArray object containing the bits extracted from
		 * the pixel array "bits".
		 */
		private static BitArray Extract(int[] bits, int size)
		{
			var file = new BitArray(size);

			for (int n = 0; n < size; n += 3)
			{
				int depth = n / bits.Length;
				Color color = Util.BitsToColor(bits[n / 3]);

				int r = color.R;
				int g = color.G;
				int b = color.B;

				r = r & 1 << depth;
				g = g & 1 << depth;
				b = b & 1 << depth;

				file.Set(n, Util.IntToBool(r));
				if (n + 1 < size) file.Set(n + 1, Util.IntToBool(g));
				if (n + 2 < size) file.Set(n + 2, Util.IntToBool(b));
			}

			return file;
		}
	}
}