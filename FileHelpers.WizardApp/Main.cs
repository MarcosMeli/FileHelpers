using System;
using FileHelpers;
using System.Windows.Forms;

namespace FileHelpers.WizardApp
{
	public class MainClass
	{
        [STAThread]
		public static void Main()
		{
            Application.EnableVisualStyles();

            //if (RegConfig.GetStringValue(frmDonate.WizardDonateRegKey, "1") == "1")
            //{
            //    frmDonate frmDon = new frmDonate();
            //    frmDon.ShowDialog();
            //    frmDon.Dispose();
            //}

            frmWizard frm = new frmWizard();
			frm.ShowDialog();
			frm.Dispose();
		}

	}
}