namespace FileHelpers.WizardApp
{
    partial class frmTextDiff
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
            this.txtTextA = new System.Windows.Forms.TextBox();
            this.txtTextB = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdPasteA = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdPasteB = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdCopyResult = new System.Windows.Forms.Button();
            this.cmdBNotA = new System.Windows.Forms.Button();
            this.cmdANotB = new System.Windows.Forms.Button();
            this.cmdUnion = new System.Windows.Forms.Button();
            this.cmdIntersect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            
            // 
            // txtTextA
            // 
            this.txtTextA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextA.Location = new System.Drawing.Point(6, 19);
            this.txtTextA.MaxLength = 50000000;
            this.txtTextA.Multiline = true;
            this.txtTextA.Name = "txtTextA";
            this.txtTextA.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextA.Size = new System.Drawing.Size(240, 385);
            this.txtTextA.TabIndex = 0;
            this.txtTextA.TextChanged += new System.EventHandler(this.txtTextA_TextChanged);
            // 
            // txtTextB
            // 
            this.txtTextB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextB.Location = new System.Drawing.Point(6, 19);
            this.txtTextB.MaxLength = 50000000;
            this.txtTextB.Multiline = true;
            this.txtTextB.Name = "txtTextB";
            this.txtTextB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextB.Size = new System.Drawing.Size(240, 385);
            this.txtTextB.TabIndex = 1;
            this.txtTextB.TextChanged += new System.EventHandler(this.txtTextB_TextChanged);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(6, 19);
            this.txtResult.MaxLength = 50000000;
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(219, 385);
            this.txtResult.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cmdPasteA);
            this.groupBox1.Controls.Add(this.txtTextA);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 439);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set A";
            // 
            // cmdPasteA
            // 
            this.cmdPasteA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPasteA.Image = global::FileHelpers.WizardApp.Properties.Resources.page_paste_icon;
            this.cmdPasteA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPasteA.Location = new System.Drawing.Point(6, 406);
            this.cmdPasteA.Name = "cmdPasteA";
            this.cmdPasteA.Size = new System.Drawing.Size(62, 29);
            this.cmdPasteA.TabIndex = 10;
            this.cmdPasteA.Text = "Paste";
            this.cmdPasteA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPasteA.UseVisualStyleBackColor = true;
            this.cmdPasteA.Click += new System.EventHandler(this.cmdPasteA_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.cmdPasteB);
            this.groupBox2.Controls.Add(this.txtTextB);
            this.groupBox2.Location = new System.Drawing.Point(287, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 439);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set B";
            // 
            // cmdPasteB
            // 
            this.cmdPasteB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPasteB.Image = global::FileHelpers.WizardApp.Properties.Resources.page_paste_icon;
            this.cmdPasteB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdPasteB.Location = new System.Drawing.Point(6, 406);
            this.cmdPasteB.Name = "cmdPasteB";
            this.cmdPasteB.Size = new System.Drawing.Size(62, 29);
            this.cmdPasteB.TabIndex = 11;
            this.cmdPasteB.Text = "Paste";
            this.cmdPasteB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPasteB.UseVisualStyleBackColor = true;
            this.cmdPasteB.Click += new System.EventHandler(this.cmdPasteB_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.cmdCopyResult);
            this.groupBox3.Controls.Add(this.txtResult);
            this.groupBox3.Location = new System.Drawing.Point(693, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 439);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Result";
            // 
            // cmdCopyResult
            // 
            this.cmdCopyResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCopyResult.Image = global::FileHelpers.WizardApp.Properties.Resources.page_copy;
            this.cmdCopyResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCopyResult.Location = new System.Drawing.Point(6, 406);
            this.cmdCopyResult.Name = "cmdCopyResult";
            this.cmdCopyResult.Size = new System.Drawing.Size(94, 29);
            this.cmdCopyResult.TabIndex = 9;
            this.cmdCopyResult.Text = "Copy Result";
            this.cmdCopyResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCopyResult.UseVisualStyleBackColor = true;
            this.cmdCopyResult.Click += new System.EventHandler(this.cmdCopyResult_Click);
            // 
            // cmdBNotA
            // 
            this.cmdBNotA.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdBNotA.BackColor = System.Drawing.Color.White;
            this.cmdBNotA.Image = global::FileHelpers.WizardApp.Properties.Resources.RightOnly;
            this.cmdBNotA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBNotA.Location = new System.Drawing.Point(558, 288);
            this.cmdBNotA.Name = "cmdBNotA";
            this.cmdBNotA.Size = new System.Drawing.Size(114, 31);
            this.cmdBNotA.TabIndex = 9;
            this.cmdBNotA.Text = "On B not A";
            this.cmdBNotA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBNotA.UseVisualStyleBackColor = false;
            this.cmdBNotA.Click += new System.EventHandler(this.cmdBNotA_Click);
            // 
            // cmdANotB
            // 
            this.cmdANotB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdANotB.BackColor = System.Drawing.Color.White;
            this.cmdANotB.Image = global::FileHelpers.WizardApp.Properties.Resources.LeftOnly;
            this.cmdANotB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdANotB.Location = new System.Drawing.Point(558, 251);
            this.cmdANotB.Name = "cmdANotB";
            this.cmdANotB.Size = new System.Drawing.Size(114, 31);
            this.cmdANotB.TabIndex = 5;
            this.cmdANotB.Text = "On A not B";
            this.cmdANotB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdANotB.UseVisualStyleBackColor = false;
            this.cmdANotB.Click += new System.EventHandler(this.cmdNor_Click);
            // 
            // cmdUnion
            // 
            this.cmdUnion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdUnion.BackColor = System.Drawing.Color.White;
            this.cmdUnion.Image = global::FileHelpers.WizardApp.Properties.Resources.Union;
            this.cmdUnion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUnion.Location = new System.Drawing.Point(558, 214);
            this.cmdUnion.Name = "cmdUnion";
            this.cmdUnion.Size = new System.Drawing.Size(114, 31);
            this.cmdUnion.TabIndex = 4;
            this.cmdUnion.Text = "On A or B";
            this.cmdUnion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdUnion.UseVisualStyleBackColor = false;
            this.cmdUnion.Click += new System.EventHandler(this.cmdUnion_Click);
            // 
            // cmdIntersect
            // 
            this.cmdIntersect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdIntersect.BackColor = System.Drawing.Color.White;
            this.cmdIntersect.Image = global::FileHelpers.WizardApp.Properties.Resources.Intersect;
            this.cmdIntersect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdIntersect.Location = new System.Drawing.Point(558, 177);
            this.cmdIntersect.Name = "cmdIntersect";
            this.cmdIntersect.Size = new System.Drawing.Size(114, 31);
            this.cmdIntersect.TabIndex = 3;
            this.cmdIntersect.Text = "On A and B";
            this.cmdIntersect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdIntersect.UseVisualStyleBackColor = false;
            this.cmdIntersect.Click += new System.EventHandler(this.cmdIntersect_Click);
            // 
            // frmTextDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 528);
            this.Controls.Add(this.cmdBNotA);
            this.Controls.Add(this.cmdANotB);
            this.Controls.Add(this.cmdUnion);
            this.Controls.Add(this.cmdIntersect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Name = "frmTextDiff";
            this.Text = "Text Diff Tool";
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cmdIntersect, 0);
            this.Controls.SetChildIndex(this.cmdUnion, 0);
            this.Controls.SetChildIndex(this.cmdANotB, 0);
            this.Controls.SetChildIndex(this.cmdBNotA, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextA;
        private System.Windows.Forms.TextBox txtTextB;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button cmdIntersect;
        private System.Windows.Forms.Button cmdUnion;
        private System.Windows.Forms.Button cmdANotB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdCopyResult;
        private System.Windows.Forms.Button cmdPasteA;
        private System.Windows.Forms.Button cmdPasteB;
        private System.Windows.Forms.Button cmdBNotA;
    }
}