using System.Diagnostics;

namespace FileHelpersSamples
{
    /// <summary>
    /// Show the latest version of the software
    /// from details on the internet.
    /// </summary>
    /// <remarks>
    /// The VersionData class has information downloaded
    /// from FileHelpers on the current version.  This
    /// screen displays in in a nice human readable format.
    /// </remarks>
    public class frmLastVersion : FileHelpersSamples.frmFather
    {
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdDownload;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.IContainer components = null;

        public frmLastVersion()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Start the version screen with details downloaded
        /// from teh web earlier.
        /// </summary>
        /// <param name="version">Internet version information</param>
        public frmLastVersion(VersionData version)
            : this()
        {
            mLastVersion = version;
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (frmLastVersion));
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            // 
            // txtHistory
            // 
            this.txtHistory.BackColor = System.Drawing.Color.Ivory;
            this.txtHistory.Font = new System.Drawing.Font("Tahoma",
                9F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.txtHistory.Location = new System.Drawing.Point(123, 192);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ReadOnly = true;
            this.txtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(469, 184);
            this.txtHistory.TabIndex = 4;
            this.txtHistory.Text = "";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.Ivory;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma",
                9F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.txtDescription.Location = new System.Drawing.Point(123, 112);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(469, 72);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Text = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(11, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(16, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "History";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(11, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Version";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma",
                9.75F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(11, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Release Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma",
                12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(119, 64);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 16);
            this.lblVersion.TabIndex = 10;
            this.lblVersion.Text = "1.6.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Tahoma",
                12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(119, 88);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(100, 16);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "12/Jun/07";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdDownload
            // 
            this.cmdDownload.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDownload.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdDownload.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdDownload.Image = ((System.Drawing.Image) (resources.GetObject("cmdDownload.Image")));
            this.cmdDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDownload.Location = new System.Drawing.Point(368, 384);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(224, 40);
            this.cmdDownload.TabIndex = 12;
            this.cmdDownload.Text = "Download Binaries";
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button1.ForeColor = System.Drawing.Color.Silver;
            this.button1.Image = ((System.Drawing.Image) (resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(120, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 40);
            this.button1.TabIndex = 13;
            this.button1.Text = "Download Sources";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmLastVersion
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(610, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdDownload);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtHistory);
            this.Name = "frmLastVersion";
            this.Text = "FileHelpers - New Version Info";
            this.Load += new System.EventHandler(this.frmLastVersion_Load);
            this.Controls.SetChildIndex(this.txtHistory, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.lblDate, 0);
            this.Controls.SetChildIndex(this.cmdDownload, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// Current Version data from the Internet
        /// </summary>
        public VersionData mLastVersion;

        /// <summary>
        /// Open a browser (explorer) at the download location
        /// </summary>
        private void cmdDownload_Click(object sender, System.EventArgs e)
        {
            // TODO:  I think that just running the URL will open the default browser...
            Process.Start("explorer", "\"" + mLastVersion.DownloadUrl + "\"");
        }

        /// <summary>
        /// Open a browser on the download other link
        /// </summary>
        private void button1_Click(object sender, System.EventArgs e)
        {
            Process.Start("explorer", "\"" + mLastVersion.DownloadOthers + "\"");
        }

        /// <summary>
        /// On start up add information on the internet version
        /// </summary>
        private void frmLastVersion_Load(object sender, System.EventArgs e)
        {
            txtDescription.Text = mLastVersion.Description;
            txtHistory.Text = mLastVersion.History;

            lblVersion.Text = mLastVersion.Version;
            lblDate.Text = mLastVersion.ReleaseDate.ToString("dd-MMM-yyyy");
        }
    }
}