using System;
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

        private void txtData_Click(object sender, EventArgs e) {}
    }
}