namespace FileHelpers.WizardApp
{
    partial class FixedWidthDesigner
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
            this.addColumnHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmnuOptions
            // 
            this.cmnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addColumnHereToolStripMenuItem,
            this.toolStripSeparator3,
            this.cmdShowInfo,
            this.toolStripSeparator1,
            this.cmdDeleteColumn,
            this.toolStripSeparator2,
            this.txtColumnName});
            this.cmnuOptions.Name = "contextMenuStrip1";
            this.cmnuOptions.Size = new System.Drawing.Size(179, 113);
            // 
            // cmdShowInfo
            // 
            this.cmdShowInfo.Name = "cmdShowInfo";
            this.cmdShowInfo.Size = new System.Drawing.Size(178, 22);
            this.cmdShowInfo.Text = "Show Extended Info";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // cmdDeleteColumn
            // 
            this.cmdDeleteColumn.Name = "cmdDeleteColumn";
            this.cmdDeleteColumn.Size = new System.Drawing.Size(178, 22);
            this.cmdDeleteColumn.Text = "Delete Column";
            this.cmdDeleteColumn.Click += new System.EventHandler(this.cmdDeleteColumn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // txtColumnName
            // 
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(100, 23);
            this.txtColumnName.Text = "Mi Name";
            this.txtColumnName.ToolTipText = "Change the name of the column";
            // 
            // addColumnHereToolStripMenuItem
            // 
            this.addColumnHereToolStripMenuItem.Name = "addColumnHereToolStripMenuItem";
            this.addColumnHereToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.addColumnHereToolStripMenuItem.Text = "AddColumnHere";
            this.addColumnHereToolStripMenuItem.Click += new System.EventHandler(this.addColumnHereToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(175, 6);
            // 
            // FixedWidthDesigner
            // 
            this.Name = "FixedWidthDesigner";
            this.Size = new System.Drawing.Size(830, 420);
            this.cmnuOptions.ResumeLayout(false);
            this.cmnuOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem addColumnHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
