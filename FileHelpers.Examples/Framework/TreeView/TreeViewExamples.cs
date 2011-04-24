using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExamplesFramework
{
    
    public class TreeViewExamples
        : TreeViewEx
    {

        public TreeViewExamples()
        {
         
        }

        /// <summary>
        /// Load all the demos into the Tree View control on the LHS
        /// </summary>
        /// <param name="demos">List of demos created by DemoGenerator </param>
        public void LoadDemos(IEnumerable<ExampleCode> demos)
        {
            mCategories = new Dictionary<string, CategoryTreeNode>();
            foreach (var demo in demos)
            {
                var cat = LeafCategory(demo);

                var demoNode = new ExampleTreeNode(demo);
                cat.Nodes.Add(demoNode);
            }

            this.ExpandAll();
        }

        /// <summary>
        /// List of categories
        /// </summary>
        Dictionary<string, CategoryTreeNode> mCategories;

        /// <summary>
        /// Create a leaf categories supplied in the demo code
        /// </summary>
        /// <param name="example">Demo to create </param>
        /// <returns>Categorynode</returns>
        private CategoryTreeNode LeafCategory(ExampleCode example)
        {
            var categories = example.Category.Split('/');
            CategoryTreeNode previous = null;
            foreach (var categ in categories)
            {
                CategoryTreeNode categNode;
                if (!mCategories.TryGetValue(categ, out categNode))
                {
                    categNode = new CategoryTreeNode(categ);
                    if (previous == null)
                        this.Nodes.Add(categNode);
                    else
                        previous.Nodes.Add(categNode);

                    mCategories.Add(categ, categNode);
                }
               
                previous = categNode;
            }

            return previous;
        }

        /// <summary>
        /// Demo selected by user on the treeview
        /// </summary>
        public ExampleCode SelectedExample
        {
            get
            {
                var node = this.SelectedNode as ExampleTreeNode;
                if (node == null)
                    return null;
                
                return node.Example;
            }
        }

        public void ProcessDocumentation()
        {
            StringBuilder index = new StringBuilder();
            int newIndent = 1;

            string Heading;
                string Footing;

                GetHeadAndFoot(out Heading, out Footing);
            index.AppendLine(Heading);
            foreach (var node in this.Nodes)
            {
                if (node is IHtmlWriter)
                    ((IHtmlWriter)node).OutputHtml(index, newIndent);
            }
            index.AppendLine(Footing);

            string filename = Path.Combine(HtmlWrapper.DocsOutput, "example_index.html");
            using (StreamWriter writer = new System.IO.StreamWriter(filename))
            {
                writer.Write(index);
            }
        }

        private static void GetHeadAndFoot(out string Heading, out string Footing)
        {
                string path = Path.Combine(HtmlWrapper.Docs, "example_index_template.html");
                using (StreamReader reader = new StreamReader(path))
                {
                    string[] temp = { "${BODY}" };
                    string[] parts = reader.ReadToEnd().Split(temp, StringSplitOptions.None);
                    reader.Close();

                    if (parts.Length != 2)
                    {
                        throw new Exception("There must be one, and only one ${BODY} in " + path + " found " + parts.Length);
                    }
                    Heading = parts[0];
                    Footing = parts[1];
                }
            }
    }
}
