namespace ExamplesFx.Controls
{
    partial class ExampleRenderer
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
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmdRunDemo = new System.Windows.Forms.ToolStripButton();
            this.lblTestDescription = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitFiles = new System.Windows.Forms.SplitContainer();
            this.fileOutput = new ExamplesFramework.FileRenderer();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitFiles)).BeginInit();
            this.splitFiles.Panel1.SuspendLayout();
            this.splitFiles.Panel2.SuspendLayout();
            this.splitFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRunDemo,
            this.lblTestDescription});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(866, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdRunDemo
            // 
            this.cmdRunDemo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRunDemo.Image = global::ExamplesFramework.Properties.Resources.arrow_right;
            this.cmdRunDemo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRunDemo.Name = "cmdRunDemo";
            this.cmdRunDemo.Size = new System.Drawing.Size(110, 25);
            this.cmdRunDemo.Text = "Run Demo";
            this.cmdRunDemo.Visible = false;
            this.cmdRunDemo.Click += new System.EventHandler(this.cmdRunDemo_Click);
            // 
            // lblTestDescription
            // 
            this.lblTestDescription.Name = "lblTestDescription";
            this.lblTestDescription.Size = new System.Drawing.Size(0, 22);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(866, 400);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // splitFiles
            // 
            this.splitFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitFiles.Location = new System.Drawing.Point(0, 25);
            this.splitFiles.Name = "splitFiles";
            // 
            // splitFiles.Panel1
            // 
            this.splitFiles.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitFiles.Panel2
            // 
            this.splitFiles.Panel2.Controls.Add(this.fileOutput);
            this.splitFiles.Panel2Collapsed = true;
            this.splitFiles.Size = new System.Drawing.Size(866, 400);
            this.splitFiles.SplitterDistance = 557;
            this.splitFiles.TabIndex = 5;
            // 
            // fileOutput
            // 
            this.fileOutput.ColorBotton = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.fileOutput.ColorTop = System.Drawing.Color.Green;
            this.fileOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileOutput.File = null;
            this.fileOutput.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileOutput.Location = new System.Drawing.Point(0, 0);
            this.fileOutput.Name = "fileOutput";
            this.fileOutput.Size = new System.Drawing.Size(96, 100);
            this.fileOutput.TabIndex = 0;
            // 
            // ExampleRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitFiles);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Name = "ExampleRenderer";
            this.Size = new System.Drawing.Size(866, 425);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitFiles.Panel1.ResumeLayout(false);
            this.splitFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitFiles)).EndInit();
            this.splitFiles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cmdRunDemo;
        private System.Windows.Forms.ToolStripLabel lblTestDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitFiles;
        private FileRenderer fileOutput;
    }
}
