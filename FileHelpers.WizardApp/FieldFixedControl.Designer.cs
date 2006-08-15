namespace FileHelpers.WizardApp
{
    partial class FieldFixedControl
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
            this.txtLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(7, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Length";
            this.label3.Click += new System.EventHandler(this.LabelControlsClick);
            // 
            // txtLength
            // 
            this.txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLength.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLength.Location = new System.Drawing.Point(45, 28);
            this.txtLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(50, 21);
            this.txtLength.TabIndex = 10;
            this.txtLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLength.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // FieldFixedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label3);
            this.Name = "FieldFixedControl";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtLength, 0);
            ((System.ComponentModel.ISupportInitialize)(this.txtLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtLength;
    }
}
