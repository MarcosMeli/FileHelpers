using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers.WizardApp
{
    public partial class frmDataFile : Form
    {
        public frmDataFile()
        {
            InitializeComponent();
        }

        public static void ShowData(String txt)
        {
            frmDataFile frm = new frmDataFile();

            frm.txtData.Text = txt;
            frm.ShowDialog();
        }

        private void txtData_Click(object sender, EventArgs e)
        {

        }
    }
}