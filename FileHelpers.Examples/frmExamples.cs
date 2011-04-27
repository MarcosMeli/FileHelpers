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
    public partial class frmExamples : Form
    {
        public frmExamples()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            examplesContainer.LoadDemos(ExampleFactory.GetExamples());
        }

    

    }
}
