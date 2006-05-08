using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmSamples.
	/// </summary>
	public class frmSamples : frmFather
	{
		private Button cmdEasy;
		private Button cmdDataLink;
		private Button button1;
		private Button cmdEasy2;
		private Button cmdLibrary;
		private System.Windows.Forms.Button cmdProgress;
		private System.Windows.Forms.PictureBox pictureBox4;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmSamples()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSamples));
			this.cmdEasy = new System.Windows.Forms.Button();
			this.cmdDataLink = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cmdEasy2 = new System.Windows.Forms.Button();
			this.cmdLibrary = new System.Windows.Forms.Button();
			this.cmdProgress = new System.Windows.Forms.Button();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(56, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(288, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// cmdEasy
			// 
			this.cmdEasy.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdEasy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdEasy.ForeColor = System.Drawing.Color.White;
			this.cmdEasy.Location = new System.Drawing.Point(80, 72);
			this.cmdEasy.Name = "cmdEasy";
			this.cmdEasy.Size = new System.Drawing.Size(240, 43);
			this.cmdEasy.TabIndex = 0;
			this.cmdEasy.Text = "Easy Delimited sample ->";
			this.cmdEasy.Click += new System.EventHandler(this.cmdEasy_Click);
			// 
			// cmdDataLink
			// 
			this.cmdDataLink.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdDataLink.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdDataLink.ForeColor = System.Drawing.Color.White;
			this.cmdDataLink.Location = new System.Drawing.Point(80, 184);
			this.cmdDataLink.Name = "cmdDataLink";
			this.cmdDataLink.Size = new System.Drawing.Size(240, 43);
			this.cmdDataLink.TabIndex = 2;
			this.cmdDataLink.Text = "DataLink Example ->";
			this.cmdDataLink.Click += new System.EventHandler(this.cmdDataLink_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Navy;
			this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(80, 362);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(240, 43);
			this.button1.TabIndex = 4;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdEasy2
			// 
			this.cmdEasy2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdEasy2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdEasy2.ForeColor = System.Drawing.Color.White;
			this.cmdEasy2.Location = new System.Drawing.Point(80, 128);
			this.cmdEasy2.Name = "cmdEasy2";
			this.cmdEasy2.Size = new System.Drawing.Size(240, 43);
			this.cmdEasy2.TabIndex = 1;
			this.cmdEasy2.Text = "Easy Fixed sample ->";
			this.cmdEasy2.Click += new System.EventHandler(this.cmdEasy2_Click);
			// 
			// cmdLibrary
			// 
			this.cmdLibrary.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdLibrary.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLibrary.ForeColor = System.Drawing.Color.White;
			this.cmdLibrary.Location = new System.Drawing.Point(80, 240);
			this.cmdLibrary.Name = "cmdLibrary";
			this.cmdLibrary.Size = new System.Drawing.Size(240, 43);
			this.cmdLibrary.TabIndex = 3;
			this.cmdLibrary.Text = "Time And Stress Tests ->";
			this.cmdLibrary.Click += new System.EventHandler(this.cmdLibrary_Click);
			// 
			// cmdProgress
			// 
			this.cmdProgress.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdProgress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdProgress.ForeColor = System.Drawing.Color.White;
			this.cmdProgress.Location = new System.Drawing.Point(80, 296);
			this.cmdProgress.Name = "cmdProgress";
			this.cmdProgress.Size = new System.Drawing.Size(240, 43);
			this.cmdProgress.TabIndex = 5;
			this.cmdProgress.Text = "Progress Notification ->";
			this.cmdProgress.Click += new System.EventHandler(this.cmdProgress_Click);
			// 
			// pictureBox4
			// 
			this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(320, 304);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(62, 24);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox4.TabIndex = 6;
			this.pictureBox4.TabStop = false;
			// 
			// frmSamples
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(386, 448);
			this.Controls.Add(this.cmdProgress);
			this.Controls.Add(this.cmdLibrary);
			this.Controls.Add(this.cmdEasy2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdDataLink);
			this.Controls.Add(this.cmdEasy);
			this.Controls.Add(this.pictureBox4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmSamples";
			this.Text = "FileHelpers Library - Samples ";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.cmdEasy, 0);
			this.Controls.SetChildIndex(this.cmdDataLink, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdEasy2, 0);
			this.Controls.SetChildIndex(this.cmdLibrary, 0);
			this.Controls.SetChildIndex(this.cmdProgress, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void cmdEasy_Click(object sender, EventArgs e)
		{
			frmEasySampleDelimited frm = new frmEasySampleDelimited();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void cmdDataLink_Click(object sender, EventArgs e)
		{
			frmDataLinkSample frm = new frmDataLinkSample();
			frm.ShowDialog();
			frm.Dispose();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmdEasy2_Click(object sender, EventArgs e)
		{
			frmEasySampleFixed frm = new frmEasySampleFixed();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void cmdLibrary_Click(object sender, EventArgs e)
		{
			frmTimming frm = new frmTimming();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void cmdProgress_Click(object sender, System.EventArgs e)
		{
			frmProgressSample frm = new frmProgressSample();
			frm.ShowDialog();
			frm.Dispose();
		}

	}
}