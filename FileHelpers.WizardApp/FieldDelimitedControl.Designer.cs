namespace FileHelpers.WizardApp
{
    partial class FieldDelimitedControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtQuoted = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(5, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Quoted";
            this.label3.Click += new System.EventHandler(this.LabelControlsClick);
            // 
            // txtQuoted
            // 
            this.txtQuoted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuoted.Location = new System.Drawing.Point(45, 28);
            this.txtQuoted.MaxLength = 2;
            this.txtQuoted.Name = "txtQuoted";
            this.txtQuoted.Size = new System.Drawing.Size(26, 21);
            this.txtQuoted.TabIndex = 10;
            this.txtQuoted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuoted.TextChanged += new System.EventHandler(this.txtQuoted_TextChanged);
            // 
            // FieldDelimitedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtQuoted);
            this.Controls.Add(this.label3);
            this.Name = "FieldDelimitedControl";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtQuoted, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtQuoted;
    }
}
