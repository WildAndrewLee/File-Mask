/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.2
 */

namespace File_Mask.lib.tour
{
	internal class Square
	{
		public Square Next;
		public bool Visited;
		public int X;
		public int Y;

		public Square(int x, int y)
		{
			X = x;
			Y = y;
			Visited = false;
			Next = null;
		}
	}
}