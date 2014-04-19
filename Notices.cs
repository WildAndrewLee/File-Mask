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
	 * Error message dictionary.
	 */

	internal class Notices
	{
		public const string MissingFileAndImage = "You must select a file and an image.";
		public const string MissingResource = "The selected image and/or file does not exist.";
		public const string MissingImage = "The selected image does not exist.";
		public const string MissingAes = "You must specify an image, hash, and key.";
		public const string MissingAction = "You must select an action to perform.";

		public const string TooLittlePixels =
			"The selected image contains too few pixels.\nFor best results please choose a larger image.";

		public const string InvalidFileFormat = "Please select a BMP/JPEG/JPG/PNG/TIFF image.";
		public const string InvalidDimensions = "The selected image does not meet the required dimension properties.";
	}
}