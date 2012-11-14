using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExamplesFx.TreeView
{
    /// <summary>
    /// Create a demo code container
    /// </summary>
    public class ExampleTreeNode
        : TreeNode, IHtmlWriter, ISearchableNode
    {
        /// <summary>
        /// Create a demo tree node with text based on Name
        /// </summary>
        /// <param name="example">DemoCode value to base node upon</param>
        public ExampleTreeNode(ExampleCode example)
            : base(example.Name)
        {
            this.Example = example;
            this.SelectedImageKey = "folder";
            this.ImageKey = "folder";
        }

        public override object Clone()
        {
            return new ExampleTreeNode(Example);
        }

        /// <summary>
        /// demo to create detail from
        /// </summary>
        public ExampleCode Example { get; set; }


        /// <summary>
        /// Output HTML,  in this case a heading
        /// </summary>
        /// <param name="index"></param>
        public void OutputHtml(StringBuilder index, int indent)
        {
            bool error = false;
            Exception MyException = null;
            try
            {
                Example.RunExample();
            }
            catch (Exception ex)
            {
                error = true;
                MyException = ex;
            }
            bool found = false;
            foreach (var file in Example.Files.Where(x => x.Status == ExampleFile.FileType.HtmlFile))
            {
                //  overview files should come first and be on their own...
                //  they are intended to display on the screen and be
                //  embedded into the index at the appropriate spot
                if (file.Filename.ToLower() == "overview.html")
                {
                    index.AppendLine("<li>");
                    index.AppendLine(file.Contents);
                    index.AppendLine("</li>");
                }
                else
                {
                    index.Append("<dt><a href=\"");
                    index.Append(file.Filename);
                    index.Append("\">");
                    index.Append(Example.Name);
                    index.AppendLine("</a></dt>");
                    index.Append("<dd>");
                    if (error)
                    {
                        index.Append("<p>Error: ");
                        index.Append(MyException.ToString());
                        index.Append("</p>");
                    }
                    index.Append(Example.Description);
                    index.AppendLine("</dd>");

                    var wrapper = new HtmlWrapper(file.Contents, Example.Files);
                    wrapper.Export(file.Filename);
                    found = true;
                }
            }

            if (!found)
            {
                index.Append("<dt><u>Missing</u> ");
                index.Append(Example.Name);
                index.AppendLine("</dt>");
                index.Append("<dd>");
                index.Append(Example.Description);
                index.AppendLine("</dd>");
            }
        }

        string ISearchableNode.GetName()
        {
            return this.Example.Name;
        }

        string ISearchableNode.GetDescription()
        {
            return this.Example.Description;
        }

        string ISearchableNode.GetDescriptionExtra()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Example.SourceCode);
            foreach (var file in Example.Files)
            {
                sb.AppendLine(file.Contents);
            }
            return sb.ToString();
        }
    }
}
