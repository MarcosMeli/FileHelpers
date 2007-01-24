using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileHelpers;
using FileHelpers.MasterDetail;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmEasySample.
	/// </summary>
	public class frmMasterDetail : frmFather
	{
		private TextBox txtClass;
		private TextBox txtData;
		private PropertyGrid grid1;
		private Button cmdRun;
		private Label label2;
		private Label label1;
		private Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmMasterDetail()
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
			this.txtData = new System.Windows.Forms.TextBox();
			this.grid1 = new System.Windows.Forms.PropertyGrid();
			this.cmdRun = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(376, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(640, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// txtClass
			// 
			this.txtClass.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClass.Location = new System.Drawing.Point(8, 232);
			this.txtClass.Multiline = true;
			this.txtClass.Name = "txtClass";
			this.txtClass.ReadOnly = true;
			this.txtClass.Size = new System.Drawing.Size(224, 176);
			this.txtClass.TabIndex = 0;
			this.txtClass.Text = "[DelimitedRecord(\"|\")]\r\npublic class Customers\r\n{\r\n   public string CustomerID;\r\n" +
				"   public string CompanyName;\r\n   public string ContactName;\r\n   public string C" +
				"ontactTitle;\r\n   public string Address;\r\n   public string City;\r\n   public strin" +
				"g Country;\r\n}";
			this.txtClass.WordWrap = false;
			// 
			// txtData
			// 
			this.txtData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtData.Location = new System.Drawing.Point(8, 424);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.ReadOnly = true;
			this.txtData.Size = new System.Drawing.Size(728, 104);
			this.txtData.TabIndex = 1;
			this.txtData.Text = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
10248|ALFKI|5|04071996|01081996|16071996|3|32.38
10251|ALFKI|3|08071996|05081996|15071996|1|41.34
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
10252|ANATR|4|09071996|06081996|11071996|2|51.3
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
10260|ANTON|6|05071996|16081996|10071996|1|11.61";
			this.txtData.WordWrap = false;
			// 
			// grid1
			// 
			this.grid1.CommandsVisibleIfAvailable = true;
			this.grid1.HelpVisible = false;
			this.grid1.LargeButtons = false;
			this.grid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.grid1.Location = new System.Drawing.Point(480, 232);
			this.grid1.Name = "grid1";
			this.grid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.grid1.Size = new System.Drawing.Size(264, 176);
			this.grid1.TabIndex = 2;
			this.grid1.Text = "propertyGrid1";
			this.grid1.ToolbarVisible = false;
			this.grid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.grid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(304, 8);
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
			this.label2.Location = new System.Drawing.Point(8, 120);
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
			this.label1.Location = new System.Drawing.Point(344, 120);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Output Array";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(8, 408);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(264, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Input Data to the MasterDetailEngine";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Code to Read the File";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 56);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(728, 152);
			this.textBox1.TabIndex = 11;
			this.textBox1.Text = @"MasterDetailEngine engine = new MasterDetailEngine(typeof(Customers), 
                                typeof(Orders), new MasterDetailSelector(ExampleSelector));

MasterDetails[] res = engine.ReadFile(""TestIn.txt"");

RecordAction ExampleSelector(string record)
{   if (Char.IsLetter(record[0])) 
          return RecordAction.Master;
     else   return RecordAction.Detail;
}";
			this.textBox1.WordWrap = false;
			// 
			// textBox2
			// 
			this.textBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox2.Location = new System.Drawing.Point(240, 232);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(224, 176);
			this.textBox2.TabIndex = 12;
			this.textBox2.Text = @"[DelimitedRecord(""|"")]
public class Orders
{
   public int OrderID;
   public string CustomerID;
   public int EmployeeID;
   public DateTime OrderDate;
   public DateTime RequiredDate;
   public DateTime ShippedDate;
   public int ShipVia;
   public decimal Freight;
}";
			this.textBox2.WordWrap = false;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(8, 216);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(152, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "Master Record Class";
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(240, 216);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(152, 16);
			this.label6.TabIndex = 14;
			this.label6.Text = "Detail Record Class";
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.White;
			this.label7.Location = new System.Drawing.Point(472, 216);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(152, 16);
			this.label7.TabIndex = 15;
			this.label7.Text = "Output MasterDetails[]";
			// 
			// frmMasterDetail
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(744, 558);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.txtData);
			this.Controls.Add(this.txtClass);
			this.Controls.Add(this.label3);
			this.MaximizeBox = false;
			this.Name = "frmMasterDetail";
			this.Text = "FileHelpers - Easy Example";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtClass, 0);
			this.Controls.SetChildIndex(this.txtData, 0);
			this.Controls.SetChildIndex(this.grid1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.textBox2, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void cmdRun_Click(object sender, EventArgs e)
		{
			MasterDetailEngine engine = new MasterDetailEngine(typeof(Customers), 
				typeof(Orders), new MasterDetailSelector(ExampleSelector));

			// to Read use:
			MasterDetails[] res = engine.ReadString(txtData.Text);
			
			grid1.SelectedObject = res;
	
		}
		
		
		RecordAction ExampleSelector(string record)
		{
			if (Char.IsLetter(record[0]))
				return RecordAction.Master;
			else
				return RecordAction.Detail;
		}

	
		[DelimitedRecord("|")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class Customers
		{
			private string mCustomerID;
			private string mCompanyName;
			private string mContactName;
			private string mContactTitle;
			private string mAddress;
			private string mCity;
			private string mCountry;

			public string CustomerID
			{
				get { return mCustomerID; }
				set { mCustomerID = value; }
			}

			public string CompanyName
			{
				get { return mCompanyName; }
				set { mCompanyName = value; }
			}

			public string ContactName
			{
				get { return mContactName; }
				set { mContactName = value; }
			}

			public string ContactTitle
			{
				get { return mContactTitle; }
				set { mContactTitle = value; }
			}

			public string Address
			{
				get { return mAddress; }
				set { mAddress = value; }
			}

			public string City
			{
				get { return mCity; }
				set { mCity = value; }
			}

			public string Country
			{
				get { return mCountry; }
				set { mCountry = value; }
			}
			
  		    public override string ToString()
			{
				return this.CustomerID + " - " + this.ContactTitle;
			}

		}

		
		[DelimitedRecord("|")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class Orders
		{
			private int mOrderID;
			private string mCustomerID;
			private int mEmployeeID;
			private DateTime mOrderDate;
			private DateTime mRequiredDate;
			private DateTime mShippedDate;
			private int mShipVia;
			private decimal mFreight;

			public int OrderID
			{
				get { return mOrderID; }
				set { mOrderID = value; }
			}

			public string CustomerID
			{
				get { return mCustomerID; }
				set { mCustomerID = value; }
			}

			public int EmployeeID
			{
				get { return mEmployeeID; }
				set { mEmployeeID = value; }
			}

			public DateTime OrderDate
			{
				get { return mOrderDate; }
				set { mOrderDate = value; }
			}

			public DateTime RequiredDate
			{
				get { return mRequiredDate; }
				set { mRequiredDate = value; }
			}

			public DateTime ShippedDate
			{
				get { return mShippedDate; }
				set { mShippedDate = value; }
			}

			public int ShipVia
			{
				get { return mShipVia; }
				set { mShipVia = value; }
			}

			public decimal Freight
			{
				get { return mFreight; }
				set { mFreight = value; }
			}

			public override string ToString()
			{
				return this.OrderID.ToString() + " - " + this.CustomerID;
			}

		}

		}
	}
