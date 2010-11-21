using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers
{

    public interface IDemo
    {
        void Run();
    }

    public sealed class DemoCode
    {
        public DemoCode(IDemo demo, string codeTitle, string category)
        {
            CodeTitle = codeTitle;
            Demo = demo;
            Category = category;
            Files = new List<DemoFile>();
        }

        public IDemo Demo { get; private set; }
        public string CodeTitle { get; private set; }
        public string CodeDescription { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Can be of the form "Async/Delimited" with multiple categories
        /// </summary>
        public string Category { get; set; }

        public List<DemoFile> Files { get; set; }
        public DemoFile LastFile
        {
            get
            {
                if (Files.Count == 0)
                    return null;
                else
                    return Files[Files.Count - 1];
            }
        }

        public UserControl Control  { get; set; }

    }
}
