using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpers.WizardApp
{
    /// <summary>
    /// This is the underlying form for all presentations.
    /// It handles the various links to FileHelpers website
    /// and gives a consistent look across the application.
    /// </summary>
    public class frmFather : Form
    {
        private PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Panel panel1;
        private LinkLabel linkLabel1;
        private LinkLabel lblCopy;
        private PictureBox pictureBox3;
        private System.Windows.Forms.LinkLabel linkLabel3;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public frmFather()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            lblCopy.Text = "(c) 2005-" + (DateTime.Today.Year - 2000) + " to Marcos Meli";
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null)
                    components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFather));
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.lblCopy = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel3);
            this.panel1.Controls.Add(this.lblCopy);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 442);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 24);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel3.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this.linkLabel3.Location = new System.Drawing.Point(319, 4);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(80, 16);
            this.linkLabel3.TabIndex = 102;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Devoo Soft";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // lblCopy
            // 
            this.lblCopy.BackColor = System.Drawing.Color.Transparent;
            this.lblCopy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblCopy.LinkColor = System.Drawing.Color.Black;
            this.lblCopy.Location = new System.Drawing.Point(2, 6);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(158, 16);
            this.lblCopy.TabIndex = 100;
            this.lblCopy.TabStop = true;
            this.lblCopy.Text = "(c) 2005-10 to Marcos Meli";
            this.lblCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(566, 6);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 18);
            this.linkLabel1.TabIndex = 101;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.filehelpers.net";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::FileHelpers.WizardApp.Properties.Resources.donate1;
            this.pictureBox3.Location = new System.Drawing.Point(339, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(85, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(333, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(288, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1231, 51);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // frmFather
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(694, 466);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFather";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileHelpers - Demos";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Color mColor1 = Color.FromArgb(30, 110, 175);
        private Color mColor2 = Color.FromArgb(20, 50, 130);

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush b = new LinearGradientBrush(this.ClientRectangle,
//			        Color.FromArgb(120, 180, 250), Color.FromArgb(10, 35, 100), LinearGradientMode.ForwardDiagonal);
                Color1,
                Color2,
                LinearGradientMode.BackwardDiagonal);

            e.Graphics.FillRectangle(b, e.ClipRectangle);
            b.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush b = new LinearGradientBrush(panel1.ClientRectangle,
                SystemColors.Control,
                Color.DarkGray,
                LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(b, e.ClipRectangle);
            e.Graphics.DrawLine(Pens.DimGray, 0, 0, panel1.Width, 0);
            b.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.filehelpers.net\"");
            info.CreateNoWindow = false;
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("explorer", "\"https://github.com/MarcosMeli/FileHelpers/issues/new\"");

            info.CreateNoWindow = true;
            info.UseShellExecute = true;
            Process.Start(info);
        }


        private DateTime mLastOpen = DateTime.Today.AddDays(-1);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (DateTime.Now > mLastOpen.AddSeconds(10)) {
                Process.Start("explorer", "\"http://www.filehelpers.net\"");
                mLastOpen = DateTime.Now;
            }
        }

        private void pictureBox3_Click(object sender, System.EventArgs e)
        {
            Process.Start("explorer",
                "\"http://www.filehelpers.net/donate/\"");
        }

        private bool mExitOnEsc = true;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (mExitOnEsc && keyData == Keys.Escape) {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void linkLabel3_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.devoo.net\"");
            Process.Start(info);
        }

        public bool ExitOnEsc
        {
            get { return mExitOnEsc; }
            set { mExitOnEsc = value; }
        }

        public Color Color1
        {
            get { return mColor1; }
            set
            {
                mColor1 = value;
                this.Invalidate();
            }
        }

        public Color Color2
        {
            get { return mColor2; }
            set
            {
                mColor2 = value;
                this.Invalidate();
            }
        }
    }
}