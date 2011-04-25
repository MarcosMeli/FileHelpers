using ExamplesFramework.Properties;
using FileHelpers;
namespace ExamplesFramework
{
    partial class frmExamples
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExamples));
            Devoo.WinForms.TextShape textShape1 = new Devoo.WinForms.TextShape();
            ExamplesFramework.DefaultExampleTheme defaultExampleTheme1 = new ExamplesFramework.DefaultExampleTheme();
            this.reflectionHeader1 = new Devoo.WinForms.ReflectionHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdCollapse = new System.Windows.Forms.ToolStripButton();
            this.cmdExpand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboSearchMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.extracthtml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdHistory = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvExamples = new ExamplesFramework.TreeViewExamples();
            this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.InfoSheet = new System.Windows.Forms.WebBrowser();
            this.exampleRenderer = new ExamplesFramework.ExampleHtmlRenderer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reflectionHeader1
            // 
            this.reflectionHeader1.BandDown.Color.Color1 = System.Drawing.Color.Black;
            this.reflectionHeader1.BandDown.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandDown.Color.Direction = Devoo.WinForms.GradientDirection.Horizontal;
            this.reflectionHeader1.BandDown.Height = 18;
            this.reflectionHeader1.BandUp.Color.Color1 = System.Drawing.Color.Black;
            this.reflectionHeader1.BandUp.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.reflectionHeader1.BandUp.Color.Direction = Devoo.WinForms.GradientDirection.Horizontal;
            this.reflectionHeader1.BandUp.Height = 0;
            this.reflectionHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.reflectionHeader1.GradientBack.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(1)))), ((int)(((byte)(74)))));
            this.reflectionHeader1.GradientBack.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(0)))), ((int)(((byte)(107)))));
            this.reflectionHeader1.GradientBack.Direction = Devoo.WinForms.GradientDirection.Vertical;
            this.reflectionHeader1.Header.Color.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.reflectionHeader1.Header.Color.Direction = Devoo.WinForms.GradientDirection.Vertical;
            this.reflectionHeader1.Header.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Bold);
            this.reflectionHeader1.Header.Position = new System.Drawing.Point(80, 17);
            this.reflectionHeader1.Header.ReflectionLevel = ((byte)(100));
            this.reflectionHeader1.Header.ReflectionOpacity = ((byte)(200));
            this.reflectionHeader1.Header.Text = "FileHelpers Examples";
            this.reflectionHeader1.Images.AddRange(new Devoo.WinForms.ImageShape[] {
            new Devoo.WinForms.ImageShape(((System.Drawing.Bitmap)(resources.GetObject("reflectionHeader1.Images"))), true, ((byte)(255)), new System.Drawing.Point(3, 1), ((byte)(0)), 0)});
            this.reflectionHeader1.Location = new System.Drawing.Point(0, 0);
            this.reflectionHeader1.Name = "reflectionHeader1";
            this.reflectionHeader1.Size = new System.Drawing.Size(934, 85);
            this.reflectionHeader1.Text = "FileHelpers Examples";
            textShape1.Color.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            textShape1.Color.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            textShape1.Color.Direction = Devoo.WinForms.GradientDirection.Vertical;
            textShape1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textShape1.Position = new System.Drawing.Point(615, 67);
            textShape1.ReflectionLevel = ((byte)(100));
            textShape1.ReflectionOpacity = ((byte)(0));
            textShape1.Text = "All FileHelpers Demos in one place";
            this.reflectionHeader1.Texts.AddRange(new Devoo.WinForms.TextShape[] {
            textShape1});
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCollapse,
            this.cmdExpand,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.txtSearch,
            this.toolStripLabel1,
            this.cboSearchMode,
            this.toolStripSeparator2,
            this.extracthtml,
            this.toolStripSeparator1,
            this.cmdHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 85);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(934, 34);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdCollapse.Image = ((System.Drawing.Image)(resources.GetObject("cmdCollapse.Image")));
            this.cmdCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(51, 31);
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdExpand
            // 
            this.cmdExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdExpand.Image = ((System.Drawing.Image)(resources.GetObject("cmdExpand.Image")));
            this.cmdExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(47, 31);
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 31);
            this.toolStripLabel2.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(121, 34);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(66, 31);
            this.toolStripLabel1.Text = "SearchMode";
            // 
            // cboSearchMode
            // 
            this.cboSearchMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchMode.Items.AddRange(new object[] {
            "Name",
            "Name & Descriptions",
            "Text, Descriptions & Code"});
            this.cboSearchMode.Name = "cboSearchMode";
            this.cboSearchMode.Size = new System.Drawing.Size(150, 34);
            this.cboSearchMode.SelectedIndexChanged += new System.EventHandler(this.cboSearchMode_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // extracthtml
            // 
            this.extracthtml.Image = global::ExamplesFramework.Properties.Resources.arrow_right;
            this.extracthtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.extracthtml.Name = "extracthtml";
            this.extracthtml.Size = new System.Drawing.Size(91, 31);
            this.extracthtml.Text = "Extract HTML";
            this.extracthtml.Click += new System.EventHandler(this.extracthtml_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // cmdHistory
            // 
            this.cmdHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdHistory.Name = "cmdHistory";
            this.cmdHistory.Size = new System.Drawing.Size(110, 31);
            this.cmdHistory.Text = "Show Library History";
            this.cmdHistory.Click += new System.EventHandler(this.cmdHistory_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 119);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvExamples);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.InfoSheet);
            this.splitContainer1.Panel2.Controls.Add(this.exampleRenderer);
            this.splitContainer1.Size = new System.Drawing.Size(934, 543);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 2;
            // 
            // tvExamples
            // 
            this.tvExamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvExamples.ImageIndex = 0;
            this.tvExamples.ImageList = this.imgTreeView;
            this.tvExamples.Location = new System.Drawing.Point(0, 0);
            this.tvExamples.Name = "tvExamples";
            this.tvExamples.SearchMode = ExamplesFramework.ExamplesSearchMode.Name;
            this.tvExamples.SearchText = null;
            this.tvExamples.SelectedImageIndex = 0;
            this.tvExamples.Size = new System.Drawing.Size(293, 543);
            this.tvExamples.TabIndex = 0;
            this.tvExamples.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExamples_AfterSelect);
            // 
            // imgTreeView
            // 
            this.imgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeView.ImageStream")));
            this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTreeView.Images.SetKeyName(0, "application_cascade.png");
            this.imgTreeView.Images.SetKeyName(1, "application_double.png");
            this.imgTreeView.Images.SetKeyName(2, "application_form.png");
            this.imgTreeView.Images.SetKeyName(3, "application_osx_terminal.png");
            this.imgTreeView.Images.SetKeyName(4, "demo");
            this.imgTreeView.Images.SetKeyName(5, "application_view_xp_terminal.png");
            this.imgTreeView.Images.SetKeyName(6, "application2.png");
            this.imgTreeView.Images.SetKeyName(7, "folder.png");
            this.imgTreeView.Images.SetKeyName(8, "folder");
            this.imgTreeView.Images.SetKeyName(9, "note.png");
            this.imgTreeView.Images.SetKeyName(10, "projection_screen.png");
            this.imgTreeView.Images.SetKeyName(11, "projection_screen_present.png");
            this.imgTreeView.Images.SetKeyName(12, "wand.png");
            // 
            // InfoSheet
            // 
            this.InfoSheet.Location = new System.Drawing.Point(288, 76);
            this.InfoSheet.MinimumSize = new System.Drawing.Size(20, 20);
            this.InfoSheet.Name = "InfoSheet";
            this.InfoSheet.Size = new System.Drawing.Size(250, 250);
            this.InfoSheet.TabIndex = 8;
            this.InfoSheet.Visible = false;
            // 
            // exampleRenderer
            // 
            this.exampleRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exampleRenderer.Example = null;
            this.exampleRenderer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exampleRenderer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.exampleRenderer.Location = new System.Drawing.Point(0, 0);
            this.exampleRenderer.Name = "exampleRenderer";
            this.exampleRenderer.Size = new System.Drawing.Size(637, 543);
            this.exampleRenderer.TabIndex = 7;
            this.exampleRenderer.Theme = defaultExampleTheme1;
            // 
            // frmExamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 662);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.reflectionHeader1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExamples";
            this.Text = "FileHelpers Examples";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExamples_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devoo.WinForms.ReflectionHeader reflectionHeader1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeViewExamples tvExamples;
        private System.Windows.Forms.ImageList imgTreeView;
        private System.Windows.Forms.ToolStripButton extracthtml;
        private System.Windows.Forms.ToolStripButton cmdHistory;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripComboBox cboSearchMode;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton cmdCollapse;
        private System.Windows.Forms.ToolStripButton cmdExpand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ExampleHtmlRenderer exampleRenderer;
        private System.Windows.Forms.WebBrowser InfoSheet;
    }
}