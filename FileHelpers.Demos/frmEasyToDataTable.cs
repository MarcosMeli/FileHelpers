using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
	/// Read a fixed length file as a DataTable and
    /// show it on a grid.
    /// <para/>
    /// This is useful for Crystal reports.
	/// </summary>
	public class frmEasyToDataTable : frmFather
	{
		private TextBox txtData;
		private Button cmdRun;
		private Label label1;
		private Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGrid DataGridDatos;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmEasyToDataTable()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEasyToDataTable));
            this.txtData = new System.Windows.Forms.TextBox();
            this.cmdRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DataGridDatos = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(578, 7);
            // 
            // txtData
            // 
            this.txtData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(8, 328);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtData.Size = new System.Drawing.Size(664, 136);
            this.txtData.TabIndex = 1;
            this.txtData.Text = resources.GetString("txtData.Text");
            this.txtData.WordWrap = false;
            // 
            // cmdRun
            // 
            this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(110)))));
            this.cmdRun.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRun.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdRun.Location = new System.Drawing.Point(336, 8);
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(152, 32);
            this.cmdRun.TabIndex = 0;
            this.cmdRun.Text = "RUN >>";
            this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Output DataTable";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Input Data to the FileHelperEngine";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(8, 75);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(664, 39);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "FileHelpersEngine engine = new FileHelpersEngine(typeof(CustomerFixed));\r\nDataGri" +
                "dDatos.DataSource = engine.ReadFileAsDT(\"infile.txt\");";
            this.textBox1.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Code to Read the File";
            // 
            // DataGridDatos
            // 
            this.DataGridDatos.CaptionVisible = false;
            this.DataGridDatos.DataMember = "";
            this.DataGridDatos.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridDatos.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGridDatos.Location = new System.Drawing.Point(8, 134);
            this.DataGridDatos.Name = "DataGridDatos";
            this.DataGridDatos.ParentRowsVisible = false;
            this.DataGridDatos.ReadOnly = true;
            this.DataGridDatos.Size = new System.Drawing.Size(664, 170);
            this.DataGridDatos.TabIndex = 14;
            // 
            // frmEasyToDataTable
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(680, 496);
            this.Controls.Add(this.DataGridDatos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdRun);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Name = "frmEasyToDataTable";
            this.Text = "FileHelpers - Read as DataTable";
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtData, 0);
            this.Controls.SetChildIndex(this.cmdRun, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DataGridDatos, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        /// <summary>
        /// Read the file as a data table
        /// </summary>
		private void cmdRun_Click(object sender, EventArgs e)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (CustomersFixed));
			DataGridDatos.DataSource = engine.ReadStringAsDT(txtData.Text);;
		}
	}
}