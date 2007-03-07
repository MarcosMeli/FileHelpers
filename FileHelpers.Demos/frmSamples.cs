using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using FileHelpers;

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
		private System.Windows.Forms.Button cmdSort;
		private System.Windows.Forms.Button cmdAsync;
		private System.Windows.Forms.Button cmdMasterDetail;
		private System.Windows.Forms.PictureBox pictureBox7;
		private System.Windows.Forms.PictureBox picCurrent;
		private System.Windows.Forms.PictureBox picNewVersion;
		private System.Windows.Forms.Button cmdMultipleDeli;
		private System.Windows.Forms.Button cmdMultiTimming;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.PictureBox pictureBox6;
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
			this.cmdMultiTimming = new System.Windows.Forms.Button();
			this.cmdSort = new System.Windows.Forms.Button();
			this.cmdMultipleDeli = new System.Windows.Forms.Button();
			this.cmdAsync = new System.Windows.Forms.Button();
			this.cmdMasterDetail = new System.Windows.Forms.Button();
			this.pictureBox7 = new System.Windows.Forms.PictureBox();
			this.picCurrent = new System.Windows.Forms.PictureBox();
			this.picNewVersion = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox6 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
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
			this.cmdEasy.Image = ((System.Drawing.Image)(resources.GetObject("cmdEasy.Image")));
			this.cmdEasy.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdEasy.Location = new System.Drawing.Point(16, 64);
			this.cmdEasy.Name = "cmdEasy";
			this.cmdEasy.Size = new System.Drawing.Size(240, 40);
			this.cmdEasy.TabIndex = 0;
			this.cmdEasy.Text = "Easy Delimited";
			this.cmdEasy.Click += new System.EventHandler(this.cmdEasy_Click);
			// 
			// cmdDataLink
			// 
			this.cmdDataLink.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdDataLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdDataLink.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdDataLink.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdDataLink.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdDataLink.Location = new System.Drawing.Point(320, 64);
			this.cmdDataLink.Name = "cmdDataLink";
			this.cmdDataLink.Size = new System.Drawing.Size(240, 40);
			this.cmdDataLink.TabIndex = 2;
			this.cmdDataLink.Text = "Access DataLink";
			this.cmdDataLink.Click += new System.EventHandler(this.cmdDataLink_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.Gainsboro;
			this.button1.Location = new System.Drawing.Point(176, 312);
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
			this.cmdEasy2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdEasy2.Location = new System.Drawing.Point(16, 112);
			this.cmdEasy2.Name = "cmdEasy2";
			this.cmdEasy2.Size = new System.Drawing.Size(240, 40);
			this.cmdEasy2.TabIndex = 1;
			this.cmdEasy2.Text = "Easy Fixed";
			this.cmdEasy2.Click += new System.EventHandler(this.cmdEasy2_Click);
			// 
			// cmdLibrary
			// 
			this.cmdLibrary.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdLibrary.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdLibrary.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdLibrary.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdLibrary.Location = new System.Drawing.Point(320, 208);
			this.cmdLibrary.Name = "cmdLibrary";
			this.cmdLibrary.Size = new System.Drawing.Size(240, 40);
			this.cmdLibrary.TabIndex = 3;
			this.cmdLibrary.Text = "Time Testing";
			this.cmdLibrary.Click += new System.EventHandler(this.cmdLibrary_Click);
			// 
			// cmdProgress
			// 
			this.cmdProgress.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdProgress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdProgress.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdProgress.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdProgress.Location = new System.Drawing.Point(320, 112);
			this.cmdProgress.Name = "cmdProgress";
			this.cmdProgress.Size = new System.Drawing.Size(240, 40);
			this.cmdProgress.TabIndex = 5;
			this.cmdProgress.Text = "Progress Notification";
			this.cmdProgress.Click += new System.EventHandler(this.cmdProgress_Click);
			// 
			// cmdMultiTimming
			// 
			this.cmdMultiTimming.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdMultiTimming.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdMultiTimming.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdMultiTimming.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdMultiTimming.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdMultiTimming.Location = new System.Drawing.Point(320, 256);
			this.cmdMultiTimming.Name = "cmdMultiTimming";
			this.cmdMultiTimming.Size = new System.Drawing.Size(240, 40);
			this.cmdMultiTimming.TabIndex = 7;
			this.cmdMultiTimming.Text = "Multiple time Sampling";
			this.cmdMultiTimming.Click += new System.EventHandler(this.button2_Click);
			// 
			// cmdSort
			// 
			this.cmdSort.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdSort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdSort.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdSort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdSort.Location = new System.Drawing.Point(320, 160);
			this.cmdSort.Name = "cmdSort";
			this.cmdSort.Size = new System.Drawing.Size(240, 40);
			this.cmdSort.TabIndex = 9;
			this.cmdSort.Text = "Sorting";
			this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
			// 
			// cmdMultipleDeli
			// 
			this.cmdMultipleDeli.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdMultipleDeli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdMultipleDeli.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdMultipleDeli.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdMultipleDeli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdMultipleDeli.Location = new System.Drawing.Point(16, 208);
			this.cmdMultipleDeli.Name = "cmdMultipleDeli";
			this.cmdMultipleDeli.Size = new System.Drawing.Size(240, 40);
			this.cmdMultipleDeli.TabIndex = 11;
			this.cmdMultipleDeli.Text = "Changing Delimiters";
			this.cmdMultipleDeli.Click += new System.EventHandler(this.cmdMiltipleDeli_Click);
			// 
			// cmdAsync
			// 
			this.cmdAsync.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdAsync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdAsync.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdAsync.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdAsync.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdAsync.Location = new System.Drawing.Point(16, 160);
			this.cmdAsync.Name = "cmdAsync";
			this.cmdAsync.Size = new System.Drawing.Size(240, 40);
			this.cmdAsync.TabIndex = 12;
			this.cmdAsync.Text = "Record by Record";
			this.cmdAsync.Click += new System.EventHandler(this.cmdAsync_Click);
			// 
			// cmdMasterDetail
			// 
			this.cmdMasterDetail.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdMasterDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdMasterDetail.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdMasterDetail.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdMasterDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdMasterDetail.Location = new System.Drawing.Point(16, 256);
			this.cmdMasterDetail.Name = "cmdMasterDetail";
			this.cmdMasterDetail.Size = new System.Drawing.Size(240, 40);
			this.cmdMasterDetail.TabIndex = 15;
			this.cmdMasterDetail.Text = "Master Detail";
			this.cmdMasterDetail.Click += new System.EventHandler(this.cmdMasterDetail_Click);
			// 
			// pictureBox7
			// 
			this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
			this.pictureBox7.Location = new System.Drawing.Point(256, 264);
			this.pictureBox7.Name = "pictureBox7";
			this.pictureBox7.Size = new System.Drawing.Size(55, 30);
			this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox7.TabIndex = 16;
			this.pictureBox7.TabStop = false;
			// 
			// picCurrent
			// 
			this.picCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.picCurrent.BackColor = System.Drawing.Color.Transparent;
			this.picCurrent.Image = ((System.Drawing.Image)(resources.GetObject("picCurrent.Image")));
			this.picCurrent.Location = new System.Drawing.Point(465, 315);
			this.picCurrent.Name = "picCurrent";
			this.picCurrent.Size = new System.Drawing.Size(146, 53);
			this.picCurrent.TabIndex = 17;
			this.picCurrent.TabStop = false;
			this.picCurrent.Visible = false;
			// 
			// picNewVersion
			// 
			this.picNewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.picNewVersion.BackColor = System.Drawing.Color.Transparent;
			this.picNewVersion.Image = ((System.Drawing.Image)(resources.GetObject("picNewVersion.Image")));
			this.picNewVersion.Location = new System.Drawing.Point(465, 315);
			this.picNewVersion.Name = "picNewVersion";
			this.picNewVersion.Size = new System.Drawing.Size(146, 53);
			this.picNewVersion.TabIndex = 18;
			this.picNewVersion.TabStop = false;
			this.picNewVersion.Visible = false;
			// 
			// pictureBox5
			// 
			this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
			this.pictureBox5.Location = new System.Drawing.Point(256, 168);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(55, 30);
			this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox5.TabIndex = 19;
			this.pictureBox5.TabStop = false;
			// 
			// pictureBox6
			// 
			this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
			this.pictureBox6.Location = new System.Drawing.Point(256, 216);
			this.pictureBox6.Name = "pictureBox6";
			this.pictureBox6.Size = new System.Drawing.Size(55, 30);
			this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox6.TabIndex = 20;
			this.pictureBox6.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(560, 264);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(55, 30);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox4.TabIndex = 21;
			this.pictureBox4.TabStop = false;
			// 
			// frmSamples
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(610, 392);
			this.Controls.Add(this.pictureBox6);
			this.Controls.Add(this.pictureBox5);
			this.Controls.Add(this.cmdMasterDetail);
			this.Controls.Add(this.cmdAsync);
			this.Controls.Add(this.cmdMultipleDeli);
			this.Controls.Add(this.cmdSort);
			this.Controls.Add(this.cmdMultiTimming);
			this.Controls.Add(this.cmdProgress);
			this.Controls.Add(this.cmdLibrary);
			this.Controls.Add(this.cmdEasy2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdDataLink);
			this.Controls.Add(this.cmdEasy);
			this.Controls.Add(this.picCurrent);
			this.Controls.Add(this.picNewVersion);
			this.Controls.Add(this.pictureBox7);
			this.Controls.Add(this.pictureBox4);
			this.ExitOnEsc = false;
			this.MaximizeBox = false;
			this.Name = "frmSamples";
			this.Text = "FileHelpers Library - Samples ";
			this.Load += new System.EventHandler(this.frmSamples_Load);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.pictureBox7, 0);
			this.Controls.SetChildIndex(this.picNewVersion, 0);
			this.Controls.SetChildIndex(this.picCurrent, 0);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.cmdEasy, 0);
			this.Controls.SetChildIndex(this.cmdDataLink, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdEasy2, 0);
			this.Controls.SetChildIndex(this.cmdLibrary, 0);
			this.Controls.SetChildIndex(this.cmdProgress, 0);
			this.Controls.SetChildIndex(this.cmdMultiTimming, 0);
			this.Controls.SetChildIndex(this.cmdSort, 0);
			this.Controls.SetChildIndex(this.cmdMultipleDeli, 0);
			this.Controls.SetChildIndex(this.cmdAsync, 0);
			this.Controls.SetChildIndex(this.cmdMasterDetail, 0);
			this.Controls.SetChildIndex(this.pictureBox5, 0);
			this.Controls.SetChildIndex(this.pictureBox6, 0);
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

		private void cmdAsync_Click(object sender, System.EventArgs e)
		{
			frmEasySampleAsync frm = new frmEasySampleAsync();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void cmdMasterDetail_Click(object sender, System.EventArgs e)
		{
			frmMasterDetail frm = new frmMasterDetail();
			frm.ShowDialog();
			frm.Dispose();
		}

	
		private void frmSamples_Load(object sender, System.EventArgs e)
		{

			
			cmdEasy2.Image = cmdEasy.Image;
			cmdAsync.Image = cmdEasy.Image;
			cmdMultipleDeli.Image = cmdEasy.Image;
			cmdMasterDetail.Image = cmdEasy.Image;
			cmdProgress.Image = cmdEasy.Image;
			cmdDataLink.Image = cmdEasy.Image;
			cmdSort.Image = cmdEasy.Image;
			cmdLibrary.Image = cmdEasy.Image;
			cmdMultiTimming.Image = cmdEasy.Image;

			try
			{
				string ver = typeof (FileHelperEngine).Assembly.GetName().Version.ToString(3);

				string dataString;
				using (WebClient webClient = new WebClient())
				{
					byte[] data = webClient.DownloadData("http://filehelpers.sourceforge.net/version.txt");
				
					dataString = System.Text.Encoding.Default.GetString(data);
				}
			
				VersionData[] versions = null;            
				FileHelperEngine engine = new FileHelperEngine(typeof(VersionData));
				versions = (VersionData[]) engine.ReadString(dataString);

				foreach (VersionData version in versions)
				{
					Console.WriteLine(version.Description);
				}
		
				string verLast = versions[versions.Length - 1].Version;
				if (CompararVersiones(ver, verLast) == 0)
					picCurrent.Visible = true;
				else
					picNewVersion.Visible = true;
			}			
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		[DelimitedRecord("|")]
		public class VersionData
		{
			public string Version;
			
			[FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
			public DateTime ReleaseDate;

			public string DownloadUrl;

			[FieldInNewLine]
			[FieldQuoted(MultilineMode.AllowForBoth)]
			public string Description;
			
			[FieldQuoted(MultilineMode.AllowForBoth)]
			public string History;


		}

		public static int CompararVersiones(string ver1, string ver2)
		{
			string[] ver1A = ver1.Split('.');
			string[] ver2A = ver2.Split('.');
			if (ver1A.Length > 0 & ver2A.Length > 0)
			{
				if (int.Parse(ver1A[0]) != int.Parse(ver2A[0]))
				{
					return int.Parse(ver1A[0]) - int.Parse(ver2A[0]);
				}
			}
			if (ver1A.Length > 1 & ver2A.Length > 1)
			{
				if (int.Parse(ver1A[1]) != int.Parse(ver2A[1]))
				{
					return int.Parse(ver1A[1]) - int.Parse(ver2A[1]);
				}
			}
			if (ver1A.Length > 2 & ver2A.Length > 2)
			{
				if (int.Parse(ver1A[2]) != int.Parse(ver2A[2]))
				{
					return int.Parse(ver1A[2]) - int.Parse(ver2A[2]);
				}
			}
			return 0;
		}
	}
}