using System;
using System.Windows.Forms;

namespace FileHelpers.WizardApp
{
    public class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string destFile = null;

            if (args.Length > 1)
                destFile = args[0];

            Application.EnableVisualStyles();

            frmWizard frm = new frmWizard();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}