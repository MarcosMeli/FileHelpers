using System;
using System.Collections.Generic;
using System.Text;

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

            this.ExpandAll();
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

                    mCategories.Add(categ, categNode);
                }
               
                previous = categNode;
            }

            return previous;
        }

        public DemoCode SelectedDemo
        {
            get
            {
                var node = this.SelectedNode as DemoTreeNode;
                if (node == null)
                    return null;
                
                return node.Demo;
            }
        }
    }
}
