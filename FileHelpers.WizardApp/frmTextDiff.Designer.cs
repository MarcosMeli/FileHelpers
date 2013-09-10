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
            this.cmdIntersect = new System.Windows.Forms.Button();
            this.cmdUnion = new System.Windows.Forms.Button();
            this.cmdNor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdPasteA = new System.Windows.Forms.Button();
            this.cmdCopyResult = new System.Windows.Forms.Button();
            this.cmdPasteB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTextA
            // 
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
            this.txtResult.Location = new System.Drawing.Point(6, 19);
            this.txtResult.MaxLength = 50000000;
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(219, 385);
            this.txtResult.TabIndex = 2;
            // 
            // cmdIntersect
            // 
            this.cmdIntersect.Location = new System.Drawing.Point(545, 108);
            this.cmdIntersect.Name = "cmdIntersect";
            this.cmdIntersect.Size = new System.Drawing.Size(128, 23);
            this.cmdIntersect.TabIndex = 3;
            this.cmdIntersect.Text = "On A and B";
            this.cmdIntersect.UseVisualStyleBackColor = true;
            this.cmdIntersect.Click += new System.EventHandler(this.cmdIntersect_Click);
            // 
            // cmdUnion
            // 
            this.cmdUnion.Location = new System.Drawing.Point(545, 137);
            this.cmdUnion.Name = "cmdUnion";
            this.cmdUnion.Size = new System.Drawing.Size(128, 23);
            this.cmdUnion.TabIndex = 4;
            this.cmdUnion.Text = "On A or B";
            this.cmdUnion.UseVisualStyleBackColor = true;
            this.cmdUnion.Click += new System.EventHandler(this.cmdUnion_Click);
            // 
            // cmdNor
            // 
            this.cmdNor.Location = new System.Drawing.Point(545, 166);
            this.cmdNor.Name = "cmdNor";
            this.cmdNor.Size = new System.Drawing.Size(128, 23);
            this.cmdNor.TabIndex = 5;
            this.cmdNor.Text = "On A not B";
            this.cmdNor.UseVisualStyleBackColor = true;
            this.cmdNor.Click += new System.EventHandler(this.cmdNor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdPasteA);
            this.groupBox1.Controls.Add(this.txtTextA);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 439);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text A";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdPasteB);
            this.groupBox2.Controls.Add(this.txtTextB);
            this.groupBox2.Location = new System.Drawing.Point(287, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 439);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text A";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdCopyResult);
            this.groupBox3.Controls.Add(this.txtResult);
            this.groupBox3.Location = new System.Drawing.Point(693, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 439);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Result";
            // 
            // cmdPasteA
            // 
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
            // cmdCopyResult
            // 
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
            // cmdPasteB
            // 
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
            // frmTextDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 460);
            this.Controls.Add(this.cmdNor);
            this.Controls.Add(this.cmdUnion);
            this.Controls.Add(this.cmdIntersect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmTextDiff";
            this.Text = "Text Diff Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextA;
        private System.Windows.Forms.TextBox txtTextB;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button cmdIntersect;
        private System.Windows.Forms.Button cmdUnion;
        private System.Windows.Forms.Button cmdNor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdCopyResult;
        private System.Windows.Forms.Button cmdPasteA;
        private System.Windows.Forms.Button cmdPasteB;
    }
}