using System;
using System.Diagnostics;

namespace File_Mask.lib
{
	/**
	 * Contains utilities to handle application benchmarking.
	 */

	internal class Benchmark
	{
		private Stopwatch _stopwatch;
		private readonly string _message;

		public Benchmark(string message)
		{
			_message = message ?? "Benchmarked";
		}

		public Benchmark(string message, bool start) : this(message)
		{
			if(start)
				Start();
		}

		public void Start()
		{
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
		}

		public void Time()
		{
			_stopwatch.Stop();
			TimeSpan time = _stopwatch.Elapsed;

			string elapsed = String.Format(
				"{0:00}:{1:00}:{2:00}.{3:00}",
				time.Hours,
				time.Minutes,
				time.Seconds,
				time.Milliseconds / 10
			);

			Console.WriteLine("-----------------");
			Console.WriteLine(_message);
			Console.WriteLine("-----------------\n");
			Console.WriteLine(elapsed + "\n");
		}
	}
}