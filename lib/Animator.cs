using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

/**
 * File Mask
 * 
 * An open source application written in C# to facilitate the
 * merging and extraction of files and/from images.
 * 
 * @author Andrew Lee
 * @version 1.0.0
 */
namespace File_Mask.lib
{
	/**
	 * Used to animate the size of various window controls.
	 */
	class Animator
	{
		private Control control = null;
		private Size size = new Size(-1, -1);
		private int width = -1;
		private int height = -1;
		private Timer timer = null;
		private int ticks = -1;
		private int tick = -1;
		private int time = -1;

		/**
		 * Constructor.
		 */
		public Animator(Control control)
		{
			this.control = control;
			this.size = control.Size;

			bootstrap();
		}

		/**
		 * Sets up various elements of the animator on creation.
		 */
		private void bootstrap()
		{
			this.timer = new Timer();
			this.timer.Interval = 1000 / 60;

			this.timer.Tick += new EventHandler(timerTick);
		}

		/**
		 * Resets the animator.
		 */
		private void reset()
		{
			this.time = this.ticks = this.height = this.width = 0;
			this.timer.Stop();
			this.timer.Enabled = false;
		}

		/**
		 * Executes every time to timer ticks and resizes the specified control.
		 */
		private void timerTick(Object myObject, EventArgs myEventArgs)
		{
			/**
			 * Check to see if this is the last tick.
			 * If it is then set the control to the given desired size.
			 */
			if (this.tick == this.ticks)
			{
				this.control.Size = new Size(width, height);

				reset();
			}
			else
			{
				/**
				 * Calculate the interval at which to resize the control.
				 * Apply the new size to the control.
				 */
				double widthInteveral = (this.width - this.size.Width) / ticks;
				double heightInterval = (this.height - this.size.Height) / ticks;

				int newWidth = (int)(this.size.Width + widthInteveral * tick);
				int newHeight = (int)(this.size.Height + heightInterval * tick);

				this.control.Size = new Size(newWidth, newHeight);

				this.control.Update();

				this.tick++;
			}

			/**
			 * Make sure the GUI thread redraws the resized control.
			 */
			Application.DoEvents();
		}

		/**
		 * User interface to initiate the resizing of a control.
		 */
		public void resize(int width, int height, int time)
		{
			if (this.size.Width != width || this.size.Height != height)
			{
				this.time = time;
				this.ticks = time / timer.Interval;
				this.height = height;
				this.width = width;

				this.tick = 0;

				timer.Enabled = true;
				timer.Start();
			}
		}
	}
}
