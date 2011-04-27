namespace ExamplesFramework.Framework.Controls
{
    partial class ExamplesContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamplesContainer));
            ExamplesFramework.DefaultExampleTheme defaultExampleTheme2 = new ExamplesFramework.DefaultExampleTheme();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdCollapse = new System.Windows.Forms.ToolStripButton();
            this.cmdExpand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.cboSearchMode = new System.Windows.Forms.ToolStripComboBox();
            this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.tvExamples = new ExamplesFramework.TreeViewExamples();
            this.exampleRenderer = new ExamplesFramework.ExampleHtmlRenderer();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.exampleRenderer);
            this.splitContainer1.Size = new System.Drawing.Size(1052, 520);
            this.splitContainer1.SplitterDistance = 295;
            this.splitContainer1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCollapse,
            this.cmdExpand,
            this.toolStripSeparator3,
            this.txtSearch,
            this.cboSearchMode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(295, 28);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCollapse.Image = global::ExamplesFramework.Properties.Resources.page_left;
            this.cmdCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(23, 20);
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdExpand
            // 
            this.cmdExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdExpand.Image = global::ExamplesFramework.Properties.Resources.page_down;
            this.cmdExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(23, 20);
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(80, 23);
            this.txtSearch.ToolTipText = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cboSearchMode
            // 
            this.cboSearchMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchMode.Items.AddRange(new object[] {
            "Name",
            "Name & Descriptions",
            "Text, Descriptions & Code"});
            this.cboSearchMode.Name = "cboSearchMode";
            this.cboSearchMode.Size = new System.Drawing.Size(135, 23);
            this.cboSearchMode.SelectedIndexChanged += new System.EventHandler(this.cboSearchMode_SelectedIndexChanged);
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
            // tvExamples
            // 
            this.tvExamples.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvExamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvExamples.ImageIndex = 0;
            this.tvExamples.ImageList = this.imgTreeView;
            this.tvExamples.Location = new System.Drawing.Point(0, 0);
            this.tvExamples.Name = "tvExamples";
            this.tvExamples.SearchMode = ExamplesFramework.ExamplesSearchMode.Name;
            this.tvExamples.SearchText = null;
            this.tvExamples.SelectedImageIndex = 0;
            this.tvExamples.Size = new System.Drawing.Size(291, 488);
            this.tvExamples.TabIndex = 0;
            this.tvExamples.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvExamples_AfterSelect);
            // 
            // exampleRenderer
            // 
            this.exampleRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exampleRenderer.Example = null;
            this.exampleRenderer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exampleRenderer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.exampleRenderer.Location = new System.Drawing.Point(0, 0);
            this.exampleRenderer.Name = "exampleRenderer";
            this.exampleRenderer.Size = new System.Drawing.Size(753, 520);
            this.exampleRenderer.TabIndex = 7;
            this.exampleRenderer.Theme = defaultExampleTheme2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tvExamples);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 492);
            this.panel1.TabIndex = 3;
            // 
            // ExamplesContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExamplesContainer";
            this.Size = new System.Drawing.Size(1052, 520);
            this.Load += new System.EventHandler(this.ExamplesContainer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeViewExamples tvExamples;
        private ExampleHtmlRenderer exampleRenderer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdCollapse;
        private System.Windows.Forms.ToolStripButton cmdExpand;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripComboBox cboSearchMode;
        private System.Windows.Forms.ImageList imgTreeView;
        private System.Windows.Forms.Panel panel1;
    }
}
