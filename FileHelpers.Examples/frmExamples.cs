using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Examples;
using ExamplesFramework.Properties;

using FileHelpers;


namespace ExamplesFramework
{
    /// <summary>
    /// Display all the sample code and allow it to be run in realtime
    /// </summary>
    public partial class frmExamples : Form
    {
        /// <summary>
        /// Where data will be written and expected to be read by the samples
        /// </summary>
        public const string InputFilename = "Input.txt";

        /// <summary>
        /// Where samples will write output to be read by the application and shown up
        /// </summary>
        public const string OutputFilename = "Output.txt";


        public frmExamples()
        {
            InitializeComponent();
            this.InfoSheet.Visible = false;
            this.InfoSheet.Location = new System.Drawing.Point(7, 49);
            this.InfoSheet.BringToFront();

#if ! DEBUG
            this.extracthtml.Visible = false;
#endif
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tvExamples.LoadDemos(ExampleFactory.GetExamples());
        }

        private void tvExamples_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (CurrentExample == null)
            {
                exampleRenderer.Clear();
                return;
            }

            exampleRenderer.Example = CurrentExample;
            if (CurrentExample.AutoRun)
                CurrentExample.RunExample();
        }


        public ExampleCode CurrentExample
        {
            get
            {
                return tvExamples.SelectedExample;
            }
        }

        private void extracthtml_Click(object sender, EventArgs e)
        {
            tvExamples.ProcessDocumentation();
        }

        private void cmdHistory_Click(object sender, EventArgs e)
        {
            InfoSheet.DocumentText = Resources.history;
            InfoSheet.Show();
        }

        private void frmExamples_Load(object sender, EventArgs e)
        {
            cboSearchMode.SelectedIndex = 1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            tvExamples.SearchText = txtSearch.Text;
        }

        private void cboSearchMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboSearchMode.SelectedIndex)
            {
                case 0:
                    tvExamples.SearchMode = ExamplesSearchMode.Name;
                    break;
                case 1:
                    tvExamples.SearchMode = ExamplesSearchMode.NameDescription;
                    break;
                case 2:
                    tvExamples.SearchMode = ExamplesSearchMode.All;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            tvExamples.CollapseAll();
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            tvExamples.ExpandAll();
        }

    }
}
