using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private static Color[] getPixels(Bitmap img)
		{
			int width = img.Width;
			int height = img.Height;
			Color[] pixels = new Color[width * height];

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
		private static Bitmap createImage(int width, int height, Color[] pixels)
		{
			Bitmap img = new Bitmap(width, height);

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
		public static string[] mask(string file, string image)
		{
			//Create a new Bitmap objects and map pixels/bits.
			Bitmap img = new Bitmap(image);

			Color[] pixels = getPixels(img);
			int[] pixelBits = new int[pixels.Length];

			for(int n = 0, length = pixels.Length; n < length; n++)
				pixelBits[n] = Util.ColorToBits(pixels[n]);

			//Read all bits from the file to mask.
			byte[] bytes = File.ReadAllBytes(file);
			BitArray bits = new BitArray(bytes);

			//Merge bits.
			pixelBits = merge(bits, pixelBits);

			//Generate pixel colors based on each the merged bits.
			for (int n = 0, length = pixelBits.Length; n < length; n++)
				pixels[n] = Util.BitsToColor(pixelBits[n]);

			Bitmap maskedImage = createImage(img.Width, img.Height, pixels);

			//Save the new image.
			string dir = Path.GetDirectoryName(image);
			string name = Path.GetFileNameWithoutExtension(image);
			string extension = Path.GetExtension(image);

			maskedImage.Save(dir + "\\" + name + " - Masked" + extension);

			//Generate AES keys and return.
			string[] hash = Crypt.encrypt(Path.GetFileName(file) + ":" + bits.Length);

			return hash;
		}

		/**
		 * Merges colors represented as 32-bit integers with an array of bits
		 * from a given file.
		 */
		private static int[] merge(BitArray bits, int[] pixels)
		{
			//Clone pixels to prevent destruction.
			int[] newPixels = (int[])pixels.Clone();

			for (int n = 0, length = bits.Length; n < length; n += 3)
			{
				//Calculate the bit depth to use.
				int depth = n / newPixels.Length;

				//Select the pixel to operate on and get the color channels.
				int index = (n / 3) % newPixels.Length;
				int pixel = newPixels[index];


				//Get channel values and create a bool representation.
				int r = (pixel >> 16) & 0xFF;
				int g = (pixel >> 8) & 0xFF;
				int b = pixel & 0xFF;

				bool red = bits[n];
				bool green = n + 1 < length ? bits[n + 1] ? true : false : false;
				bool blue = n + 2 < length ? bits[n + 2] ? true : false : false;

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
		public static void unmask(string image, string hash, string key)
		{
			//Create a new Bitmap object and decrypt the AES hash.
			Bitmap img = new Bitmap(image);
			string info = Crypt.decrypt(hash, key);

			if (info != null)
			{
				string name = info.Split(':')[0];
				int size = int.Parse(info.Split(':')[1]);

				//Get pixel data and extract bits from the data.
				Color[] pixels = getPixels(img);
				int[] pixelBits = new int[pixels.Length];

				for (int n = 0, length = pixels.Length; n < length; n++)
					pixelBits[n] = Util.ColorToBits(pixels[n]);

				byte[] file = new byte[size / 8];

				extract(pixelBits, size).CopyTo(file, 0);

				//Write the bits to a file.
				File.WriteAllBytes(Path.GetDirectoryName(image) + "\\" + name, file);
			}
		}

		/**
		 * Returns a BitArray object containing the bits extracted from
		 * the pixel array "bits".
		 */
		private static BitArray extract(int[] bits, int size)
		{
			BitArray file = new BitArray(size);

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