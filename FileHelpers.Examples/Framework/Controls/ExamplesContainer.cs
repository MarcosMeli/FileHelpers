using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExamplesFramework.Framework.Controls
{
    public partial class ExamplesContainer : UserControl
    {
        public ExamplesContainer()
        {
            InitializeComponent();
        }

        public void LoadDemos(IEnumerable<ExampleCode> demos)
        {
            tvExamples.LoadDemos(demos);
        }

        public void ShowCustomHtml(string html)
        {
           exampleRenderer.ShowCustomHtml(html);
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            tvExamples.CollapseAll();
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            tvExamples.ExpandAll();
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

        private void ExamplesContainer_Load(object sender, EventArgs e)
        {
            cboSearchMode.SelectedIndex = 1;
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

    }
}
