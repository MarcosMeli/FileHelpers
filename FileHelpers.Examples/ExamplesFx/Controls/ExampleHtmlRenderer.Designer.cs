namespace ExamplesFx.Controls
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
            this.splitOutHorizontal = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitBottom = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.browserInput = new System.Windows.Forms.WebBrowser();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitOutHorizontal)).BeginInit();
            this.splitOutHorizontal.Panel1.SuspendLayout();
            this.splitOutHorizontal.Panel2.SuspendLayout();
            this.splitOutHorizontal.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).BeginInit();
            this.splitBottom.Panel1.SuspendLayout();
            this.splitBottom.Panel2.SuspendLayout();
            this.splitBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRunDemo});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(866, 28);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdRunDemo
            // 
            this.cmdRunDemo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRunDemo.Image = global::ExamplesFx.Properties.Resources.arrow_right;
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
            this.browserExample.Size = new System.Drawing.Size(862, 203);
            this.browserExample.TabIndex = 9;
            // 
            // browserOutput
            // 
            this.browserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserOutput.Location = new System.Drawing.Point(0, 23);
            this.browserOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserOutput.Name = "browserOutput";
            this.browserOutput.Size = new System.Drawing.Size(574, 163);
            this.browserOutput.TabIndex = 10;
            // 
            // splitOutHorizontal
            // 
            this.splitOutHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitOutHorizontal.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitOutHorizontal.Location = new System.Drawing.Point(0, 28);
            this.splitOutHorizontal.Name = "splitOutHorizontal";
            this.splitOutHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitOutHorizontal.Panel1
            // 
            this.splitOutHorizontal.Panel1.Controls.Add(this.panel1);
            // 
            // splitOutHorizontal.Panel2
            // 
            this.splitOutHorizontal.Panel2.Controls.Add(this.splitBottom);
            this.splitOutHorizontal.Size = new System.Drawing.Size(866, 397);
            this.splitOutHorizontal.SplitterDistance = 207;
            this.splitOutHorizontal.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.browserExample);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 207);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(574, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Console";
            // 
            // splitBottom
            // 
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBottom.Location = new System.Drawing.Point(0, 0);
            this.splitBottom.Name = "splitBottom";
            // 
            // splitBottom.Panel1
            // 
            this.splitBottom.Panel1.Controls.Add(this.browserInput);
            this.splitBottom.Panel1.Controls.Add(this.label2);
            // 
            // splitBottom.Panel2
            // 
            this.splitBottom.Panel2.Controls.Add(this.browserOutput);
            this.splitBottom.Panel2.Controls.Add(this.label1);
            this.splitBottom.Size = new System.Drawing.Size(866, 186);
            this.splitBottom.SplitterDistance = 288;
            this.splitBottom.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "Input";
            // 
            // browserInput
            // 
            this.browserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserInput.Location = new System.Drawing.Point(0, 23);
            this.browserInput.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserInput.Name = "browserInput";
            this.browserInput.Size = new System.Drawing.Size(288, 163);
            this.browserInput.TabIndex = 13;
            // 
            // ExampleHtmlRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitOutHorizontal);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Name = "ExampleHtmlRenderer";
            this.Size = new System.Drawing.Size(866, 425);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitOutHorizontal.Panel1.ResumeLayout(false);
            this.splitOutHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitOutHorizontal)).EndInit();
            this.splitOutHorizontal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitBottom.Panel1.ResumeLayout(false);
            this.splitBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).EndInit();
            this.splitBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cmdRunDemo;
        private System.Windows.Forms.WebBrowser browserExample;
        private System.Windows.Forms.WebBrowser browserOutput;
        private System.Windows.Forms.SplitContainer splitOutHorizontal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitBottom;
        private System.Windows.Forms.WebBrowser browserInput;
        private System.Windows.Forms.Label label2;
    }
}
