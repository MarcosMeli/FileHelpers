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
	public class frmEasyMulti : frmFather
	{
		private PropertyGrid grid1;
		private Button cmdRun;
		private Label label2;
		private Label label1;
		private Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox txtOut3;
		private System.Windows.Forms.TextBox txtOut1;
		private System.Windows.Forms.TextBox txtOut2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmEasyMulti()
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
			this.txtOut3 = new System.Windows.Forms.TextBox();
			this.grid1 = new System.Windows.Forms.PropertyGrid();
			this.cmdRun = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.txtOut1 = new System.Windows.Forms.TextBox();
			this.txtOut2 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(618, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// txtOut3
			// 
			this.txtOut3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOut3.Location = new System.Drawing.Point(392, 312);
			this.txtOut3.Multiline = true;
			this.txtOut3.Name = "txtOut3";
			this.txtOut3.ReadOnly = true;
			this.txtOut3.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtOut3.Size = new System.Drawing.Size(320, 112);
			this.txtOut3.TabIndex = 1;
			this.txtOut3.Text = "";
			this.txtOut3.WordWrap = false;
			// 
			// grid1
			// 
			this.grid1.CommandsVisibleIfAvailable = true;
			this.grid1.HelpVisible = false;
			this.grid1.LargeButtons = false;
			this.grid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.grid1.Location = new System.Drawing.Point(8, 288);
			this.grid1.Name = "grid1";
			this.grid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.grid1.Size = new System.Drawing.Size(368, 136);
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
			this.cmdRun.ForeColor = System.Drawing.Color.Gainsboro;
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
			this.label2.Location = new System.Drawing.Point(8, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Input Array";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(392, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 8;
			this.label1.Text = "Output Arrays";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(8, 264);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(264, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Input Data to the DelimitedFileEngine";
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
			this.label4.Text = "Code to Write the File";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 72);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(368, 176);
			this.textBox1.TabIndex = 11;
			this.textBox1.Text = @"DelimitedFileEngine engine = 
  new DelimitedFileEngine(typeof(CustomersVertical));
engine.WriteFile(""Out_Vertical.txt"", customers)

engine.Options.Delimiter = "";""
engine.WriteFile(""Out_SemiColon.txt"", customers)

engine.Options.Delimiter = ""\t""
engine.WriteFile(""Out_Tab.txt"", customers)";
			this.textBox1.WordWrap = false;
			// 
			// txtOut1
			// 
			this.txtOut1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOut1.Location = new System.Drawing.Point(392, 72);
			this.txtOut1.Multiline = true;
			this.txtOut1.Name = "txtOut1";
			this.txtOut1.ReadOnly = true;
			this.txtOut1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtOut1.Size = new System.Drawing.Size(320, 112);
			this.txtOut1.TabIndex = 12;
			this.txtOut1.Text = "";
			this.txtOut1.WordWrap = false;
			// 
			// txtOut2
			// 
			this.txtOut2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOut2.Location = new System.Drawing.Point(392, 192);
			this.txtOut2.Multiline = true;
			this.txtOut2.Name = "txtOut2";
			this.txtOut2.ReadOnly = true;
			this.txtOut2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtOut2.Size = new System.Drawing.Size(320, 112);
			this.txtOut2.TabIndex = 13;
			this.txtOut2.Text = "";
			this.txtOut2.WordWrap = false;
			// 
			// frmEasyMulti
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(722, 464);
			this.Controls.Add(this.txtOut2);
			this.Controls.Add(this.txtOut1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.txtOut3);
			this.Controls.Add(this.label3);
			this.MaximizeBox = false;
			this.Name = "frmEasyMulti";
			this.Text = "FileHelpers -  Multiple Formats - Same Record Class";
			this.Load += new System.EventHandler(this.frmEasyMulti_Load);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtOut3, 0);
			this.Controls.SetChildIndex(this.grid1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.txtOut1, 0);
			this.Controls.SetChildIndex(this.txtOut2, 0);
			this.ResumeLayout(false);

		}

		#endregion

		string tempCustomers = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain";
		private void cmdRun_Click(object sender, EventArgs e)
		{
			CustomersVerticalBar[] customers = (CustomersVerticalBar[]) grid1.SelectedObject;
			//
			DelimitedFileEngine engine = new DelimitedFileEngine(typeof (CustomersVerticalBar));
			txtOut1.Text = engine.WriteString(customers);

			engine.Options.Delimiter = ";";
			txtOut2.Text = engine.WriteString(customers);
			
			engine.Options.Delimiter = "\t";
			txtOut3.Text = engine.WriteString(customers);
		
	
		}

		private void frmEasyMulti_Load(object sender, System.EventArgs e)
		{
			CustomersVerticalBar[] customers = (CustomersVerticalBar[]) CommonEngine.ReadString(typeof(CustomersVerticalBar), tempCustomers);
			grid1.SelectedObject = customers;
	
		}

		}
	}
