using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using File_Mask.lib;
using System.IO;
using System.Threading;

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
		private void hideAes()
		{
			Animator animator = new Animator(this);
			Animator aes = new Animator(aesContainer);

			animator.resize(344, 169, 100);
			aes.resize(344, 0, 100);
		}

		/**
		 * Show AES fields.
		 */
		private void showAes()
		{
			Animator animator = new Animator(this);
			Animator aes = new Animator(aesContainer);

			animator.resize(344, 197, 100);
			aes.resize(344, 55, 100);
		}

		/**
		 * Mask the specified image and file.
		 */
		private void maskImage()
		{
			run.Text = "Running...";

			string[] h = null;

			//New thread so the GUI thread doesn't get clogged.
			Thread thread = new Thread(() =>
				{
					h = Masker.mask(selectedFile.Text, selectedImage.Text);
				}
			);

			thread.Start();
			thread.Join();

			hash.Text = h[0];
			key.Text = h[1];

			run.Text = "Run File Mask";

			showAes();
		}

		/**
		 * Unmask the file from an image.
		 */
		private void unmaskImage()
		{
			run.Text = "Running...";

			//Prevent the GUI thread from getting clogged.
			Thread thread = new Thread(() =>
				{
					Masker.unmask(selectedImage.Text, hash.Text, key.Text);
				}
			);

			thread.Start();
			thread.Join();

			run.Text = "Run File Mask";
		}

		/**
		 * Creates a file open dialog filter based on the Formats enum.
		 */
		public static string enumToFilter(string label)
		{
			string filter = label + "|";
			string[] types = Enum.GetNames(typeof(Formats));

			foreach (string type in types)
				filter += "*." + type + ";";

			return filter;
		}

		/**
		 * Check the specified image to see if it has enough pixels
		 */
		private bool verifyPixels()
		{
			//If a file is specified display the minimum number of recommended pixels.
			if (Util.IsFilled(selectedFile))
			{
				long size = new FileInfo(selectedFile.Text).Length * 8;
				int pixels = (int)(size / 6);
				value.Text = pixels.ToString();

				//If an image is specified determine of the image has enough pixels.
				if (Util.IsFilled(selectedImage))
				{
					Bitmap bmp = new Bitmap(selectedImage.Text);
					int area = bmp.Width * bmp.Height;

					if (area >= pixels) return (value.ForeColor = Color.Green) == Color.Green;
					else value.ForeColor = Color.Red;
				}
			}

			return false;
		}

		/**
		 * Confirms whether or not the user really wants to
		 * mask an image.
		 */
		private bool confirm()
		{
			DialogResult res = MessageBox.Show(Notices.TOO_LITTLE_PIXELS, "Warning!", MessageBoxButtons.OKCancel);
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

				verifyPixels();

				hideAes();
			}
		}

		/**
		 * Open the file choose dialog for the image to mask the file with.
		 */
		private void browseImage_Click(object sender, EventArgs e)
		{
			openFile.Filter = enumToFilter("Image Files");

			if (openFile.ShowDialog() == DialogResult.OK)
			{
				selectedImage.Text = openFile.FileName;

				verifyPixels();
			}
		}

		/**
		 * Perform the appropriate action.
		 */
		private void run_Click(object sender, EventArgs e)
		{
			try
			{
				if(!(applyMask.Checked || removeMask.Checked))
					throw new MaskException(Notices.MISSING_ACTION);

				if (applyMask.Checked)
				{
					if (!Util.IsFilled(selectedFile, selectedImage))
						throw new MaskException(Notices.MISSING_FILE_AND_IMAGE);
					if(!Enum.GetNames(typeof(Formats)).Contains(Path.GetExtension(selectedImage.Text)))
						throw new MaskException(Notices.INVALID_FILE_FORMAT);
					if (!(File.Exists(selectedFile.Text) && File.Exists(selectedImage.Text)))
						throw new MaskException(Notices.MISSING_RESOURCE);
					if (verifyPixels() || confirm())
						maskImage();
				}
				else if (removeMask.Checked)
				{
					if (!Util.IsFilled(selectedImage, hash, key))
						throw new MaskException(Notices.MISSING_AES);
					if(!Enum.GetNames(typeof(Formats)).Contains(Path.GetExtension(selectedImage.Text)))
						throw new MaskException(Notices.INVALID_FILE_FORMAT);
					if (!File.Exists(selectedImage.Text))
						throw new MaskException(Notices.MISSING_IMAGE);

					unmaskImage();
				}
			}
			catch (MaskException ex)
			{
				MessageBox.Show(ex.getNotice(), "Error");
			}
		}

		/**
		 * Hide AES fields.
		 */
		private void applyMask_CheckedChanged(object sender, EventArgs e)
		{
			if (applyMask.Checked)
			{
				hideAes();

				hash.ReadOnly = key.ReadOnly = true;
				hash.Text = key.Text = "";
			}
		}

		/**
		 * Show AES fields.
		 */
		private void removeMask_CheckedChanged(object sender, EventArgs e)
		{
			if (removeMask.Checked)
			{
				showAes();

				hash.ReadOnly = key.ReadOnly = false;
			}
		}

		/**
		 * Make it user friendly.
		 */
		private void hash_Click(object sender, EventArgs e)
		{
			hash.SelectAll();
		}

		private void key_Click(object sender, EventArgs e)
		{
			key.SelectAll();
		}
	}
}
