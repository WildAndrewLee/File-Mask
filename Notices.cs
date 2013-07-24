using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	 * Error message dictionary.
	 */
	class Notices
	{
		public const string MISSING_FILE_AND_IMAGE = "You must select a file and an image.";
		public const string MISSING_RESOURCE = "The selected image and/or file does not exist.";
		public const string MISSING_IMAGE = "The selected image does not exist.";
		public const string MISSING_AES = "You must specify an image, hash, and key.";
		public const string MISSING_ACTION = "You must select an action to perform.";
		public const string TOO_LITTLE_PIXELS = "The selected image contains too few pixels.\nFor best results please choose a larger image.";
		public const string INVALID_FILE_FORMAT = "Please select a BMP/JPEG/JPG/PNG/TIFF image.";
	}
}
