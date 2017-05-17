using System;
using System.Diagnostics;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Menu of available sample forms.
    /// Allows a new user to walk through the facilities of
    /// the engine.
    /// </summary>
    /// <remarks>
    /// This sample code will test whether the sample is the
    /// latest available version.
    /// </remarks>
    public class frmSamples : frmFather
    {
        private Button cmdEasy;
        private Button cmdDataLink;
        private Button button1;
        private Button cmdEasy2;
        private Button cmdLibrary;
        private System.Windows.Forms.Button cmdProgress;
        private System.Windows.Forms.Button cmdSort;
        private System.Windows.Forms.Button cmdAsync;
        private System.Windows.Forms.Button cmdMasterDetail;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox picCurrent;
        private System.Windows.Forms.PictureBox picNewVersion;
        private System.Windows.Forms.Button cmdMultipleDeli;
        private System.Windows.Forms.Button cmdMultiTimming;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolTip tip;
        private System.Windows.Forms.Label lblVersion2;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Button cmdReadAsDatatable;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Button cmdRecordConditions;
        private System.ComponentModel.IContainer components;

        public frmSamples()
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof (frmSamples));
            this.cmdEasy = new System.Windows.Forms.Button();
            this.cmdDataLink = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdEasy2 = new System.Windows.Forms.Button();
            this.cmdLibrary = new System.Windows.Forms.Button();
            this.cmdProgress = new System.Windows.Forms.Button();
            this.cmdMultiTimming = new System.Windows.Forms.Button();
            this.cmdSort = new System.Windows.Forms.Button();
            this.cmdMultipleDeli = new System.Windows.Forms.Button();
            this.cmdAsync = new System.Windows.Forms.Button();
            this.cmdMasterDetail = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.picCurrent = new System.Windows.Forms.PictureBox();
            this.picNewVersion = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.lblVersion2 = new System.Windows.Forms.Label();
            this.cmdReadAsDatatable = new System.Windows.Forms.Button();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.cmdRecordConditions = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // cmdEasy
            // 
            this.cmdEasy.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdEasy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEasy.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdEasy.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdEasy.Image = ((System.Drawing.Image) (resources.GetObject("cmdEasy.Image")));
            this.cmdEasy.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEasy.Location = new System.Drawing.Point(11, 64);
            this.cmdEasy.Name = "cmdEasy";
            this.cmdEasy.Size = new System.Drawing.Size(216, 40);
            this.cmdEasy.TabIndex = 0;
            this.cmdEasy.Text = "Easy Delimited";
            this.cmdEasy.Click += new System.EventHandler(this.cmdEasy_Click);
            // 
            // cmdDataLink
            // 
            this.cmdDataLink.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdDataLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDataLink.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdDataLink.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdDataLink.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDataLink.Location = new System.Drawing.Point(424, 208);
            this.cmdDataLink.Name = "cmdDataLink";
            this.cmdDataLink.Size = new System.Drawing.Size(216, 40);
            this.cmdDataLink.TabIndex = 2;
            this.cmdDataLink.Text = "Access DataLink";
            this.cmdDataLink.Click += new System.EventHandler(this.cmdDataLink_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma",
                12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(231, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 40);
            this.button1.TabIndex = 4;
            this.button1.Text = "Exit";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdEasy2
            // 
            this.cmdEasy2.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdEasy2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEasy2.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdEasy2.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdEasy2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEasy2.Location = new System.Drawing.Point(11, 112);
            this.cmdEasy2.Name = "cmdEasy2";
            this.cmdEasy2.Size = new System.Drawing.Size(216, 40);
            this.cmdEasy2.TabIndex = 1;
            this.cmdEasy2.Text = "Easy Fixed";
            this.cmdEasy2.Click += new System.EventHandler(this.cmdEasy2_Click);
            // 
            // cmdLibrary
            // 
            this.cmdLibrary.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLibrary.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdLibrary.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdLibrary.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLibrary.Location = new System.Drawing.Point(424, 256);
            this.cmdLibrary.Name = "cmdLibrary";
            this.cmdLibrary.Size = new System.Drawing.Size(216, 40);
            this.cmdLibrary.TabIndex = 3;
            this.cmdLibrary.Text = "Time Testing";
            this.cmdLibrary.Click += new System.EventHandler(this.cmdLibrary_Click);
            // 
            // cmdProgress
            // 
            this.cmdProgress.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdProgress.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdProgress.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdProgress.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdProgress.Location = new System.Drawing.Point(424, 160);
            this.cmdProgress.Name = "cmdProgress";
            this.cmdProgress.Size = new System.Drawing.Size(216, 40);
            this.cmdProgress.TabIndex = 5;
            this.cmdProgress.Text = "Progress Notification";
            this.cmdProgress.Click += new System.EventHandler(this.cmdProgress_Click);
            // 
            // cmdMultiTimming
            // 
            this.cmdMultiTimming.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdMultiTimming.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMultiTimming.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdMultiTimming.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdMultiTimming.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdMultiTimming.Location = new System.Drawing.Point(424, 304);
            this.cmdMultiTimming.Name = "cmdMultiTimming";
            this.cmdMultiTimming.Size = new System.Drawing.Size(216, 40);
            this.cmdMultiTimming.TabIndex = 7;
            this.cmdMultiTimming.Text = "Mult Time Testing";
            this.cmdMultiTimming.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdSort
            // 
            this.cmdSort.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSort.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdSort.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdSort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSort.Location = new System.Drawing.Point(424, 112);
            this.cmdSort.Name = "cmdSort";
            this.cmdSort.Size = new System.Drawing.Size(216, 40);
            this.cmdSort.TabIndex = 9;
            this.cmdSort.Text = "Sorting";
            this.cmdSort.Click += new System.EventHandler(this.cmdSort_Click);
            // 
            // cmdMultipleDeli
            // 
            this.cmdMultipleDeli.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdMultipleDeli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMultipleDeli.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdMultipleDeli.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdMultipleDeli.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdMultipleDeli.Location = new System.Drawing.Point(424, 64);
            this.cmdMultipleDeli.Name = "cmdMultipleDeli";
            this.cmdMultipleDeli.Size = new System.Drawing.Size(216, 40);
            this.cmdMultipleDeli.TabIndex = 11;
            this.cmdMultipleDeli.Text = "Changing Delimiters";
            this.cmdMultipleDeli.Click += new System.EventHandler(this.cmdMiltipleDeli_Click);
            // 
            // cmdAsync
            // 
            this.cmdAsync.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdAsync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAsync.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdAsync.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdAsync.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAsync.Location = new System.Drawing.Point(11, 160);
            this.cmdAsync.Name = "cmdAsync";
            this.cmdAsync.Size = new System.Drawing.Size(216, 40);
            this.cmdAsync.TabIndex = 12;
            this.cmdAsync.Text = "Record by Record";
            this.cmdAsync.Click += new System.EventHandler(this.cmdAsync_Click);
            // 
            // cmdMasterDetail
            // 
            this.cmdMasterDetail.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdMasterDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMasterDetail.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdMasterDetail.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdMasterDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdMasterDetail.Location = new System.Drawing.Point(11, 256);
            this.cmdMasterDetail.Name = "cmdMasterDetail";
            this.cmdMasterDetail.Size = new System.Drawing.Size(216, 40);
            this.cmdMasterDetail.TabIndex = 15;
            this.cmdMasterDetail.Text = "Master Detail";
            this.cmdMasterDetail.Click += new System.EventHandler(this.cmdMasterDetail_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(224, 264);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(48, 30);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 16;
            this.pictureBox7.TabStop = false;
            // 
            // picCurrent
            // 
            this.picCurrent.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                    ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picCurrent.BackColor = System.Drawing.Color.Transparent;
            this.picCurrent.Image = ((System.Drawing.Image) (resources.GetObject("picCurrent.Image")));
            this.picCurrent.Location = new System.Drawing.Point(545, 352);
            this.picCurrent.Name = "picCurrent";
            this.picCurrent.Size = new System.Drawing.Size(146, 53);
            this.picCurrent.TabIndex = 17;
            this.picCurrent.TabStop = false;
            this.picCurrent.Visible = false;
            // 
            // picNewVersion
            // 
            this.picNewVersion.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                    ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picNewVersion.BackColor = System.Drawing.Color.Transparent;
            this.picNewVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picNewVersion.Image = ((System.Drawing.Image) (resources.GetObject("picNewVersion.Image")));
            this.picNewVersion.Location = new System.Drawing.Point(545, 352);
            this.picNewVersion.Name = "picNewVersion";
            this.picNewVersion.Size = new System.Drawing.Size(146, 53);
            this.picNewVersion.TabIndex = 18;
            this.picNewVersion.TabStop = false;
            this.picNewVersion.Visible = false;
            this.picNewVersion.Click += new System.EventHandler(this.picNewVersion_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(224, 168);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 30);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(224, 216);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(48, 30);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 20;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(640, 312);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(48, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 21;
            this.pictureBox4.TabStop = false;
            // 
            // tip
            // 
            this.tip.AutomaticDelay = 200;
            this.tip.AutoPopDelay = 20000;
            this.tip.InitialDelay = 200;
            this.tip.ReshowDelay = 0;
            this.tip.ShowAlways = true;
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox11.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(302, 244);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(80, 98);
            this.pictureBox11.TabIndex = 28;
            this.pictureBox11.TabStop = false;
            this.tip.SetToolTip(this.pictureBox11, "--> Devoo Software Home");
            this.pictureBox11.Click += new System.EventHandler(this.pictureBox11_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox8.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(280, 80);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(128, 152);
            this.pictureBox8.TabIndex = 23;
            this.pictureBox8.TabStop = false;
            this.tip.SetToolTip(this.pictureBox8, "--> FileHelpers Home");
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // lblVersion2
            // 
            this.lblVersion2.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion2.Font = new System.Drawing.Font("Tahoma",
                9F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.lblVersion2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte) (224)),
                ((System.Byte) (224)),
                ((System.Byte) (224)));
            this.lblVersion2.Location = new System.Drawing.Point(0, 386);
            this.lblVersion2.Name = "lblVersion2";
            this.lblVersion2.Size = new System.Drawing.Size(100, 18);
            this.lblVersion2.TabIndex = 22;
            this.lblVersion2.Text = "Version: ";
            // 
            // cmdReadAsDatatable
            // 
            this.cmdReadAsDatatable.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdReadAsDatatable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdReadAsDatatable.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdReadAsDatatable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdReadAsDatatable.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdReadAsDatatable.Location = new System.Drawing.Point(11, 208);
            this.cmdReadAsDatatable.Name = "cmdReadAsDatatable";
            this.cmdReadAsDatatable.Size = new System.Drawing.Size(216, 40);
            this.cmdReadAsDatatable.TabIndex = 24;
            this.cmdReadAsDatatable.Text = "Read as DataTable";
            this.cmdReadAsDatatable.Click += new System.EventHandler(this.cmdReadAsDatatable_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(640, 72);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(48, 30);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 25;
            this.pictureBox9.TabStop = false;
            // 
            // cmdRecordConditions
            // 
            this.cmdRecordConditions.BackColor = System.Drawing.Color.FromArgb(((System.Byte) (0)),
                ((System.Byte) (0)),
                ((System.Byte) (110)));
            this.cmdRecordConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdRecordConditions.Font = new System.Drawing.Font("Tahoma",
                11.25F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((System.Byte) (0)));
            this.cmdRecordConditions.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmdRecordConditions.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRecordConditions.Location = new System.Drawing.Point(11, 304);
            this.cmdRecordConditions.Name = "cmdRecordConditions";
            this.cmdRecordConditions.Size = new System.Drawing.Size(216, 40);
            this.cmdRecordConditions.TabIndex = 26;
            this.cmdRecordConditions.Text = "Record Conditions";
            this.cmdRecordConditions.Click += new System.EventHandler(this.cmdRecordConditions_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox10.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(224, 312);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(48, 30);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 27;
            this.pictureBox10.TabStop = false;
            // 
            // frmSamples
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(690, 429);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.cmdRecordConditions);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.cmdReadAsDatatable);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.lblVersion2);
            this.Controls.Add(this.cmdMasterDetail);
            this.Controls.Add(this.cmdMultipleDeli);
            this.Controls.Add(this.cmdSort);
            this.Controls.Add(this.cmdMultiTimming);
            this.Controls.Add(this.cmdProgress);
            this.Controls.Add(this.cmdLibrary);
            this.Controls.Add(this.cmdEasy2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdDataLink);
            this.Controls.Add(this.cmdEasy);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.cmdAsync);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.picCurrent);
            this.Controls.Add(this.picNewVersion);
            this.ExitOnEsc = false;
            this.Name = "frmSamples";
            this.Text = "FileHelpers Library - Samples ";
            this.Load += new System.EventHandler(this.frmSamples_Load);
            this.Activated += new System.EventHandler(this.frmSamples_Activated);
            this.Controls.SetChildIndex(this.picNewVersion, 0);
            this.Controls.SetChildIndex(this.picCurrent, 0);
            this.Controls.SetChildIndex(this.pictureBox10, 0);
            this.Controls.SetChildIndex(this.pictureBox5, 0);
            this.Controls.SetChildIndex(this.cmdAsync, 0);
            this.Controls.SetChildIndex(this.pictureBox7, 0);
            this.Controls.SetChildIndex(this.pictureBox6, 0);
            this.Controls.SetChildIndex(this.pictureBox4, 0);
            this.Controls.SetChildIndex(this.cmdEasy, 0);
            this.Controls.SetChildIndex(this.cmdDataLink, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.cmdEasy2, 0);
            this.Controls.SetChildIndex(this.cmdLibrary, 0);
            this.Controls.SetChildIndex(this.cmdProgress, 0);
            this.Controls.SetChildIndex(this.cmdMultiTimming, 0);
            this.Controls.SetChildIndex(this.cmdSort, 0);
            this.Controls.SetChildIndex(this.cmdMultipleDeli, 0);
            this.Controls.SetChildIndex(this.cmdMasterDetail, 0);
            this.Controls.SetChildIndex(this.lblVersion2, 0);
            this.Controls.SetChildIndex(this.pictureBox8, 0);
            this.Controls.SetChildIndex(this.cmdReadAsDatatable, 0);
            this.Controls.SetChildIndex(this.pictureBox9, 0);
            this.Controls.SetChildIndex(this.cmdRecordConditions, 0);
            this.Controls.SetChildIndex(this.pictureBox11, 0);
            this.ResumeLayout(false);
        }

        #endregion

        private void cmdEasy_Click(object sender, EventArgs e)
        {
            frmEasySampleDelimited frm = new frmEasySampleDelimited();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdDataLink_Click(object sender, EventArgs e)
        {
            frmDataLinkSample frm = new frmDataLinkSample();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdEasy2_Click(object sender, EventArgs e)
        {
            frmEasySampleFixed frm = new frmEasySampleFixed();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdLibrary_Click(object sender, EventArgs e)
        {
            frmTimming frm = new frmTimming();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdProgress_Click(object sender, System.EventArgs e)
        {
            frmProgressSample frm = new frmProgressSample();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            frmTimmingAdvanced frm = new frmTimmingAdvanced();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdSort_Click(object sender, System.EventArgs e)
        {
            frmSort frm = new frmSort();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdMiltipleDeli_Click(object sender, System.EventArgs e)
        {
            frmEasyMulti frm = new frmEasyMulti();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdAsync_Click(object sender, System.EventArgs e)
        {
            frmEasySampleAsync frm = new frmEasySampleAsync();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdMasterDetail_Click(object sender, System.EventArgs e)
        {
            frmMasterDetail frm = new frmMasterDetail();
            frm.ShowDialog();
            frm.Dispose();
        }


        private void frmSamples_Load(object sender, System.EventArgs e)
        {
            lblVersion2.Text += typeof (FileHelperEngine).Assembly.GetName().Version.ToString(3);

            cmdEasy2.Image = cmdEasy.Image;
            cmdAsync.Image = cmdEasy.Image;
            cmdMultipleDeli.Image = cmdEasy.Image;
            cmdMasterDetail.Image = cmdEasy.Image;
            cmdProgress.Image = cmdEasy.Image;
            cmdDataLink.Image = cmdEasy.Image;
            cmdSort.Image = cmdEasy.Image;
            cmdLibrary.Image = cmdEasy.Image;
            cmdMultiTimming.Image = cmdEasy.Image;
            cmdRecordConditions.Image = cmdEasy.Image;

            cmdReadAsDatatable.Image = cmdEasy.Image;
        }


        private bool mFirstTime = true;

        private void frmSamples_Activated(object sender, System.EventArgs e)
        {
            if (mFirstTime == false)
                return;

            mFirstTime = false;

            // check versions with internet ones in background
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(BuscarVersion));
        }

        /// <summary>
        /// Version details from the Internet
        /// </summary>
        private VersionData mLastVersion;

        /// <summary>
        /// Load the current version details from the Internet
        /// Then check the version of the engine against it.
        /// </summary>
        private void BuscarVersion(object target)
        {
            try {
                mLastVersion = VersionData.GetLastVersion();
                picNewVersion.Invoke(new SimpleHandler(MostrarVersion));
            }
            catch {}
        }

        /// <summary>
        /// Confirm the working version against the latest one on the web
        /// </summary>
        /// <remarks>
        /// Displays a 'current version' image or another image to encourage an update.
        /// </remarks>
        private void MostrarVersion()
        {
            if (mLastVersion == null)
                return;

            string ver = typeof (FileHelperEngine).Assembly.GetName().Version.ToString(3);
            if (VersionData.CompararVersiones(ver, mLastVersion.Version) >= 0)
                picCurrent.Visible = true;
            else {
                picNewVersion.Visible = true;
                picNewVersion.Tag = mLastVersion;
                tip.SetToolTip(picNewVersion,
                    "Version: " + mLastVersion.Version + Environment.NewLine + mLastVersion.Description);
            }
        }

        private delegate void SimpleHandler();

        private void picNewVersion_Click(object sender, System.EventArgs e)
        {
            frmLastVersion frm = new frmLastVersion((VersionData) picNewVersion.Tag);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void cmdReadAsDatatable_Click(object sender, System.EventArgs e)
        {
            frmEasyToDataTable frm = new frmEasyToDataTable();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void pictureBox11_Click(object sender, System.EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.devoo.net\"");
            Process.Start(info);
        }

        private void pictureBox8_Click(object sender, System.EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("explorer", "\"http://www.filehelpers.net\"");
            Process.Start(info);
        }

        private void cmdRecordConditions_Click(object sender, System.EventArgs e)
        {
            frmRecordConditions frm = new frmRecordConditions();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}