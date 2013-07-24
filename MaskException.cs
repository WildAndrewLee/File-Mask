using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		private string notice = null;

		public MaskException(string notice)
		{
			this.notice = notice;
		}

		public string getNotice()
		{
			return this.notice;
		}
	}
}