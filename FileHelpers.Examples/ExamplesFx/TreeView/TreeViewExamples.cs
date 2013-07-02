using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExamplesFx.TreeView
{
    
    public class TreeViewExamples
        : TreeViewEx
    {


        /// <summary>
        /// Load all the exampless into the Tree View control on the LHS
        /// </summary>
        /// <param name="examples">List of examples created by ExamplesGenerator </param>
        public void LoadExamples(IEnumerable<ExampleCode> examples)
        {
            mCategories = new Dictionary<string, CategoryTreeNode>();
            foreach (var example in examples)
            {
                var cat = LeafCategory(example);

                var exampleNode = new ExampleTreeNode(example);

                cat.Nodes.Add(exampleNode);
            }

            this.ExpandAll();
        }

        /// <summary>
        /// List of categories
        /// </summary>
        Dictionary<string, CategoryTreeNode> mCategories;

        /// <summary>
        /// Create a leaf categories supplied in the example code
        /// </summary>
        /// <param name="example">Example to create </param>
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
        /// Example selected by user on the treeview
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
            var res = new StringBuilder();
            int newIndent = 1;

            string Heading;
            string Footing;

            GetHeadAndFoot(out Heading, out Footing);
            res.AppendLine(Heading);
            foreach (var node in this.Nodes)
            {
                if (node is IHtmlWriter)
                    ((IHtmlWriter)node).OutputHtml(res, newIndent);
            }
            res.AppendLine(Footing);

            string filename = Path.Combine(HtmlWrapper.DocsOutput, "example_index.html");
            using (var writer = new StreamWriter(filename))
            {
                writer.Write(res);
            }
        }

        private static void GetHeadAndFoot(out string Heading, out string Footing)
        {
                string path = Path.Combine(HtmlWrapper.Docs, "example_index_template.html");
                using (var reader = new StreamReader(path))
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
