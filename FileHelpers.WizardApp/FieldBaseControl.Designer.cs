namespace FileHelpers.WizardApp
{
    partial class FieldBaseControl
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.LinkLabel();
            this.lblDown = new System.Windows.Forms.LinkLabel();
            this.lblDelete = new System.Windows.Forms.LinkLabel();
            this.cboTrim = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkOptional = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(45, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(104, 21);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtType
            // 
            this.txtType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtType.Items.AddRange(new object[] {
            "String",
            "DateTime",
            "Int16",
            "Int32",
            "Int64",
            "Decimal",
            "Double",
            "Single",
            "Bool"});
            this.txtType.Location = new System.Drawing.Point(186, 5);
            this.txtType.MaxDropDownItems = 20;
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(70, 21);
            this.txtType.TabIndex = 1;
            this.txtType.SelectedIndexChanged += new System.EventHandler(this.txtType_SelectedIndexChanged);
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            this.label1.Click += new System.EventHandler(this.LabelControlsClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(154, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type";
            this.label2.Click += new System.EventHandler(this.LabelControlsClick);
            // 
            // lblUp
            // 
            this.lblUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUp.AutoSize = true;
            this.lblUp.BackColor = System.Drawing.Color.Transparent;
            this.lblUp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUp.Location = new System.Drawing.Point(283, 48);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(22, 13);
            this.lblUp.TabIndex = 1000;
            this.lblUp.TabStop = true;
            this.lblUp.Text = "Up";
            this.lblUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUp_LinkClicked);
            // 
            // lblDown
            // 
            this.lblDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDown.AutoSize = true;
            this.lblDown.BackColor = System.Drawing.Color.Transparent;
            this.lblDown.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDown.Location = new System.Drawing.Point(308, 48);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(38, 13);
            this.lblDown.TabIndex = 1001;
            this.lblDown.TabStop = true;
            this.lblDown.Text = "Down";
            this.lblDown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDown_LinkClicked);
            // 
            // lblDelete
            // 
            this.lblDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelete.AutoSize = true;
            this.lblDelete.BackColor = System.Drawing.Color.Transparent;
            this.lblDelete.LinkColor = System.Drawing.Color.Maroon;
            this.lblDelete.Location = new System.Drawing.Point(242, 48);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(38, 13);
            this.lblDelete.TabIndex = 1003;
            this.lblDelete.TabStop = true;
            this.lblDelete.Text = "Delete";
            this.lblDelete.VisitedLinkColor = System.Drawing.Color.Red;
            this.lblDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDelete_LinkClicked);
            // 
            // cboTrim
            // 
            this.cboTrim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboTrim.Items.AddRange(new object[] {
            "None",
            "Left",
            "Right",
            "Both"});
            this.cboTrim.Location = new System.Drawing.Point(287, 5);
            this.cboTrim.MaxDropDownItems = 15;
            this.cboTrim.Name = "cboTrim";
            this.cboTrim.Size = new System.Drawing.Size(58, 21);
            this.cboTrim.TabIndex = 2;
            this.cboTrim.SelectedIndexChanged += new System.EventHandler(this.cboTrim_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(261, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1005;
            this.label3.Text = "Trim";
            // 
            // chkOptional
            // 
            this.chkOptional.AutoSize = true;
            this.chkOptional.BackColor = System.Drawing.Color.Transparent;
            this.chkOptional.Location = new System.Drawing.Point(261, 31);
            this.chkOptional.Name = "chkOptional";
            this.chkOptional.Size = new System.Drawing.Size(91, 17);
            this.chkOptional.TabIndex = 1006;
            this.chkOptional.Text = "Optional Field";
            this.chkOptional.UseVisualStyleBackColor = false;
            this.chkOptional.CheckedChanged += new System.EventHandler(this.chkOptional_CheckedChanged);
            // 
            // FieldBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOptional);
            this.Controls.Add(this.cboTrim);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.lblDown);
            this.Controls.Add(this.lblUp);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FieldBaseControl";
            this.Size = new System.Drawing.Size(348, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox txtType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblUp;
        private System.Windows.Forms.LinkLabel lblDown;
        private System.Windows.Forms.LinkLabel lblDelete;
        private System.Windows.Forms.ComboBox cboTrim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkOptional;
    }
}
