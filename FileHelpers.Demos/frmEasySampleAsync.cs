using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmEasySample.
	/// </summary>
	public class frmEasySampleAsync : frmFather
	{
		private TextBox txtClass;
		private Button cmdRun;
		private Label label2;
		private Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox txtOut;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmEasySampleAsync()
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
			this.txtClass = new System.Windows.Forms.TextBox();
			this.cmdRun = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.txtOut = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(576, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// txtClass
			// 
			this.txtClass.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClass.Location = new System.Drawing.Point(8, 288);
			this.txtClass.Multiline = true;
			this.txtClass.Name = "txtClass";
			this.txtClass.ReadOnly = true;
			this.txtClass.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtClass.Size = new System.Drawing.Size(328, 160);
			this.txtClass.TabIndex = 0;
			this.txtClass.Text = @"	[DelimitedRecord(""|"")]
	public class CustomersVerticalBar
	{
		public string CustomerID;
		public string CompanyName;
		public string ContactName;
		public string ContactTitle;
		public string Address;
		public string City;
		public string Country;
	}";
			this.txtClass.WordWrap = false;
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(336, 8);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(152, 32);
			this.cmdRun.TabIndex = 0;
			this.cmdRun.Text = "RUN >>";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(8, 272);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Code of the Mapping Class";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(344, 272);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Output Array";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Code to Read the File";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 80);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(656, 184);
			this.textBox1.TabIndex = 11;
			this.textBox1.Text = @"  FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(CustomersVerticalBar));

  engine.BeginReadFile(""TestIn.txt"");

  // The Async engines are IEnumerable
  foreach(CustomersVerticalBar cust in engine)
  {
     // your code here
     Console.WriteLine(cust.CustomerID + "" - "" + cust.ContactTitle + Environment.NewLine);
  }

  engine.Close();";
			this.textBox1.WordWrap = false;
			// 
			// txtOut
			// 
			this.txtOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOut.Location = new System.Drawing.Point(344, 288);
			this.txtOut.Multiline = true;
			this.txtOut.Name = "txtOut";
			this.txtOut.ReadOnly = true;
			this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOut.Size = new System.Drawing.Size(328, 160);
			this.txtOut.TabIndex = 12;
			this.txtOut.Text = "";
			this.txtOut.WordWrap = false;
			// 
			// frmEasySampleAsync
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(680, 480);
			this.Controls.Add(this.txtOut);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.txtClass);
			this.Controls.Add(this.textBox1);
			this.Name = "frmEasySampleAsync";
			this.Text = "FileHelpers - Easy Example";
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.txtClass, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.txtOut, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void cmdRun_Click(object sender, EventArgs e)
		{
			txtOut.Text = string.Empty;
			
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(CustomersVerticalBar));

			
			engine.BeginReadString(TestData.mCustomersTest);

			// The Async engines are IEnumerable
			foreach(CustomersVerticalBar cust in engine)
			{
				// your code here
				txtOut.Text += cust.CustomerID + " - " + cust.ContactTitle + Environment.NewLine;
			}

			engine.Close();
	
		}
		
	}
	}
