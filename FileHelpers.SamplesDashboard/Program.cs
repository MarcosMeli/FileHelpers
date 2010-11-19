using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers.SamplesDashboard
{
    
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var demos = new StringBuilder();
            demos.AppendLine("var demos = new List<DemoCode>();\r\n");
                

            demos.AppendLine(
                ExampleParser.Parse(
                    File.ReadAllText(
                        @"d:\Desarrollo\Devoo\FileHelpers\trunk\FileHelpers.SamplesDashboard\Core\Basic\ReadFileDelimited.cs")));


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSamples());
        }

    }
}
