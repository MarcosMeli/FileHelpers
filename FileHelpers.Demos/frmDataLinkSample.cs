using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using FileHelpers.DataLink;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmEasySample.
	/// </summary>
	public class frmDataLinkSample : frmFather
	{
		private Button cmdRun;
		private Label label2;
		private RichTextBox richTextBox2;
		private Label lblStatus;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmDataLinkSample()
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
			this.cmdRun = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(400, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(664, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdRun.Location = new System.Drawing.Point(312, 8);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(152, 32);
			this.cmdRun.TabIndex = 3;
			this.cmdRun.Text = "RUN >>";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(8, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Code Used by the Run Button";
			// 
			// richTextBox2
			// 
			this.richTextBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox2.Location = new System.Drawing.Point(8, 72);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ReadOnly = true;
			this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox2.ShowSelectionMargin = true;
			this.richTextBox2.Size = new System.Drawing.Size(752, 408);
			this.richTextBox2.TabIndex = 7;
			this.richTextBox2.Text = @"private void Run()
{
	AccessStorage storage = new AccessStorage(typeof(CustomersVerticalBar), MainClass.GlobalTestMdb);

	storage.SelectSql = ""SELECT * FROM Customers"";
				
	storage.FillRecordCallback = new FillRecordHandler(FillRecord);
	storage.GetInsertSqlCallback = new GetInsertSqlHandler(GetInsertSql);
}

private void FillRecord(object rec, object[] fields)
{
	CustomersVerticalBar record = (CustomersVerticalBar) rec;

	record.CustomerID = (string) fields[0];
	record.CompanyName = (string) fields[1];
	record.ContactName = (string) fields[2];
	record.ContactTitle = (string) fields[3];
	record.Address = (string) fields[4];
	record.City = (string) fields[5];
	record.Country = (string) fields[6];
}

private string GetInsertSql(object record)
{
	CustomersVerticalBar obj = (CustomersVerticalBar) record;

	return String.Format("" INSERT INTO Customers (Address, City, CompanyName, ContactName, ContactTitle, Country, CustomerID)  VALUES ( ""{0}"" , ""{1}"" , ""{2}"" , ""{3}"" , ""{4}"" , ""{5}"" , ""{6}""  ); "", 
			obj.Address,
			obj.City,
			obj.CompanyName,
			obj.ContactName,
			obj.ContactTitle,
			obj.Country,
			obj.CustomerID
		);

}
";
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(232, 54);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(568, 16);
			this.lblStatus.TabIndex = 8;
			// 
			// frmDataLinkSample
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(770, 512);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.richTextBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.MaximizeBox = false;
			this.Name = "frmDataLinkSample";
			this.Text = "Example of the DataLink Feature";
			this.Load += new System.EventHandler(this.frmDataLinkSample_Load);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.richTextBox2, 0);
			this.Controls.SetChildIndex(this.lblStatus, 0);
			this.ResumeLayout(false);

		}

		#endregion

		
		private void FillRecord(object rec, object[] fields)
		{
			CustomersVerticalBar record = (CustomersVerticalBar) rec;

			record.CustomerID = (string) fields[0];
			record.CompanyName = (string) fields[1];
			record.ContactName = (string) fields[2];
			record.ContactTitle = (string) fields[3];
			record.Address = (string) fields[4];
			record.City = (string) fields[5];
			record.Country = (string) fields[6];
		}
		
		private string GetInsertSql(object record)
		{
			CustomersVerticalBar obj = (CustomersVerticalBar) record;

			return String.Format("INSERT INTO Customers (Address, City, CompanyName, ContactName, ContactTitle, Country, CustomerID) " +
				" VALUES ( \"{0}\" , \"{1}\" , \"{2}\" , \"{3}\" , \"{4}\" , \"{5}\" , \"{6}\"  ); ",
				obj.Address,
				obj.City,
				obj.CompanyName,
				obj.ContactName,
				obj.ContactTitle,
				obj.Country,
				obj.CustomerID
				);

		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			try
			{
				cmdRun.Enabled = false;

				lblStatus.Text = "Creating the DataLinkEngine";

				AccessStorage storage = new AccessStorage(typeof(CustomersVerticalBar), MainClass.GlobalTestMdb);

				storage.SelectSql = "SELECT * FROM Customers";
				
				storage.FillRecordCallback = new FillRecordHandler(FillRecord);
				storage.InsertSqlCallback = new InsertSqlHandler(GetInsertSql);

				FileDataLink mLink = new FileDataLink(storage);

				Application.DoEvents();
				Thread.Sleep(800);

				// Db -> File
				lblStatus.Text = "Extracting records from the access db to a file";
				mLink.ExtractToFile(@"temp.txt");
				Application.DoEvents();
				Thread.Sleep(800);

				// File -> Db
				lblStatus.Text = "Inserting records from a file to the access db";
				mLink.InsertFromFile(@"temp.txt");

				if (File.Exists(@"temp.txt"))
					File.Delete(@"temp.txt");

				Application.DoEvents();
				Thread.Sleep(800);


				lblStatus.Text = "DONE !!!";

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Unexpected Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				cmdRun.Enabled = true;
			}

		}

		private void frmDataLinkSample_Load(object sender, EventArgs e)
		{
			//richTextBox2.Rtf = richTextBox2.Text;
		}

	}
}