using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FileHelpers.WizardApp
{
	public class frmDonate : frmFather
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.ListBox lstAmount;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Panel panText;
		private System.Windows.Forms.CheckBox chkPopup;
		private System.Windows.Forms.Button cmdDonate;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.ComponentModel.IContainer components = null;
	    public const string WizardDonateRegKey = "WizardShowDonate_2_1";

	    public frmDonate()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonate));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lstAmount = new System.Windows.Forms.ListBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.panText = new System.Windows.Forms.Panel();
            this.chkPopup = new System.Windows.Forms.CheckBox();
            this.cmdDonate = new System.Windows.Forms.Button();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panText.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(296, 7);
            this.pictureBox3.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 185);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(284, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Thanks !!";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(40, 328);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(72, 29);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // lstAmount
            // 
            this.lstAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAmount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAmount.ItemHeight = 16;
            this.lstAmount.Items.AddRange(new object[] {
            "US      $5",
            "US    $10",
            "US    $20",
            "US    $50",
            "US  $100"});
            this.lstAmount.Location = new System.Drawing.Point(120, 303);
            this.lstAmount.Name = "lstAmount";
            this.lstAmount.Size = new System.Drawing.Size(64, 84);
            this.lstAmount.TabIndex = 8;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(110)))));
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdClose.Location = new System.Drawing.Point(192, 349);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(172, 38);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Maybe next time :P ";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // panText
            // 
            this.panText.Controls.Add(this.label2);
            this.panText.Controls.Add(this.label1);
            this.panText.Location = new System.Drawing.Point(12, 56);
            this.panText.Name = "panText";
            this.panText.Size = new System.Drawing.Size(386, 226);
            this.panText.TabIndex = 10;
            this.panText.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // chkPopup
            // 
            this.chkPopup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPopup.BackColor = System.Drawing.Color.Transparent;
            this.chkPopup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPopup.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkPopup.Location = new System.Drawing.Point(3, 430);
            this.chkPopup.Name = "chkPopup";
            this.chkPopup.Size = new System.Drawing.Size(264, 22);
            this.chkPopup.TabIndex = 11;
            this.chkPopup.Text = "Dont show me this popup anymore";
            this.chkPopup.UseVisualStyleBackColor = false;
            // 
            // cmdDonate
            // 
            this.cmdDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDonate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(110)))));
            this.cmdDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDonate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDonate.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdDonate.Image = ((System.Drawing.Image)(resources.GetObject("cmdDonate.Image")));
            this.cmdDonate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDonate.Location = new System.Drawing.Point(192, 303);
            this.cmdDonate.Name = "cmdDonate";
            this.cmdDonate.Size = new System.Drawing.Size(172, 40);
            this.cmdDonate.TabIndex = 12;
            this.cmdDonate.Text = "Donation Page";
            this.cmdDonate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDonate.UseVisualStyleBackColor = false;
            this.cmdDonate.Click += new System.EventHandler(this.cmdDonate_Click_1);
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.ForeColor = System.Drawing.Color.White;
            this.linkLabel3.LinkColor = System.Drawing.Color.White;
            this.linkLabel3.Location = new System.Drawing.Point(32, 407);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(344, 16);
            this.linkLabel3.TabIndex = 102;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Or Donate a book from the Amazon Wish List";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // frmDonate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(410, 479);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.cmdDonate);
            this.Controls.Add(this.chkPopup);
            this.Controls.Add(this.panText);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lstAmount);
            this.Controls.Add(this.pictureBox4);
            this.Name = "frmDonate";
            this.Text = "Donate =)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDonate_FormClosed);
            this.Load += new System.EventHandler(this.frmDonate_Load);
            this.Controls.SetChildIndex(this.pictureBox3, 0);
            this.Controls.SetChildIndex(this.pictureBox4, 0);
            this.Controls.SetChildIndex(this.lstAmount, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.panText, 0);
            this.Controls.SetChildIndex(this.chkPopup, 0);
            this.Controls.SetChildIndex(this.cmdDonate, 0);
            this.Controls.SetChildIndex(this.linkLabel3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panText.ResumeLayout(false);
            this.panText.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmDonate_Load(object sender, System.EventArgs e)
		{
			lstAmount.SelectedIndex = 2;
            chkPopup.Checked = RegConfig.GetStringValue(WizardDonateRegKey, "1") == "0";
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

				Close();
			}
		
		}

		private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.amazon.com/gp/registry/wishlist/20HRDZWS0NJ6C/104-5286383-8923129?reveal=unpurchased&filter=all&sort=priority&layout=standard&x=10&y=9\"");
			//info.CreateNoWindow = false;
			//info.UseShellExecute = true;
			Process.Start(info);
		
		}

        private void frmDonate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (chkPopup.Checked)
                RegConfig.SetStringValue(WizardDonateRegKey, "0");
            else
                RegConfig.SetStringValue(WizardDonateRegKey, "1");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


	}

    /// <summary>
    /// Store settings for user in the registry under Software\FileHelpers\
    /// </summary>
    public class RegConfig
    {

        private static string mBranch = @"Software\FileHelpers\";

        public static string GetStringValue(string keyName, string def)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            }
            string res;
            res = (string)key.GetValue(keyName, def);
            key.Close();
            return res;
        }

        public static bool HasValue(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            }
            object res = (string)key.GetValue(keyName, null);
            key.Close();

            return res != null;
        }

        public static void SetStringValue(string keyName, string value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, true);
            key.SetValue(keyName, value, RegistryValueKind.String);
            key.Close();
        }


    }

}

