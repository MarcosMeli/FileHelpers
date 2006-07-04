using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers.WizardApp
{
    public partial class frmDataPreview : Form
    {
        public frmDataPreview()
        {
            InitializeComponent();
        }

        public static void ShowPreview(DataTable dt)
        {
            frmDataPreview frm = new frmDataPreview();

            frm.dgbPreview.DataSource = dt;
            frm.ShowDialog();
        }
    }
}