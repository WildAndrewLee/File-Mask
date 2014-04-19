namespace File_Mask
{
	partial class Frame
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frame));
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.selectedFile = new System.Windows.Forms.TextBox();
			this.selectedImage = new System.Windows.Forms.TextBox();
			this.run = new System.Windows.Forms.Button();
			this.applyMask = new System.Windows.Forms.RadioButton();
			this.removeMask = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.hash = new System.Windows.Forms.TextBox();
			this.browseFile = new System.Windows.Forms.Button();
			this.browseImage = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.key = new System.Windows.Forms.TextBox();
			this.noticeContainer = new System.Windows.Forms.Panel();
			this.value = new System.Windows.Forms.Label();
			this.notice = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.aesContainer = new System.Windows.Forms.Panel();
			this.browseHash = new System.Windows.Forms.Button();
			this.browseKey = new System.Windows.Forms.Button();
			this.noticeContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.aesContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "File:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Image:";
			// 
			// selectedFile
			// 
			this.selectedFile.Location = new System.Drawing.Point(64, 6);
			this.selectedFile.Name = "selectedFile";
			this.selectedFile.Size = new System.Drawing.Size(184, 20);
			this.selectedFile.TabIndex = 2;
			// 
			// selectedImage
			// 
			this.selectedImage.Location = new System.Drawing.Point(64, 32);
			this.selectedImage.Name = "selectedImage";
			this.selectedImage.Size = new System.Drawing.Size(184, 20);
			this.selectedImage.TabIndex = 3;
			// 
			// run
			// 
			this.run.Location = new System.Drawing.Point(12, 26);
			this.run.Name = "run";
			this.run.Size = new System.Drawing.Size(304, 23);
			this.run.TabIndex = 4;
			this.run.Text = "Run File Mask";
			this.run.UseVisualStyleBackColor = true;
			this.run.Click += new System.EventHandler(this.run_Click);
			// 
			// applyMask
			// 
			this.applyMask.AutoSize = true;
			this.applyMask.Location = new System.Drawing.Point(12, 3);
			this.applyMask.Name = "applyMask";
			this.applyMask.Size = new System.Drawing.Size(80, 17);
			this.applyMask.TabIndex = 5;
			this.applyMask.TabStop = true;
			this.applyMask.Text = "Apply Mask";
			this.applyMask.UseVisualStyleBackColor = true;
			this.applyMask.CheckedChanged += new System.EventHandler(this.applyMask_CheckedChanged);
			// 
			// removeMask
			// 
			this.removeMask.AutoSize = true;
			this.removeMask.Location = new System.Drawing.Point(98, 3);
			this.removeMask.Name = "removeMask";
			this.removeMask.Size = new System.Drawing.Size(94, 17);
			this.removeMask.TabIndex = 6;
			this.removeMask.TabStop = true;
			this.removeMask.Text = "Remove Mask";
			this.removeMask.UseVisualStyleBackColor = true;
			this.removeMask.CheckedChanged += new System.EventHandler(this.removeMask_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "AES Hash:";
			// 
			// hash
			// 
			this.hash.Location = new System.Drawing.Point(74, 3);
			this.hash.Name = "hash";
			this.hash.ReadOnly = true;
			this.hash.Size = new System.Drawing.Size(174, 20);
			this.hash.TabIndex = 8;
			// 
			// browseFile
			// 
			this.browseFile.Location = new System.Drawing.Point(255, 4);
			this.browseFile.Name = "browseFile";
			this.browseFile.Size = new System.Drawing.Size(61, 23);
			this.browseFile.TabIndex = 9;
			this.browseFile.Text = "Browse";
			this.browseFile.UseVisualStyleBackColor = true;
			this.browseFile.Click += new System.EventHandler(this.browseFile_Click);
			// 
			// browseImage
			// 
			this.browseImage.Location = new System.Drawing.Point(255, 30);
			this.browseImage.Name = "browseImage";
			this.browseImage.Size = new System.Drawing.Size(61, 23);
			this.browseImage.TabIndex = 10;
			this.browseImage.Text = "Browse";
			this.browseImage.UseVisualStyleBackColor = true;
			this.browseImage.Click += new System.EventHandler(this.browseImage_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "AES Key:";
			// 
			// key
			// 
			this.key.Location = new System.Drawing.Point(74, 29);
			this.key.Name = "key";
			this.key.ReadOnly = true;
			this.key.Size = new System.Drawing.Size(174, 20);
			this.key.TabIndex = 12;
			// 
			// noticeContainer
			// 
			this.noticeContainer.Controls.Add(this.value);
			this.noticeContainer.Controls.Add(this.notice);
			this.noticeContainer.Location = new System.Drawing.Point(-1, 58);
			this.noticeContainer.Name = "noticeContainer";
			this.noticeContainer.Size = new System.Drawing.Size(329, 22);
			this.noticeContainer.TabIndex = 13;
			// 
			// value
			// 
			this.value.AutoSize = true;
			this.value.Location = new System.Drawing.Point(195, 2);
			this.value.Name = "value";
			this.value.Size = new System.Drawing.Size(0, 13);
			this.value.TabIndex = 1;
			// 
			// notice
			// 
			this.notice.AutoSize = true;
			this.notice.Location = new System.Drawing.Point(10, 2);
			this.notice.Name = "notice";
			this.notice.Size = new System.Drawing.Size(187, 13);
			this.notice.TabIndex = 0;
			this.notice.Text = "Suggested Minimum Number of Pixels:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.selectedImage);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.selectedFile);
			this.panel1.Controls.Add(this.browseImage);
			this.panel1.Controls.Add(this.browseFile);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 59);
			this.panel1.TabIndex = 14;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.applyMask);
			this.panel2.Controls.Add(this.removeMask);
			this.panel2.Controls.Add(this.run);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 54);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(328, 52);
			this.panel2.TabIndex = 15;
			// 
			// aesContainer
			// 
			this.aesContainer.Controls.Add(this.browseKey);
			this.aesContainer.Controls.Add(this.browseHash);
			this.aesContainer.Controls.Add(this.key);
			this.aesContainer.Controls.Add(this.label3);
			this.aesContainer.Controls.Add(this.hash);
			this.aesContainer.Controls.Add(this.label4);
			this.aesContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.aesContainer.Location = new System.Drawing.Point(0, 106);
			this.aesContainer.Name = "aesContainer";
			this.aesContainer.Size = new System.Drawing.Size(328, 0);
			this.aesContainer.TabIndex = 16;
			// 
			// browseHash
			// 
			this.browseHash.Location = new System.Drawing.Point(250, 1);
			this.browseHash.Name = "browseHash";
			this.browseHash.Size = new System.Drawing.Size(66, 23);
			this.browseHash.TabIndex = 13;
			this.browseHash.Text = "Browse";
			this.browseHash.UseVisualStyleBackColor = true;
			this.browseHash.Click += new System.EventHandler(this.browseHash_Click);
			// 
			// browseKey
			// 
			this.browseKey.Location = new System.Drawing.Point(250, 27);
			this.browseKey.Name = "browseKey";
			this.browseKey.Size = new System.Drawing.Size(66, 23);
			this.browseKey.TabIndex = 14;
			this.browseKey.Text = "Browse";
			this.browseKey.UseVisualStyleBackColor = true;
			this.browseKey.Click += new System.EventHandler(this.browseKey_Click);
			// 
			// Frame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(328, 106);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.aesContainer);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.noticeContainer);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Frame";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Mask";
			this.noticeContainer.ResumeLayout(false);
			this.noticeContainer.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.aesContainer.ResumeLayout(false);
			this.aesContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox selectedFile;
		private System.Windows.Forms.TextBox selectedImage;
		private System.Windows.Forms.Button run;
		private System.Windows.Forms.RadioButton applyMask;
		private System.Windows.Forms.RadioButton removeMask;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox hash;
		private System.Windows.Forms.Button browseFile;
		private System.Windows.Forms.Button browseImage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox key;
		private System.Windows.Forms.Panel noticeContainer;
		private System.Windows.Forms.Label notice;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel aesContainer;
		private System.Windows.Forms.Label value;
		private System.Windows.Forms.Button browseKey;
		private System.Windows.Forms.Button browseHash;
	}
}

