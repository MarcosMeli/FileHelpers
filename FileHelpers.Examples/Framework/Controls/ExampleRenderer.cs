using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace ExamplesFramework
{
    public partial class ExampleRenderer : UserControl
    {
        public ExampleRenderer()
        {
            InitializeComponent();
        }

        private ExampleCode mExample;
        public ExampleCode Example
        {
            get { return mExample; }
            set
            {
                if (mExample == value)
                    return;

                mExample = value;
                RenderExample();
            }
        }

        private void RenderExample()
        {
            this.SuspendLayout();
            Clear();

            this.lblTestDescription.Text = Example.Description;

            tableLayoutPanel1.RowCount = Example.Files.Count;
            for (int i = 0; i < Example.Files.Count; i++)
            {
                var file = Example.Files[i];
                CreateNewDemoFile(i, file);
            }

            cmdRunDemo.Visible = Example.Runnable;
            this.ResumeLayout();
        }


        public void Clear()
        {
            this.lblTestDescription.Text = string.Empty;
            cmdRunDemo.Visible = false;
        }

        private void cmdRunDemo_Click(object sender, EventArgs e)
        {
            if (Example == null)
                return;
            Example.AddedFile += FileHandler;
            Example.Test();
            Example.AddedFile -= FileHandler;

        }

        private void FileHandler(object sender, ExampleCode.NewFile file)
        {
            CreateNewDemoFile(int.MaxValue, file.details);
        }

        private void CreateNewDemoFile(int i, ExampleFile file)
        {
            var ctrl = new FileRenderer(file);
            ctrl.Dock = DockStyle.Fill;
            if (i > tableLayoutPanel1.RowCount)
            {
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.Controls.Add(ctrl, 0, tableLayoutPanel1.RowCount - 1);
            }
            else
            {
                tableLayoutPanel1.Controls.Add(ctrl, 0, i);
                
            }


        }

    }
}
