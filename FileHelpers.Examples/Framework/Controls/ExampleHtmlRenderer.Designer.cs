namespace ExamplesFramework
{
    partial class ExampleHtmlRenderer
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
            this.browserExample = new System.Windows.Forms.WebBrowser();
            this.browserOutput = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRunDemo});
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
            // browserExample
            // 
            this.browserExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserExample.Location = new System.Drawing.Point(0, 0);
            this.browserExample.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserExample.Name = "browserExample";
            this.browserExample.Size = new System.Drawing.Size(866, 400);
            this.browserExample.TabIndex = 9;
            // 
            // browserOutput
            // 
            this.browserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserOutput.Location = new System.Drawing.Point(0, 0);
            this.browserOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserOutput.Name = "browserOutput";
            this.browserOutput.Size = new System.Drawing.Size(150, 46);
            this.browserOutput.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.browserExample);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.browserOutput);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(866, 400);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 11;
            // 
            // ExampleHtmlRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Name = "ExampleHtmlRenderer";
            this.Size = new System.Drawing.Size(866, 425);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cmdRunDemo;
        private System.Windows.Forms.WebBrowser browserExample;
        private System.Windows.Forms.WebBrowser browserOutput;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
