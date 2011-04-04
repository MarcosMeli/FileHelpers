using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamplesFramework
{
    /// <summary>
    /// Enable object to output itself as HTML code
    /// </summary>
    public interface IHTMLwriter
    {
        /// <summary>
        /// Generate the HTML for the index details to the
        /// string builder and optionally write any
        /// other html output files
        /// </summary>
        /// <param name="index">html version of the index</param>
        void OutputHtml(StringBuilder index, int indent);
    }
}
