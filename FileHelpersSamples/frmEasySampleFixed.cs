using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmEasySample.
	/// </summary>
	public class frmEasySampleFixed : frmFather
	{
		private TextBox txtClass;
		private TextBox txtData;
		private PropertyGrid grid1;
		private Button cmdRun;
		private Label label2;
		private Label label1;
		private Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label4;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmEasySampleFixed()
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(304, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(584, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// txtClass
			// 
			this.txtClass.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtClass.Location = new System.Drawing.Point(8, 136);
			this.txtClass.Multiline = true;
			this.txtClass.Name = "txtClass";
			this.txtClass.ReadOnly = true;
			this.txtClass.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtClass.Size = new System.Drawing.Size(328, 160);
			this.txtClass.TabIndex = 0;
			this.txtClass.Text = @"	[FixedLengthRecord]
	public class CustomersFixed
	{
		[FieldFixedLength(11)]
		public string CustomerID;

		[FieldFixedLength(38)]
		public string CompanyName;

		[FieldFixedLength(22)]
		public string ContactName;

		[FieldFixedLength(38)]
		public string ContactTitle;

		[FieldFixedLength(41)]
		public string Address;

		[FieldFixedLength(18)]
		public string City;

		[FieldFixedLength(10)]
		public string Country;
	}";
			this.txtClass.WordWrap = false;
			// 
			// txtData
			// 
			this.txtData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtData.Location = new System.Drawing.Point(8, 320);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.ReadOnly = true;
			this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtData.Size = new System.Drawing.Size(664, 144);
			this.txtData.TabIndex = 1;
			this.txtData.Text = @"ALFKI      Alfreds Futterkiste                   Maria Anders          Sales Representative                  Obere Str. 57                            Berlin            Germany   
ANATR      Ana Trujillo Emparedados y helados    Ana Trujillo          Owner                                 Avda. de la Constitución 2222            México D.F.       Mexico    
ANTON      Antonio Moreno Taquería               Antonio Moreno        Owner                                 Mataderos  2312                          México D.F.       Mexico    
AROUT      Around the Horn                       Thomas Hardy          Sales Representative                  120 Hanover Sq.                          London            UK        
BERGS      Berglunds snabbköp                    Christina Berglund    Order Administrator                   Berguvsvägen  8                          Luleå             Sweden    
BLAUS      Blauer See Delikatessen               Hanna Moos            Sales Representative                  Forsterstr. 57                           Mannheim          Germany   
BLONP      Blondesddsl père et fils              Frédérique Citeaux    Marketing Manager                     24, place Kléber                         Strasbourg        France    
BOLID      Bólido Comidas preparadas             Martín Sommer         Owner                                 C/ Araquil, 67                           Madrid            Spain     ";
			this.txtData.WordWrap = false;
			// 
			// grid1
			// 
			this.grid1.CommandsVisibleIfAvailable = true;
			this.grid1.HelpVisible = false;
			this.grid1.LargeButtons = false;
			this.grid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.grid1.Location = new System.Drawing.Point(344, 136);
			this.grid1.Name = "grid1";
			this.grid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.grid1.Size = new System.Drawing.Size(320, 160);
			this.grid1.TabIndex = 2;
			this.grid1.Text = "propertyGrid1";
			this.grid1.ToolbarVisible = false;
			this.grid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.grid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
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
			this.label3.Location = new System.Drawing.Point(8, 304);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(264, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Input Data to the FileHelperEngine";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 72);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(656, 40);
			this.textBox1.TabIndex = 13;
			this.textBox1.Text = "FileHelperEngine engine = new FileHelperEngine(typeof (CustomersFixed));\r\n ...  =" +
				" engine.ReadFile(\"infile.txt\")";
			this.textBox1.WordWrap = false;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 16);
			this.label4.TabIndex = 12;
			this.label4.Text = "Code To Read The File";
			// 
			// frmEasySampleFixed
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(680, 496);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.txtData);
			this.Controls.Add(this.txtClass);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmEasySampleFixed";
			this.Text = "FileHelpers - Easy Fixed Length Example";
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtClass, 0);
			this.Controls.SetChildIndex(this.txtData, 0);
			this.Controls.SetChildIndex(this.grid1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private void cmdRun_Click(object sender, EventArgs e)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (CustomersFixed));

			grid1.SelectedObject = engine.ReadString(txtData.Text);;
		}
	}
}