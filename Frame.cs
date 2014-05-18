using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using File_Mask.lib;
using File_Mask.Properties;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.2
 */

namespace File_Mask
{
	/**
	 * GUI backend for the program
	 */

	public partial class Frame : Form
	{
		public Frame()
		{
			InitializeComponent();
		}

		/**
		 * Hide the AES fields.
		 */

		private void HideAes()
		{
			var animator = new Animator(this);
			var aes = new Animator(aesContainer);

			animator.Resize(Width, 169, 100);
			aes.Resize(aesContainer.Width, 0, 100);
		}

		/**
		 * Show AES fields.
		 */

		private void ShowAes()
		{
			var animator = new Animator(this);
			var aes = new Animator(aesContainer);

			animator.Resize(Width, 197, 100);
			aes.Resize(aesContainer.Width, 55, 100);
		}

		/**
		 * Mask the specified image and file.
		 */

		private void MaskImage()
		{
			run.Text = Resources.Frame_MaskImage_Running___;

			Benchmark mark = new Benchmark("Time To Mask Image", true);

			string[] h = Masker.Mask(selectedFile.Text, selectedImage.Text);

			mark.Time();

			string dir = Path.GetDirectoryName(selectedImage.Text);
			string name = Path.GetFileNameWithoutExtension(selectedImage.Text);

			File.WriteAllText(dir + "\\" + name + ".hash", h[0]);
			File.WriteAllText(dir + "\\" + name + ".key", h[1]);

			run.Text = Resources.Frame_UnmaskImage_Run_File_Mask;
		}

		/**
		 * Unmask the file from an image.
		 */

		private void UnmaskImage()
		{
			run.Text = Resources.Frame_MaskImage_Running___;

			//Prevent the GUI thread from getting clogged.
			string h = File.ReadAllText(hash.Text);
			string k = File.ReadAllText(key.Text);

			Masker.Unmask(selectedImage.Text, h, k);

			run.Text = Resources.Frame_UnmaskImage_Run_File_Mask;
		}

		/**
		 * Creates a file open dialog filter based on the Formats enum.
		 */

		private static string EnumToFilter(string label)
		{
			string filter = label + "|";
			string[] types = Enum.GetNames(typeof (Formats));

			return types.Aggregate(filter, (current, type) => current + ("*." + type + ";"));
		}

		/**
		 * Check the specified image to see if it has enough pixels
		 */

		private bool VerifyPixels()
		{
			//If a file is specified display the minimum number of recommended pixels.
			if (Util.IsFilled(selectedFile))
			{
				long size = new FileInfo(selectedFile.Text).Length*8;
				var pixels = (int) (size/6);
				value.Text = pixels.ToString(CultureInfo.InvariantCulture);

				//If an image is specified determine of the image has enough pixels.
				if (Util.IsFilled(selectedImage))
				{
					var bmp = new Bitmap(selectedImage.Text);
					int area = bmp.Width*bmp.Height;

					if (area >= pixels)
					{
						value.ForeColor = Color.Green;
						return true;
					}

					value.ForeColor = Color.Red;
				}
			}

			return false;
		}

		/**
		 * Confirms whether or not the user really wants to
		 * mask an image.
		 */

		private static bool Confirm()
		{
			DialogResult res = MessageBox.Show(Notices.TooLittlePixels, Resources.Frame_Confirm_Warning_,
				MessageBoxButtons.OKCancel);
			return res == DialogResult.OK;
		}

		/**
		 * Open the file chooser dialog for the file to mask.
		 */

		private void browseFile_Click(object sender, EventArgs e)
		{
			openFile.Filter = "";

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				selectedFile.Text = openFile.FileName;

				VerifyPixels();

				HideAes();
			}
		}

		/**
		 * Open the file choose dialog for the image to mask the file with.
		 */

		private void browseImage_Click(object sender, EventArgs e)
		{
			openFile.Filter = EnumToFilter("Image Files");

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				selectedImage.Text = openFile.FileName;

				VerifyPixels();
			}
		}

		/**
		 * Open the file choose dialog for the hash to unmask with.
		 */

		private void browseHash_Click(object sender, EventArgs e)
		{
			openFile.Filter = "";

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				hash.Text = openFile.FileName;
			}
		}

		/**
		 * Open the file choose dialog for the key to unmask with.
		 */

		private void browseKey_Click(object sender, EventArgs e)
		{
			openFile.Filter = "";

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				key.Text = openFile.FileName;
			}
		}

		/**
		 * Perform the appropriate action.
		 */

		private void run_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(applyMask.Checked || removeMask.Checked))
					throw new MaskException(Notices.MissingAction);

				if (applyMask.Checked)
				{
					if (!Util.IsFilled(selectedFile, selectedImage))
						throw new MaskException(Notices.MissingFileAndImage);
					if (!(File.Exists(selectedFile.Text) && File.Exists(selectedImage.Text)))
						throw new MaskException(Notices.MissingResource);
					if (!Util.IsImage(selectedImage.Text))
						throw new MaskException(Notices.InvalidFileFormat);
					if (VerifyPixels() || Confirm())
						MaskImage();
				}
				else
				{
					if (!Util.IsFilled(selectedImage, hash, key))
						throw new MaskException(Notices.MissingAes);
					if (!File.Exists(selectedImage.Text))
						throw new MaskException(Notices.MissingImage);
					if (!Util.IsImage(selectedImage.Text))
						throw new MaskException(Notices.InvalidFileFormat);

					UnmaskImage();
				}
			}
			catch (MaskException ex)
			{
				MessageBox.Show(ex.GetNotice(), Resources.Frame_run_Click_Error);
			}
		}

		/**
		 * Hide AES fields.
		 */

		private void applyMask_CheckedChanged(object sender, EventArgs e)
		{
			if (!applyMask.Checked) return;

			HideAes();

			hash.ReadOnly = key.ReadOnly = true;
			hash.Text = key.Text = "";
		}

		/**
		 * Show AES fields.
		 */

		private void removeMask_CheckedChanged(object sender, EventArgs e)
		{
			if (removeMask.Checked)
			{
				ShowAes();

				hash.ReadOnly = key.ReadOnly = false;
			}
		}
	}
}