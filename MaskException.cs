using System;

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
	 * Used to contain custom exceptions.
	 */
	class MaskException : Exception
	{
		private readonly string _notice;

		public MaskException(string notice)
		{
			_notice = notice;
		}

		public string GetNotice()
		{
			return _notice;
		}
	}
}