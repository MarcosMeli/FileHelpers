using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FileHelpersSamples
{
	public class frmDonate : FileHelpersSamples.frmFather
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.ListBox lstAmount;
		private System.Windows.Forms.Button cmdDonate;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Panel panText;
		private System.ComponentModel.IContainer components = null;

		public frmDonate()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmDonate));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.cmdDonate = new System.Windows.Forms.Button();
			this.lstAmount = new System.Windows.Forms.ListBox();
			this.cmdClose = new System.Windows.Forms.Button();
			this.panText = new System.Windows.Forms.Panel();
			this.panText.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(34, 51);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(298, 7);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Visible = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(40)), ((System.Byte)(40)), ((System.Byte)(40)));
			this.label1.Location = new System.Drawing.Point(20, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(354, 107);
			this.label1.TabIndex = 4;
			this.label1.Text = @"The library becomes day by day harder to mantain and is using a lot of my time, so is very important that if you found the library useful you can donate an small amount of money to let me to do less consultant work and use more time to add features and enhace the existant ones";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
			this.label2.Location = new System.Drawing.Point(189, 121);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(201, 22);
			this.label2.TabIndex = 5;
			this.label2.Text = "Thank you very much !!!!";
			// 
			// pictureBox4
			// 
			this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(328, 10);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(72, 29);
			this.pictureBox4.TabIndex = 6;
			this.pictureBox4.TabStop = false;
			// 
			// cmdDonate
			// 
			this.cmdDonate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdDonate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdDonate.ForeColor = System.Drawing.Color.White;
			this.cmdDonate.Location = new System.Drawing.Point(184, 240);
			this.cmdDonate.Name = "cmdDonate";
			this.cmdDonate.Size = new System.Drawing.Size(136, 32);
			this.cmdDonate.TabIndex = 7;
			this.cmdDonate.Text = "Donate >>";
			this.cmdDonate.Click += new System.EventHandler(this.cmdDonate_Click);
			// 
			// lstAmount
			// 
			this.lstAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			this.lstAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstAmount.ItemHeight = 16;
			this.lstAmount.Items.AddRange(new object[] {
														   "$  5",
														   "$ 10",
														   "$ 20",
														   "$ 50",
														   "$ 100"});
			this.lstAmount.Location = new System.Drawing.Point(104, 216);
			this.lstAmount.Name = "lstAmount";
			this.lstAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lstAmount.Size = new System.Drawing.Size(64, 84);
			this.lstAmount.TabIndex = 8;
			// 
			// cmdClose
			// 
			this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdClose.ForeColor = System.Drawing.Color.Silver;
			this.cmdClose.Location = new System.Drawing.Point(259, 311);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(144, 29);
			this.cmdClose.TabIndex = 9;
			this.cmdClose.Text = "Maybe next time :P ";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// panText
			// 
			this.panText.Controls.Add(this.label1);
			this.panText.Controls.Add(this.label2);
			this.panText.Location = new System.Drawing.Point(14, 56);
			this.panText.Name = "panText";
			this.panText.Size = new System.Drawing.Size(392, 144);
			this.panText.TabIndex = 10;
			this.panText.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			// 
			// frmDonate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(418, 368);
			this.Controls.Add(this.panText);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lstAmount);
			this.Controls.Add(this.cmdDonate);
			this.Controls.Add(this.pictureBox4);
			this.MaximizeBox = false;
			this.Name = "frmDonate";
			this.Text = "Donate =)";
			this.Load += new System.EventHandler(this.frmDonate_Load);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.cmdDonate, 0);
			this.Controls.SetChildIndex(this.lstAmount, 0);
			this.Controls.SetChildIndex(this.cmdClose, 0);
			this.Controls.SetChildIndex(this.panText, 0);
			this.panText.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmDonate_Load(object sender, System.EventArgs e)
		{
			lstAmount.SelectedIndex = 2;
		}

		private void cmdDonate_Click(object sender, System.EventArgs e)
		{
			string amount = lstAmount.Text.Substring(lstAmount.Text.LastIndexOf(" ")).Trim();
			ProcessStartInfo info = new ProcessStartInfo("\"http://sourceforge.net/donate/index.php?group_id=152382&amt="+amount.ToString()+"&type=0\"");
			info.CreateNoWindow = false;
			info.UseShellExecute = true;
			Process.Start(info);

		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			LinearGradientBrush b = new LinearGradientBrush(this.ClientRectangle,
				Color.FromArgb(150, 200, 255),
				Color.FromArgb(40, 65, 150),
				LinearGradientMode.ForwardDiagonal);
			e.Graphics.FillRectangle(b, e.ClipRectangle);
			b.Dispose();

		}


		private void panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			LinearGradientBrush b = new LinearGradientBrush(panText.ClientRectangle,
				Color.LightGray,
				Color.AliceBlue,
				LinearGradientMode.ForwardDiagonal);
			e.Graphics.FillRectangle(b, e.ClipRectangle);
			e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, panText.Width - 1, panText.Height - 1);
			b.Dispose();
		
		}
		
       protected override bool ProcessDialogKey(Keys keyData)
       {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

	}
}

