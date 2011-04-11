using System;
using System.Linq;
using System.Windows.Forms;

namespace ExamplesFramework
{
    /// <summary>
    /// Create a demo code container
    /// </summary>
    public class ExampleTreeNode
        : TreeNode, IHTMLwriter, ISearchableNode
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
        public void OutputHtml(System.Text.StringBuilder index, int indent)
        {
            bool error = false;
            Exception MyException = null;
            try
            {
                if (!Example.TestRun)
                    Example.Test();
            }
            catch (Exception ex)
            {
                error = true;
                MyException = ex;
            }
            bool found = false;
            foreach (ExampleFile file in Example.Files.Where(x => x.Status == ExampleFile.FileType.HtmlFile))
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

                    HtmlWrapper wrapper = new HtmlWrapper(file.Contents, Example.Files);
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

        public string GetDescriptionExtra()
        {
            return this.Example.Description;
        }
    }
}
