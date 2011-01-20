using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ICSharpCode.TextEditor.Document;
using Demos;

namespace FileHelpers.SamplesDashboard
{
    /// <summary>
    /// Display all the sample code and allow it to be run in realtime
    /// </summary>
    public partial class frmSamples : Form
    {
        /// <summary>
        /// Where data will be written and expected to be read by the samples
        /// </summary>
        public const string InputFilename = "Input.txt";

        /// <summary>
        /// Where samples will write output to be read by the application and shown up
        /// </summary>
        public const string OutputFilename = "Output.txt";


        public frmSamples()
        {
            InitializeComponent();
            this.InfoSheet.Visible = false;
            this.splitContainer2.Panel1.Controls.Add(this.InfoSheet);
            this.InfoSheet.Location = new System.Drawing.Point(7, 49);
            this.InfoSheet.BringToFront();
#if ! DEBUG
            this.extracthtml.Visible = false;
#endif
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

            this.TestDescription.Text = CurrentDemo.CodeDescription;

            foreach (var file in CurrentDemo.Files)
            {
                CreateNewDemoFile(file);

                //if (tcCodeFiles.TabPages.Count == 1)
                //    tcCodeFiles.SelectedTab = tp;
            }

            ShowDemoFile();
            this.ResumeLayout();
        }

        private void CreateNewDemoFile(DemoFile file)
        {
            var tp = new TabPage();
            tp.Text = file.Filename;
            tp.Tag = file;
            tcCodeFiles.TabPages.Add(tp);
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

            if (demo.Status == DemoFile.FileType.HtmlFile)
            {
                //  Hide the code editor
                txtCode.Visible = false;
                this.InfoSheet.Visible = true;

                HtmlWrapper html = new HtmlWrapper(demo.Contents, CurrentDemo.Files);
                string text = html.ToString();
                this.InfoSheet.DocumentText = text;
                int retries = 0;
                while (this.InfoSheet.DocumentText != text && retries < 10)
                {
                    System.Threading.Thread.Sleep(200);
                    retries++;
                }
                this.InfoSheet.Invalidate();
                return;
            }
            this.InfoSheet.Visible = false;
            this.txtCode.Visible = true;

            txtCode.IsReadOnly = true;
            var doc = new ICSharpCode.TextEditor.Document.DocumentFactory();
            var doc2 = doc.CreateDocument();
            doc2.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            doc2.TextContent = demo.Contents;
            doc2.ReadOnly = true;
            txtCode.Document = doc2;
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
            CurrentDemo.AddedFile += new EventHandler<DemoCode.NewFile>(FileHandler);
            CurrentDemo.Test();
            CurrentDemo.AddedFile -= FileHandler;
        }

        private void FileHandler( object sender, DemoCode.NewFile file )
        {
            CreateNewDemoFile(file.details);
        }

        private void extracthtml_Click(object sender, EventArgs e)
        {
            treeViewDemos1.ProcessDocumentation();
        }

    }
}
