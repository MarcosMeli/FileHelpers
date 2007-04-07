namespace FileHelpers.WizardApp
{
    partial class frmDataPreview
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
            Fireball.Windows.Forms.LineMarginRender lineMarginRender1 = new Fireball.Windows.Forms.LineMarginRender();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataPreview));
            this.dgPreview = new System.Windows.Forms.DataGridView();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboClassLeng = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClass = new Fireball.Windows.Forms.CodeEditorControl();
            this.sdClassOut = new Fireball.Syntax.SyntaxDocument(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.cmdReadTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.dlgOpenTest = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgPreview)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPreview
            // 
            this.dgPreview.AllowUserToAddRows = false;
            this.dgPreview.AllowUserToDeleteRows = false;
            this.dgPreview.AllowUserToResizeRows = false;
            this.dgPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgPreview.ColumnHeadersHeight = 25;
            this.dgPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgPreview.Location = new System.Drawing.Point(0, 29);
            this.dgPreview.Name = "dgPreview";
            this.dgPreview.ReadOnly = true;
            this.dgPreview.RowHeadersVisible = false;
            this.dgPreview.ShowEditingIcon = false;
            this.dgPreview.Size = new System.Drawing.Size(483, 208);
            this.dgPreview.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(782, 436);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(782, 461);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.SlateGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboClassLeng);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtClass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(782, 436);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.SplitterIncrement = 4;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // cboClassLeng
            // 
            this.cboClassLeng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClassLeng.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassLeng.FormattingEnabled = true;
            this.cboClassLeng.Items.AddRange(new object[] {
            "C#",
            "VB.NET"});
            this.cboClassLeng.Location = new System.Drawing.Point(233, 3);
            this.cboClassLeng.Name = "cboClassLeng";
            this.cboClassLeng.Size = new System.Drawing.Size(60, 21);
            this.cboClassLeng.TabIndex = 1007;
            this.cboClassLeng.SelectedIndexChanged += new System.EventHandler(this.cboClassLeng_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 30);
            this.label3.TabIndex = 1013;
            this.label3.Text = "Class Source";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClass
            // 
            this.txtClass.ActiveView = Fireball.Windows.Forms.CodeEditor.ActiveView.BottomRight;
            this.txtClass.AllowBreakPoints = false;
            this.txtClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClass.AutoListAutoSelect = false;
            this.txtClass.AutoListPosition = null;
            this.txtClass.AutoListSelectedText = "a123";
            this.txtClass.AutoListVisible = false;
            this.txtClass.CopyAsRTF = false;
            this.txtClass.Document = this.sdClassOut;
            this.txtClass.FontSize = 9F;
            this.txtClass.GutterMarginWidth = 0;
            this.txtClass.Indent = Fireball.Windows.Forms.CodeEditor.IndentStyle.Smart;
            this.txtClass.InfoTipCount = 1;
            this.txtClass.InfoTipPosition = null;
            this.txtClass.InfoTipSelectedIndex = 1;
            this.txtClass.InfoTipVisible = false;
            lineMarginRender1.Bounds = new System.Drawing.Rectangle(0, 0, 18, 14);
            this.txtClass.LineMarginRender = lineMarginRender1;
            this.txtClass.Location = new System.Drawing.Point(0, 29);
            this.txtClass.LockCursorUpdate = false;
            this.txtClass.Name = "txtClass";
            this.txtClass.ParseOnPaste = true;
            this.txtClass.Saved = true;
            this.txtClass.ShowGutterMargin = false;
            this.txtClass.ShowScopeIndicator = false;
            this.txtClass.Size = new System.Drawing.Size(296, 407);
            this.txtClass.SmoothScroll = false;
            this.txtClass.SplitView = false;
            this.txtClass.SplitviewH = -4;
            this.txtClass.SplitviewV = -4;
            this.txtClass.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.txtClass.TabIndex = 1008;
            this.txtClass.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // sdClassOut
            // 
            this.sdClassOut.Lines = new string[] {
        " "};
            this.sdClassOut.MaxUndoBufferSize = 1;
            this.sdClassOut.Modified = false;
            this.sdClassOut.UndoStep = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.txtInput);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblResults);
            this.splitContainer2.Panel2.Controls.Add(this.cmdReadTest);
            this.splitContainer2.Panel2.Controls.Add(this.dgPreview);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(483, 436);
            this.splitContainer2.SplitterDistance = 196;
            this.splitContainer2.SplitterIncrement = 4;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(483, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtInput.Location = new System.Drawing.Point(0, 29);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInput.Size = new System.Drawing.Size(483, 166);
            this.txtInput.TabIndex = 0;
            this.txtInput.WordWrap = false;
            // 
            // lblResults
            // 
            this.lblResults.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lblResults.Location = new System.Drawing.Point(359, 6);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(127, 20);
            this.lblResults.TabIndex = 1013;
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdReadTest
            // 
            this.cmdReadTest.Image = global::FileHelpers.WizardApp.Properties.Resources.tick;
            this.cmdReadTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdReadTest.Location = new System.Drawing.Point(0, 1);
            this.cmdReadTest.Name = "cmdReadTest";
            this.cmdReadTest.Size = new System.Drawing.Size(144, 28);
            this.cmdReadTest.TabIndex = 1011;
            this.cmdReadTest.Text = "Compile and Read Data";
            this.cmdReadTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdReadTest.UseVisualStyleBackColor = true;
            this.cmdReadTest.Click += new System.EventHandler(this.cmdReadTest_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(483, 30);
            this.label2.TabIndex = 1012;
            this.label2.Text = "DataTable Results";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(433, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::FileHelpers.WizardApp.Properties.Resources.folder_table;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 22);
            this.toolStripButton1.Text = "Load XML Class";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::FileHelpers.WizardApp.Properties.Resources.folder_table;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(114, 22);
            this.toolStripButton3.Text = "Load Source Class";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton2.Text = "Paste Class";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(113, 22);
            this.toolStripButton4.Text = "Load Sample Data";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // dlgOpenTest
            // 
            this.dlgOpenTest.DefaultExt = "txt";
            this.dlgOpenTest.FileName = "openFileDialog1";
            this.dlgOpenTest.Filter = "Text Files (*.txt) |*.txt";
            this.dlgOpenTest.Title = "Load a Test File";
            // 
            // frmDataPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 461);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDataPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read Class Tester - Results Preview";
            this.Load += new System.EventHandler(this.frmDataPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPreview)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPreview;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ComboBox cboClassLeng;
        private Fireball.Windows.Forms.CodeEditorControl txtClass;
        private Fireball.Syntax.SyntaxDocument sdClassOut;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button cmdReadTest;
        private System.Windows.Forms.OpenFileDialog dlgOpenTest;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label lblResults;
    }
}