namespace Demo.IronPython.Hosting
{
    partial class DemoForm
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
            this.LoadModuleButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LoadDataButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoadModuleButton
            // 
            this.LoadModuleButton.Location = new System.Drawing.Point(14, 8);
            this.LoadModuleButton.Name = "LoadModuleButton";
            this.LoadModuleButton.Size = new System.Drawing.Size(84, 34);
            this.LoadModuleButton.TabIndex = 0;
            this.LoadModuleButton.Text = "Load Python Module";
            this.LoadModuleButton.UseVisualStyleBackColor = true;
            this.LoadModuleButton.Click += new System.EventHandler(this.LoadModuleButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Maroon;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox1.Location = new System.Drawing.Point(0, 48);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(462, 306);
            this.textBox1.TabIndex = 1;
            // 
            // LoadDataButton
            // 
            this.LoadDataButton.Location = new System.Drawing.Point(104, 8);
            this.LoadDataButton.Name = "LoadDataButton";
            this.LoadDataButton.Size = new System.Drawing.Size(84, 34);
            this.LoadDataButton.TabIndex = 2;
            this.LoadDataButton.Text = "Load CINLIST file";
            this.LoadDataButton.UseVisualStyleBackColor = true;
            this.LoadDataButton.Click += new System.EventHandler(this.LoadDataButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.Enabled = false;
            this.ProcessButton.Location = new System.Drawing.Point(194, 8);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(84, 34);
            this.ProcessButton.TabIndex = 3;
            this.ProcessButton.Text = "Process!";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 354);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.LoadDataButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LoadModuleButton);
            this.Name = "Form1";
            this.Text = "IronPython Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadModuleButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button LoadDataButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ProcessButton;


    }
}