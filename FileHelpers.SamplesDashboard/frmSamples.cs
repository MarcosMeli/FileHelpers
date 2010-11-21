using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace FileHelpers.SamplesDashboard
{
    public partial class frmSamples : Form
    {
        public frmSamples()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            treeViewDemos1.LoadDemos(DemoFactory.GetDemos());
        }

        private void treeViewDemos1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var demo = treeViewDemos1.SelectedDemo;
            if (demo == null)
            {
                Clear();
                return;
            }

            ShowDemo(demo);

        }

        private void ShowDemo(DemoCode demo)
        {
            this.SuspendLayout();
            tcCodeFiles.TabPages.Clear();

            foreach (var file in demo.Files)
            {
                var tp = new TabPage();
                tp.Text = file.Filename;
                tp.Tag = file;
                tcCodeFiles.TabPages.Add(tp);

                //if (tcCodeFiles.TabPages.Count == 1)
                //    tcCodeFiles.SelectedTab = tp;
            }

            ShowDemoFile();
            this.ResumeLayout();
        }

        private void Clear()
        {
            
        }

        public void ShowDemoFile()
        {
            if (tcCodeFiles.SelectedTab == null)
                return;

            var demo = tcCodeFiles.SelectedTab.Tag as DemoFile;
            if (demo == null)
                return;

            txtCode.IsReadOnly = true;
            var doc = new ICSharpCode.TextEditor.Document.DocumentFactory();
            var doc2 = doc.CreateDocument();
            doc2.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            doc2.TextContent = demo.Contents;
            txtCode.Document = doc2;

            txtCode.Show();
            txtCode.Refresh();

        }
        private void tcCodeFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDemoFile();
        }

    }
}
