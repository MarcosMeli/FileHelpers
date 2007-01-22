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
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.Button cmdSort;
		private System.Windows.Forms.Button cmdMiltipleDeli;

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
			this.button2 = new System.Windows.Forms.Button();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.cmdSort = new System.Windows.Forms.Button();
			this.cmdMiltipleDeli = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(280, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(508, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// cmdEasy
			// 
			this.cmdEasy.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdEasy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdEasy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdEasy.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdEasy.Location = new System.Drawing.Point(16, 64);
			this.cmdEasy.Name = "cmdEasy";
			this.cmdEasy.Size = new System.Drawing.Size(240, 40);
			this.cmdEasy.TabIndex = 0;
			this.cmdEasy.Text = "Easy Delimited ->";
			this.cmdEasy.Click += new System.EventHandler(this.cmdEasy_Click);
			// 
			// cmdDataLink
			// 
			this.cmdDataLink.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdDataLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdDataLink.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdDataLink.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdDataLink.Location = new System.Drawing.Point(320, 64);
			this.cmdDataLink.Name = "cmdDataLink";
			this.cmdDataLink.Size = new System.Drawing.Size(240, 40);
			this.cmdDataLink.TabIndex = 2;
			this.cmdDataLink.Text = "Access DataLink ->";
			this.cmdDataLink.Click += new System.EventHandler(this.cmdDataLink_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.Gainsboro;
			this.button1.Location = new System.Drawing.Point(184, 320);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(240, 40);
			this.button1.TabIndex = 4;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdEasy2
			// 
			this.cmdEasy2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdEasy2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdEasy2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdEasy2.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdEasy2.Location = new System.Drawing.Point(16, 112);
			this.cmdEasy2.Name = "cmdEasy2";
			this.cmdEasy2.Size = new System.Drawing.Size(240, 40);
			this.cmdEasy2.TabIndex = 1;
			this.cmdEasy2.Text = "Easy Fixed ->";
			this.cmdEasy2.Click += new System.EventHandler(this.cmdEasy2_Click);
			// 
			// cmdLibrary
			// 
			this.cmdLibrary.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdLibrary.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLibrary.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdLibrary.Location = new System.Drawing.Point(320, 208);
			this.cmdLibrary.Name = "cmdLibrary";
			this.cmdLibrary.Size = new System.Drawing.Size(240, 40);
			this.cmdLibrary.TabIndex = 3;
			this.cmdLibrary.Text = "Time Testing ->";
			this.cmdLibrary.Click += new System.EventHandler(this.cmdLibrary_Click);
			// 
			// cmdProgress
			// 
			this.cmdProgress.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdProgress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdProgress.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdProgress.Location = new System.Drawing.Point(320, 112);
			this.cmdProgress.Name = "cmdProgress";
			this.cmdProgress.Size = new System.Drawing.Size(240, 40);
			this.cmdProgress.TabIndex = 5;
			this.cmdProgress.Text = "Progress Notification ->";
			this.cmdProgress.Click += new System.EventHandler(this.cmdProgress_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.ForeColor = System.Drawing.Color.Gainsboro;
			this.button2.Location = new System.Drawing.Point(320, 256);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(240, 40);
			this.button2.TabIndex = 7;
			this.button2.Text = "Multiple time Sampling ->";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// pictureBox5
			// 
			this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
			this.pictureBox5.Location = new System.Drawing.Point(256, 176);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(48, 18);
			this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox5.TabIndex = 8;
			this.pictureBox5.TabStop = false;
			// 
			// cmdSort
			// 
			this.cmdSort.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdSort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdSort.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdSort.Location = new System.Drawing.Point(320, 160);
			this.cmdSort.Name = "cmdSort";
			this.cmdSort.Size = new System.Drawing.Size(240, 40);
			this.cmdSort.TabIndex = 9;
			this.cmdSort.Text = "Sorting ->";
			this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
			// 
			// cmdMiltipleDeli
			// 
			this.cmdMiltipleDeli.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdMiltipleDeli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdMiltipleDeli.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdMiltipleDeli.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdMiltipleDeli.Location = new System.Drawing.Point(16, 160);
			this.cmdMiltipleDeli.Name = "cmdMiltipleDeli";
			this.cmdMiltipleDeli.Size = new System.Drawing.Size(240, 40);
			this.cmdMiltipleDeli.TabIndex = 11;
			this.cmdMiltipleDeli.Text = "Multiple Delimiters ->";
			this.cmdMiltipleDeli.Click += new System.EventHandler(this.cmdMiltipleDeli_Click);
			// 
			// frmSamples
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(610, 392);
			this.Controls.Add(this.cmdMiltipleDeli);
			this.Controls.Add(this.cmdSort);
			this.Controls.Add(this.pictureBox5);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.cmdProgress);
			this.Controls.Add(this.cmdLibrary);
			this.Controls.Add(this.cmdEasy2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdDataLink);
			this.Controls.Add(this.cmdEasy);
			this.ExitOnEsc = false;
			this.MaximizeBox = false;
			this.Name = "frmSamples";
			this.Text = "FileHelpers Library - Samples ";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.cmdEasy, 0);
			this.Controls.SetChildIndex(this.cmdDataLink, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdEasy2, 0);
			this.Controls.SetChildIndex(this.cmdLibrary, 0);
			this.Controls.SetChildIndex(this.cmdProgress, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.cmdSort, 0);
			this.Controls.SetChildIndex(this.cmdMiltipleDeli, 0);
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

		private void button2_Click(object sender, System.EventArgs e)
		{
			frmTimmingAdvanced frm = new frmTimmingAdvanced();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void cmdSort_Click(object sender, System.EventArgs e)
		{
			frmSort frm = new frmSort();
			frm.ShowDialog();
			frm.Dispose();		
		}

		private void cmdMiltipleDeli_Click(object sender, System.EventArgs e)
		{
			frmEasyMulti frm = new frmEasyMulti();
			frm.ShowDialog();
			frm.Dispose();	
		}


	}
}