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

        /// <summary>
        /// Demo class that runs
        /// </summary>
        public IDemo Demo { get; private set; }

        /// <summary>
        /// Title set from code
        /// </summary>
        public string CodeTitle { get; private set; }

        /// <summary>
        /// Description set from code
        /// </summary>
        public string CodeDescription { get; set; }

        /// <summary>
        /// Code from file.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Can be of the form "Async/Delimited" with multiple categories
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// List of logical files extracted from the code
        /// </summary>
        public List<DemoFile> Files { get; set; }

        /// <summary>
        /// last file extracted, generally sample data
        /// </summary>
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

        /// <summary>
        /// Control???
        /// </summary>
        public UserControl Control  { get; set; }

    }
}
