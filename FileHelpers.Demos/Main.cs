using System;
using FileHelpers;

namespace FileHelpersSamples
{
	public class MainClass
	{
		
		public static void Main()
		{

            //if (RegConfig.GetStringValue("DemoShowDonate", "1") == "1")
            //{
            //    frmDonate frmDon = new frmDonate();
            //    frmDon.ShowDialog();
            //    frmDon.Dispose();
            //}

			frmSamples frm = new frmSamples();
			frm.ShowDialog();
			frm.Dispose();
		}

		public const string GlobalTestFile = @"..\Customers.txt";
		public const string GlobalTestMdb = @"..\TestData.mdb";

	}
}