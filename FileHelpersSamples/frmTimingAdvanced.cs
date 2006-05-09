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
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(328, 0);
			this.pictureBox2.Name = "pictureBox2";
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(368, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Navy;
			this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(104, 408);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(192, 32);
			this.button1.TabIndex = 6;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(280, 56);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(168, 32);
			this.cmdRun.TabIndex = 7;
			this.cmdRun.Text = "Run Test ->";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// lstView
			// 
			this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2,
																					  this.columnHeader3,
																					  this.columnHeader4});
			this.lstView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstView.FullRowSelect = true;
			this.lstView.GridLines = true;
			this.lstView.Location = new System.Drawing.Point(16, 96);
			this.lstView.Name = "lstView";
			this.lstView.Size = new System.Drawing.Size(432, 304);
			this.lstView.TabIndex = 16;
			this.lstView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Records";
			this.columnHeader1.Width = 100;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "FileSize";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 109;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Time Sync";
			this.columnHeader3.Width = 104;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Time Async";
			this.columnHeader4.Width = 114;
			// 
			// frmTimmingAdvanced
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(466, 471);
			this.Controls.Add(this.lstView);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmTimmingAdvanced";
			this.Text = "FileHelpers Library - Time And Stress Tests";
			this.Load += new System.EventHandler(this.frmTimming_Load);
			this.Closed += new System.EventHandler(this.frmTimming_Closed);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.lstView, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private long CreateFile(int records)
		{
			return CreateDelimitedFile("tempFile.tmp", records);
		}

		long CreateDelimitedFile(string fileName, int records)
		{
			StreamWriter fs = new StreamWriter(fileName, false);

			for (int i = 0; i < (records/10); i++)
			{
				fs.Write(mTestData);
			}

			fs.Close();

			FileInfo info = new FileInfo(fileName);
			return info.Length;
		}

		private string mTestData;
		private string mTestData2;

		private void frmTimming_Load(object sender, EventArgs e)
		{
			mTestData = "10248|VINET|5|04071996|01081996|16071996|3|32.38" + Environment.NewLine +
				"10249|TOMSP|6|05071996|16081996|10071996|1|11.61" + Environment.NewLine +
				"10250|HANAR|4|08071996|05081996|12071996|2|65.83" + Environment.NewLine +
				"10251|VICTE|3|08071996|05081996|15071996|1|41.34" + Environment.NewLine +
				"10252|SUPRD|4|09071996|06081996|11071996|2|51.3" + Environment.NewLine +
				"10253|HANAR|3|10071996|24071996|16071996|2|58.17" + Environment.NewLine +
				"10254|CHOPS|5|11071996|08081996|23071996|2|22.98" + Environment.NewLine +
				"10255|RICSU|9|12071996|09081996|15071996|3|148.33" + Environment.NewLine +
				"10256|WELLI|3|15071996|12081996|17071996|2|13.97" + Environment.NewLine +
				"10257|HILAA|4|16071996|13081996|22071996|3|81.91" + Environment.NewLine;

			mTestData2 = "10248|VINET|5|3|32.38" + Environment.NewLine +
				"10249|TOMSP|6|1|11.61" + Environment.NewLine +
				"10250|HANAR|4|2|65.83" + Environment.NewLine +
				"10251|VICTE|3|1|41.34" + Environment.NewLine +
				"10252|SUPRD|4|2|51.3" + Environment.NewLine +
				"10253|HANAR|3|2|58.17" + Environment.NewLine +
				"10254|CHOPS|5|2|22.98" + Environment.NewLine +
				"10255|RICSU|9|3|148.33" + Environment.NewLine +
				"10256|WELLI|3|2|13.97" + Environment.NewLine +
				"10257|HILAA|4|3|81.91" + Environment.NewLine;

		}

		void RunTestFor(int records)
		{
			Application.DoEvents();
			long size = CreateFile(records);
			Application.DoEvents();

			RunTest(records, size);

			if (File.Exists("tempFile.tmp"))
				File.Delete("tempFile.tmp");


		}


		private void cmdRun_Click(object sender, EventArgs e)
		{

			RunTestFor(10);
			lstView.Items.Clear();

			RunTestFor(10);
			RunTestFor(100);
			RunTestFor(500);
			RunTestFor(1000);
			RunTestFor(5000);
			RunTestFor(10000);
			RunTestFor(50000);
			RunTestFor(100000);
			//RunTestFor(500000);
			//RunTestFor(1000000);

		}

		FileHelperAsyncEngine mAsyncEngine = new FileHelperAsyncEngine(typeof (OrdersVerticalBar));
		FileHelperEngine mEngine = new FileHelperEngine(typeof (OrdersVerticalBar));

		private void RunTest(int records, long size)
		{

			ListViewItem item = new ListViewItem(records.ToString());
			//item.SubItems[0].Font = new Font(lstView.Font, FontStyle.Bold);

			item.SubItems.Add((size/1024).ToString() + " Kb");
			lstView.Items.Add(item);
			Application.DoEvents();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			long start1 = DateTime.Now.Ticks;

			mEngine.ReadFile("tempFile.tmp");

			long end1= DateTime.Now.Ticks;

			TimeSpan span1, span2;
			span1 = new TimeSpan(end1 - start1);
			item.SubItems.Add(Math.Round(span1.TotalSeconds, 4).ToString() + " sec");

			Application.DoEvents();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			long start2 = DateTime.Now.Ticks;

			mAsyncEngine.BeginReadFile("tempFile.tmp");

			while (mAsyncEngine.ReadNext() != null)
			{}

			long end2= DateTime.Now.Ticks;

			span2 = new TimeSpan(end2 - start2);

			item.SubItems.Add(Math.Round(span2.TotalSeconds, 4).ToString() + " sec");

			Application.DoEvents();
		}

		private void frmTimming_Closed(object sender, EventArgs e)
		{
			if (File.Exists("tempFile.tmp"))
				File.Delete("tempFile.tmp");
		}


	}
}