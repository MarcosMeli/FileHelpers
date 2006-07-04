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
            this.dgbPreview = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // dgbPreview
            // 
            this.dgbPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbPreview.Location = new System.Drawing.Point(12, 12);
            this.dgbPreview.Name = "dgbPreview";
            this.dgbPreview.Size = new System.Drawing.Size(486, 411);
            this.dgbPreview.TabIndex = 0;
            // 
            // frmDataPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 435);
            this.Controls.Add(this.dgbPreview);
            this.Name = "frmDataPreview";
            this.Text = "Read Data Preview";
            ((System.ComponentModel.ISupportInitialize)(this.dgbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgbPreview;
    }
}