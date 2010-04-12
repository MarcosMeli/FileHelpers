using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

            //treeViewDemos1.LoadDemos(DemoFactory.GetDemos());
        }
    }
}
