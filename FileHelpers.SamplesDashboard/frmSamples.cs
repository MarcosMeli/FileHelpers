using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using Demos;

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
            if (CurrentDemo == null)
            {
                Clear();
                return;
            }

            ShowDemo();

        }

        private void ShowDemo()
        {
            this.SuspendLayout();
            tcCodeFiles.TabPages.Clear();

            foreach (var file in CurrentDemo.Files)
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

        public DemoCode CurrentDemo
        {
            get
            {
                return treeViewDemos1.SelectedDemo;
            }
        }
        private void tcCodeFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDemoFile();
        }

        private void cmdRunDemo_Click(object sender, EventArgs e)
        {
            if (CurrentDemo == null)
                return;

            CurrentDemo.Demo.Run();
        }

    }
}
