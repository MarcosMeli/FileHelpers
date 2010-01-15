using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers.SamplesDashboard
{
    
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSamples());
        }

    }
}
