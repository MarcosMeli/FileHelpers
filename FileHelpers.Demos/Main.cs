using System;
using FileHelpers;

namespace FileHelpersSamples
{
	public class MainClass
	{
		
		public static void Main()
		{

            // No ask anymore for donations :P for now !!
            //if (RegConfig.GetStringValue("DemoShowDonate", "1") == "1")
            //{
            //    frmDonate frmDon = new frmDonate();
            //    frmDon.ShowDialog();
            //    frmDon.Dispose();
            //}

            // Show the menu screen, allows the user to navigate to
            // what they want to see.
			frmSamples frm = new frmSamples();
			frm.ShowDialog();
			frm.Dispose();
		}

        /// <summary>
        /// Customers test file
        /// </summary>
		//public const string GlobalTestFile = @"Customers.txt";
        /// <summary>
        /// Database file for SQL connections
        /// </summary>
		public const string GlobalTestMdb = @"..\TestData.mdb";

	}
}