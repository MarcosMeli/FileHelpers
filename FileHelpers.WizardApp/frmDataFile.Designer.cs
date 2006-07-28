namespace FileHelpers.WizardApp
{
    partial class frmDataFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataFile));
            this.rulerControl2 = new MyControls.RulerControl();
            this.txtData = new FileHelpers.WizardApp.FixedWithDesigner();
            this.rulerControl1 = new MyControls.RulerControl();
            this.SuspendLayout();
            // 
            // rulerControl2
            // 
            this.rulerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rulerControl2.DivisionMarkFactor = 4;
            this.rulerControl2.Divisions = 5;
            this.rulerControl2.ForeColor = System.Drawing.Color.Gray;
            this.rulerControl2.Location = new System.Drawing.Point(11, 309);
            this.rulerControl2.MajorInterval = 5;
            this.rulerControl2.MiddleMarkFactor = 3;
            this.rulerControl2.MouseTrackingOn = false;
            this.rulerControl2.Name = "rulerControl2";
            this.rulerControl2.Orientation = MyControls.OrientationMode.Horizontal;
            this.rulerControl2.RulerAlignment = MyControls.RulerAlignment.TopOrLeft;
            this.rulerControl2.ScaleMode = MyControls.ScaleMode.Points;
            this.rulerControl2.Size = new System.Drawing.Size(622, 21);
            this.rulerControl2.StartValue = 0;
            this.rulerControl2.TabIndex = 1;
            this.rulerControl2.Text = "rulerControl2";
            this.rulerControl2.VerticalNumbers = false;
            this.rulerControl2.ZoomFactor = 1;
            // 
            // txtData
            // 
            this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtData.AutoScroll = true;
            this.txtData.FontSize = 12;
            this.txtData.Location = new System.Drawing.Point(12, 32);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(620, 278);
            this.txtData.TabIndex = 2;
            this.txtData.TextLeft = 0;
            this.txtData.TextTop = 18;
            this.txtData.Click += new System.EventHandler(this.txtData_Click);
            // 
            // rulerControl1
            // 
            this.rulerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rulerControl1.DivisionMarkFactor = 4;
            this.rulerControl1.Divisions = 5;
            this.rulerControl1.ForeColor = System.Drawing.Color.Gray;
            this.rulerControl1.Location = new System.Drawing.Point(11, 12);
            this.rulerControl1.MajorInterval = 5;
            this.rulerControl1.MiddleMarkFactor = 3;
            this.rulerControl1.MouseTrackingOn = false;
            this.rulerControl1.Name = "rulerControl1";
            this.rulerControl1.Orientation = MyControls.OrientationMode.Horizontal;
            this.rulerControl1.RulerAlignment = MyControls.RulerAlignment.BottomOrRight;
            this.rulerControl1.ScaleMode = MyControls.ScaleMode.Points;
            this.rulerControl1.Size = new System.Drawing.Size(622, 21);
            this.rulerControl1.StartValue = 0;
            this.rulerControl1.TabIndex = 0;
            this.rulerControl1.Text = "rulerControl1";
            this.rulerControl1.VerticalNumbers = false;
            this.rulerControl1.ZoomFactor = 1;
            // 
            // frmDataFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 342);
            this.Controls.Add(this.rulerControl2);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.rulerControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDataFile";
            this.Text = "Fixed Length Record Designer";
            this.ResumeLayout(false);

        }

        #endregion

        private MyControls.RulerControl rulerControl1;
        private MyControls.RulerControl rulerControl2;
        private FixedWithDesigner txtData;
    }
}