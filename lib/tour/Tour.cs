using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.0
 */

namespace File_Mask.lib.tour
{
	/**
	 * Contains utilities for the construction and
	 * destruction of a knight's tour.
	 */

	internal class Tour
	{
		public Square[,] Board;
		public int Height;
		public int Width;
		private Square _path;

		public Tour(int width, int height)
		{
			Board = new Square[height, width];

			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
					Board[y, x] = new Square(x, y);

			Width = width;
			Height = height;
		}

		/**
		 * Checks whether or not the given dimensions are valid
		 * for a knight's tour.
		 */

		public static bool IsValid(int width, int height)
		{
			if (width%2 == 1 && height%2 == 1)
				return false;
			if (width == 1 || width == 2 || width == 4)
				return false;
			if ((height == 4 || height == 6 || height == 8) && width == 3)
				return false;

			return true;
		}

		public bool InBounds(int x, int y)
		{
			return (x >= 0 && x < Width) && (y >= 0 && y < Height);
		}

		/**
		 * Basic constructor. Does not conduct any dimension
		 * checks.
		 */

		/**
		 * Creates a knight's tour starting from a random
		 * square.
		 */

		public Square CreateTour()
		{
			var rand = new Random();
			Square square = Board[rand.Next(0, Height), rand.Next(0, Width)];
			Square current = square;

			do
			{
				current.Visited = true;
				current.Next = FindNext(current);
				current = current.Next;
			} while (current != null);

			_path = square;
			return square;
		}

		/**
		 * Creates a list of all possible moves.
		 */

		public List<Square> GetMoves(Square square)
		{
			var list = new List<Square>();

			int x = square.X;
			int y = square.Y;

			if (InBounds(x + 2, y - 1) && !Board[y - 1, x + 2].Visited)
				list.Add(Board[y - 1, x + 2]);
			if (InBounds(x + 2, y + 1) && !Board[y + 1, x + 2].Visited)
				list.Add(Board[y + 1, x + 2]);
			if (InBounds(x - 2, y - 1) && !Board[y - 1, x - 2].Visited)
				list.Add(Board[y - 1, x - 2]);
			if (InBounds(x - 2, y + 1) && !Board[y + 1, x - 2].Visited)
				list.Add(Board[y + 1, x - 2]);
			if (InBounds(x - 1, y + 2) && !Board[y + 2, x - 1].Visited)
				list.Add(Board[y + 2, x - 1]);
			if (InBounds(x - 1, y - 2) && !Board[y - 2, x - 1].Visited)
				list.Add(Board[y - 2, x - 1]);
			if (InBounds(x + 1, y + 2) && !Board[y + 2, x + 1].Visited)
				list.Add(Board[y + 2, x + 1]);
			if (InBounds(x + 1, y - 2) && !Board[y - 2, x + 1].Visited)
				list.Add(Board[y - 2, x + 1]);

			return list;
		}

		/**
		 * Finds a next possible square.
		 */

		public Square FindNext(Square square)
		{
			List<Square> moves = GetMoves(square);

			if (Util.IntToBool(moves.Count))
				return moves.OrderBy(s => GetMoves(s).Count).First();

			return null;
		}

		/**
		 * Exports the current path to a string.
		 */

		public String Export()
		{
			if (_path != null)
			{
				string path = "";
				Square current = _path;

				while (current != null)
				{
					path += current.X + "," + current.Y + ";";
					current = current.Next;
				}

				File.WriteAllText(@"C:\Users\Andrew\Desktop\test\original path.txt", path);

				return path;
			}

			return null;
		}

		/**
		 * Creates a tour based on the imported path.
		 */

		public Square Import(String export)
		{
			File.WriteAllText(@"C:\Users\Andrew\Desktop\test\import path.txt", export);

			string[] squares = export.Trim().Split(';');
			var square = new Square(-1, -1)
			{
				X = int.Parse(squares[0].Split(',')[0]),
				Y = int.Parse(squares[0].Split(',')[1])
			};

			Square current = square;

			for (int n = 1; n < squares.Length - 1; n++)
			{
				var next = new Square(-1, -1)
				{
					X = int.Parse(squares[n].Split(',')[0]),
					Y = int.Parse(squares[n].Split(',')[1])
				};

				current.Next = next;
				current = next;
			}

			_path = square;
			return square;
		}
	}
}