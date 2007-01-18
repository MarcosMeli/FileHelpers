using System;
using FileHelpers;

namespace FileHelpersSamples
{
	public class MainClass
	{
		public static void Main()
		{
			frmDonate frmDon = new frmDonate();
			frmDon.ShowDialog();
			frmDon.Dispose();

			frmSamples frm = new frmSamples();
			frm.ShowDialog();
			frm.Dispose();
		}


	}
}