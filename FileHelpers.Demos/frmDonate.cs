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
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Panel panText;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button cmdDonate;
		private System.Windows.Forms.LinkLabel linkLabel3;
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
			this.lstAmount = new System.Windows.Forms.ListBox();
			this.cmdClose = new System.Windows.Forms.Button();
			this.panText = new System.Windows.Forms.Panel();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.cmdDonate = new System.Windows.Forms.Button();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.panText.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(280, 7);
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
			this.label1.Size = new System.Drawing.Size(300, 135);
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
			this.label2.Location = new System.Drawing.Point(152, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(201, 22);
			this.label2.TabIndex = 5;
			this.label2.Text = "Thank you very much !!!!";
			// 
			// pictureBox4
			// 
			this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(32, 248);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(72, 29);
			this.pictureBox4.TabIndex = 6;
			this.pictureBox4.TabStop = false;
			// 
			// lstAmount
			// 
			this.lstAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			this.lstAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lstAmount.ItemHeight = 16;
			this.lstAmount.Items.AddRange(new object[] {
														   "u$s      5",
														   "u$s    10",
														   "u$s    20",
														   "u$s    50",
														   "u$s  100"});
			this.lstAmount.Location = new System.Drawing.Point(112, 248);
			this.lstAmount.Name = "lstAmount";
			this.lstAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lstAmount.Size = new System.Drawing.Size(64, 82);
			this.lstAmount.TabIndex = 8;
			// 
			// cmdClose
			// 
			this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdClose.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdClose.Location = new System.Drawing.Point(184, 296);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(184, 38);
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
			this.panText.Size = new System.Drawing.Size(354, 176);
			this.panText.TabIndex = 10;
			this.panText.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
			// 
			// checkBox1
			// 
			this.checkBox1.BackColor = System.Drawing.Color.Transparent;
			this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.checkBox1.ForeColor = System.Drawing.Color.White;
			this.checkBox1.Location = new System.Drawing.Point(64, 376);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(280, 16);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "Dont show me this popup anymore";
			// 
			// cmdDonate
			// 
			this.cmdDonate.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(110)));
			this.cmdDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdDonate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdDonate.ForeColor = System.Drawing.Color.Gainsboro;
			this.cmdDonate.Image = ((System.Drawing.Image)(resources.GetObject("cmdDonate.Image")));
			this.cmdDonate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdDonate.Location = new System.Drawing.Point(184, 248);
			this.cmdDonate.Name = "cmdDonate";
			this.cmdDonate.Size = new System.Drawing.Size(184, 38);
			this.cmdDonate.TabIndex = 12;
			this.cmdDonate.Text = "Donation Page";
			this.cmdDonate.Click += new System.EventHandler(this.cmdDonate_Click_1);
			// 
			// linkLabel3
			// 
			this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel3.ForeColor = System.Drawing.Color.White;
			this.linkLabel3.LinkColor = System.Drawing.Color.White;
			this.linkLabel3.Location = new System.Drawing.Point(72, 348);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(248, 16);
			this.linkLabel3.TabIndex = 102;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Or check out the Amazon Book Wish List";
			this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			// 
			// frmDonate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(394, 421);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.cmdDonate);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.panText);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.lstAmount);
			this.Controls.Add(this.pictureBox4);
			this.Name = "frmDonate";
			this.Text = "Donate =)";
			this.Load += new System.EventHandler(this.frmDonate_Load);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.pictureBox4, 0);
			this.Controls.SetChildIndex(this.lstAmount, 0);
			this.Controls.SetChildIndex(this.cmdClose, 0);
			this.Controls.SetChildIndex(this.panText, 0);
			this.Controls.SetChildIndex(this.checkBox1, 0);
			this.Controls.SetChildIndex(this.cmdDonate, 0);
			this.Controls.SetChildIndex(this.linkLabel3, 0);
			this.panText.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmDonate_Load(object sender, System.EventArgs e)
		{
			lstAmount.SelectedIndex = 2;
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

		private void cmdDonate_Click_1(object sender, System.EventArgs e)
		{
			lock(this)
			{
				if (cmdDonate.Enabled == false)
					return;
				
				string amount = lstAmount.Text.Substring(lstAmount.Text.LastIndexOf(" ")).Trim();

				ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://sourceforge.net/donate/index.php?group_id=152382&amt="+amount.ToString()+"&type=0\"");
				//info.CreateNoWindow = false;
				//info.UseShellExecute = true;
				Process.Start(info);
				cmdDonate.Enabled = false;
				cmdClose.Focus();
			}
		
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.amazon.com/gp/registry/wishlist/20HRDZWS0NJ6C/104-5286383-8923129?reveal=unpurchased&filter=all&sort=priority&layout=standard&x=10&y=9\"");
			//info.CreateNoWindow = false;
			//info.UseShellExecute = true;
			Process.Start(info);
		
		}

	}
}

