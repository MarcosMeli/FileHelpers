using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmSamples.
	/// </summary>
	public class frmTimmingAdvanced : frmFather
	{
		private Button button1;
		private Button cmdRun;
		private System.Windows.Forms.ListView lstView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private Framework.Controls.XpProgressBar pb;
		private System.Windows.Forms.ColumnHeader columnHeader5;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmTimmingAdvanced()
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
			this.button1 = new System.Windows.Forms.Button();
			this.cmdRun = new System.Windows.Forms.Button();
			this.lstView = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.pb = new Framework.Controls.XpProgressBar();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(361, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.button1.Location = new System.Drawing.Point(136, 416);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(192, 32);
			this.button1.TabIndex = 6;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdRun.Location = new System.Drawing.Point(280, 56);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(168, 32);
			this.cmdRun.TabIndex = 7;
			this.cmdRun.Text = "Run Test ->";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// lstView
			// 
			this.lstView.BackColor = System.Drawing.Color.Ivory;
			this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader5,
																					  this.columnHeader1,
																					  this.columnHeader2,
																					  this.columnHeader3,
																					  this.columnHeader4});
			this.lstView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstView.FullRowSelect = true;
			this.lstView.GridLines = true;
			this.lstView.Location = new System.Drawing.Point(16, 125);
			this.lstView.Name = "lstView";
			this.lstView.Size = new System.Drawing.Size(432, 280);
			this.lstView.TabIndex = 16;
			this.lstView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Test Nº";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Records";
			this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader1.Width = 84;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "FileSize";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 81;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Time Sync";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader3.Width = 94;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Time Async";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader4.Width = 109;
			// 
			// pb
			// 
			this.pb.Color1 = System.Drawing.Color.RoyalBlue;
			this.pb.Color2 = System.Drawing.Color.AliceBlue;
			this.pb.ColorText = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(50)));
			this.pb.ColorTextShadow = System.Drawing.Color.DimGray;
			this.pb.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pb.GradientStyle = Framework.Controls.GradientMode.HorizontalCenter;
			this.pb.Location = new System.Drawing.Point(16, 94);
			this.pb.Name = "pb";
			this.pb.Position = 0;
			this.pb.PositionMax = 100;
			this.pb.PositionMin = 0;
			this.pb.Size = new System.Drawing.Size(430, 24);
			this.pb.StepDistance = ((System.Byte)(1));
			this.pb.StepWidth = ((System.Byte)(3));
			this.pb.WatermarkAlpha = 255;
			this.pb.WatermarkImage = null;
			// 
			// frmTimmingAdvanced
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(466, 480);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.lstView);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.button1);
			this.MaximizeBox = false;
			this.Name = "frmTimmingAdvanced";
			this.Text = "FileHelpers Library - Time And Stress Tests";
			this.Closed += new System.EventHandler(this.frmTimming_Closed);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.lstView, 0);
			this.Controls.SetChildIndex(this.pb, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void RunTestFor(int records)
		{
			Application.DoEvents();
			string mSourceData = TestData.CreateDelimitedString(records);
			Application.DoEvents();

			RunTest(records, mSourceData);

			if (File.Exists("tempFile.tmp"))
				File.Delete("tempFile.tmp");


		}


		private void cmdRun_Click(object sender, EventArgs e)
		{
			cmdRun.Enabled = false;

			pb.PositionMax = 10;
			pb.Position = 0;

			RunTestFor(10);
			lstView.Items.Clear();

			RunTestFor(10);
			AdvanceProgress();
			RunTestFor(100);
			AdvanceProgress();
			RunTestFor(500);
			AdvanceProgress();
			RunTestFor(1000);
			AdvanceProgress();
			RunTestFor(5000);
			AdvanceProgress();
			RunTestFor(10000);
			AdvanceProgress();
			RunTestFor(20000);
			AdvanceProgress();
			RunTestFor(30000);
			AdvanceProgress();
			RunTestFor(50000);
			AdvanceProgress();
			RunTestFor(75000);
			AdvanceProgress();
			RunTestFor(100000);
			AdvanceProgress();
			//RunTestFor(500000);
			//RunTestFor(1000000);

			cmdRun.Enabled = true;
		}

		void AdvanceProgress()
		{
			pb.Position++;
			pb.Text = "Test " + pb.Position.ToString() + " de " + pb.PositionMax.ToString();
			Application.DoEvents();
		}

		FileHelperAsyncEngine mAsyncEngine = new FileHelperAsyncEngine(typeof (OrdersVerticalBar));
		FileHelperEngine mEngine = new FileHelperEngine(typeof (OrdersVerticalBar));

		private void RunTest(int records, string data)
		{

			ListViewItem item = new ListViewItem("Test " + (pb.Position + 1).ToString());
			item.SubItems.Add(records.ToString());
			//item.SubItems[0].Font = new Font(lstView.Font, FontStyle.Bold);

			item.SubItems.Add((data.Length/1024).ToString() + " Kb");
			lstView.Items.Add(item);
			Application.DoEvents();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			long start1 = DateTime.Now.Ticks;

			mEngine.ReadString(data);

			long end1= DateTime.Now.Ticks;

			TimeSpan span1, span2;
			span1 = new TimeSpan(end1 - start1);
			item.SubItems.Add(string.Format("{0:F4}", span1.TotalSeconds) + " sec");

			Application.DoEvents();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			long start2 = DateTime.Now.Ticks;

			mAsyncEngine.BeginReadString(data);

			while (mAsyncEngine.ReadNext() != null)
			{}

			long end2= DateTime.Now.Ticks;

			span2 = new TimeSpan(end2 - start2);

			item.SubItems.Add(string.Format("{0:F4}", span2.TotalSeconds) + " sec");

			Application.DoEvents();
		}

		private void frmTimming_Closed(object sender, EventArgs e)
		{
			if (File.Exists("tempFile.tmp"))
				File.Delete("tempFile.tmp");
		}


	}
}