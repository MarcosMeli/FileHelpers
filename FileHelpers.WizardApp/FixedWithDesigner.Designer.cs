namespace FileHelpers.WizardApp
{
    partial class FixedWithDesigner
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
            this.components = new System.ComponentModel.Container();
            this.cmnuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdShowInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtColumnName = new System.Windows.Forms.ToolStripTextBox();
            this.cmnuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmnuOptions
            // 
            this.cmnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdShowInfo,
            this.toolStripSeparator1,
            this.cmdDeleteColumn,
            this.toolStripSeparator2,
            this.txtColumnName});
            this.cmnuOptions.Name = "contextMenuStrip1";
            this.cmnuOptions.Size = new System.Drawing.Size(190, 105);
            // 
            // cmdShowInfo
            // 
            this.cmdShowInfo.Name = "cmdShowInfo";
            this.cmdShowInfo.Size = new System.Drawing.Size(189, 22);
            this.cmdShowInfo.Text = "Show Extended Info";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // cmdDeleteColumn
            // 
            this.cmdDeleteColumn.Name = "cmdDeleteColumn";
            this.cmdDeleteColumn.Size = new System.Drawing.Size(189, 22);
            this.cmdDeleteColumn.Text = "Delete Column";
            this.cmdDeleteColumn.Click += new System.EventHandler(this.cmdDeleteColumn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // txtColumnName
            // 
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(100, 21);
            this.txtColumnName.Text = "Mi Name";
            this.txtColumnName.ToolTipText = "Change the name of the column";
            // 
            // FixedWithDesigner
            // 
            this.Name = "FixedWithDesigner";
            this.cmnuOptions.ResumeLayout(false);
            this.cmnuOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
