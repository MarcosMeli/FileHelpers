using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers
{
    
    public class TreeViewDemos
        : TreeViewEx
    {


        public void LoadDemos(IEnumerable<DemoCode> demos)
        {
            mCategories = new Dictionary<string, CategoryTreeNode>();
            foreach (var demo in demos)
            {
                var cat = LeaftCategory(demo);

                var demoNode = new DemoTreeNode(demo);
                cat.Nodes.Add(demoNode);
            }
        }

        Dictionary<string, CategoryTreeNode> mCategories;

        private CategoryTreeNode LeaftCategory(DemoCode demo)
        {
            var categories = demo.Category.Split('/');
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
                }
               
                previous = categNode;
            }

            return previous;
        }
    }

        public class DemoTreeNode
        : TreeNode
        {
            public DemoTreeNode(DemoCode demo)
                : base(demo.CodeTitle)
            {
                this.Demo = demo;
                this.SelectedImageKey = "folder";
                this.ImageKey = "folder";
            }

            public DemoCode Demo { get; set; }

        }

        public class CategoryTreeNode
            : TreeNode
        {
            public CategoryTreeNode(string text)
                : base(text)
            {
                this.SelectedImageKey = "demo";
                this.ImageKey = "demo";
            }

            public DemoCode Demo { get; set; }

        }

}
