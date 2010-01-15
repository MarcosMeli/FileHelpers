using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers
{
    
    public class DemoCode
    {
        public DemoCode(string codeTitle, string category)
        {
            CodeTitle = codeTitle;
            Category = category;
        }

        public string CodeTitle { get; set; }
        public string CodeDescription { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// Can be of the form "Async/Delimited" with multiple categories
        /// </summary>
        public string Category { get; set; }
        public DemoFiles[] Files { get; set; }
        public UserControl Control  { get; set; }

    }
}
