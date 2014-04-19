using System;
using System.Drawing;
using System.Windows.Forms;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.2
 */

namespace File_Mask.lib
{
	/**
	 * Used to animate the size of various window controls.
	 */

	internal class Animator
	{
		private readonly Control _control;
		private int _height = -1;
		private Size _size = new Size(-1, -1);
		private int _tick = -1;
		private int _ticks = -1;
		private Timer _timer;
		private int _width = -1;

		/**
		 * Constructor.
		 */

		public Animator(Control control)
		{
			_control = control;
			_size = control.Size;

			Bootstrap();
		}

		/**
		 * Sets up various elements of the animator on creation.
		 */

		private void Bootstrap()
		{
			_timer = new Timer
			{
				Interval = 1000/60
			};

			_timer.Tick += TimerTick;
		}

		/**
		 * Resets the animator.
		 */

		private void Reset()
		{
			_ticks = _height = _width = 0;
			_timer.Stop();
			_timer.Enabled = false;
		}

		/**
		 * Executes every time to timer ticks and resizes the specified control.
		 */

		private void TimerTick(Object myObject, EventArgs myEventArgs)
		{
			/**
			 * Check to see if this is the last tick.
			 * If it is then set the control to the given desired size.
			 */
			if (_tick == _ticks)
			{
				_control.Size = new Size(_width, _height);

				Reset();
			}
			else
			{
				/**
				 * Calculate the interval at which to resize the control.
				 * Apply the new size to the control.
				 */
				double widthInteveral = (_width - _size.Width)/_ticks;
				double heightInterval = (_height - _size.Height)/_ticks;

				var newWidth = (int) (_size.Width + widthInteveral*_tick);
				var newHeight = (int) (_size.Height + heightInterval*_tick);

				_control.Size = new Size(newWidth, newHeight);

				_control.Update();

				_tick++;
			}

			/**
			 * Make sure the GUI thread redraws the resized control.
			 */
			Application.DoEvents();
		}

		/**
		 * User interface to initiate the resizing of a control.
		 */

		public void Resize(int width, int height, int time)
		{
			if (_size.Width != width || _size.Height != height)
			{
				_ticks = time/_timer.Interval;
				_height = height;
				_width = width;

				_tick = 0;

				_timer.Enabled = true;
				_timer.Start();
			}
		}
	}
}