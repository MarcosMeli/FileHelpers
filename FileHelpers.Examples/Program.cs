using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExamplesFramework
{
    
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmExamples());
        }

    }
}
