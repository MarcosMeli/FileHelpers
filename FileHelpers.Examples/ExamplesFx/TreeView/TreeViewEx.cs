using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace ExamplesFx.TreeView
{
    // Quick Search, right clicking, menu, etc
    public class TreeViewEx
        : System.Windows.Forms.TreeView
    {
        public TreeViewEx()
        {
            DoubleBuffer(true);
        }

        private void DoubleBuffer(bool setting)
        {
            Type dgvType = this.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this, setting, null);
        }

        private string mSearchText;
        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return mSearchText; }
            set
            {
                if (mSearchText == value)
                    return;

                mSearchText = value;
                PerformSearch();

            }
        }


        private ExamplesSearchMode mSearchMode;
        /// <summary>
        /// Gets or sets the search mode.
        /// </summary>
        /// <value>The search mode.</value>
        public ExamplesSearchMode SearchMode
        {
            get { return mSearchMode; }
            set
            {
                if (mSearchMode == value)
                    return;

                mSearchMode = value;
                PerformSearch();
            }
        }

        private bool mFirstSearch = true;

        private List<TreeNode> mAllNodes;
        private void PerformSearch()
        {
            if (mFirstSearch && string.IsNullOrEmpty(SearchText))
                return;

            mFirstSearch = false;
            if (mAllNodes == null)
            {
                mAllNodes = new List<TreeNode>();
                foreach (TreeNode node in this.Nodes)
                {
                    mAllNodes.Add(node);
                    
                }
            }
            

            this.BeginUpdate();
            this.Nodes.Clear();
            var resultNodes = new List<TreeNode>();
            var filtered = SearchInNodes(mAllNodes);

            foreach (var nodeFiltered in filtered)
            {
                this.Nodes.Add(nodeFiltered);
            }

            this.ExpandAll();
            this.EndUpdate();

        }

        private List<TreeNode> SearchInNodes(IEnumerable nodes)
        {
            var res = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Nodes.Count == 0)
                {
                    if (NodeInResult((ISearchableNode) node))
                        res.Add(node);
                }
                else
                {
                    var filtered = SearchInNodes(node.Nodes);
                    if (filtered.Count > 0)
                    {
                        var nodeClone = (TreeNode) node.Clone();

                        foreach (var nodeFiltered in filtered)
                        {
                            nodeClone.Nodes.Add((TreeNode) nodeFiltered.Clone());
                        }
                        res.Add(nodeClone);
                    }
                }
            }
            return res;
        }

        private bool NodeInResult(ISearchableNode node)
        {
            switch (SearchMode)
            {
                case ExamplesSearchMode.Name:
                    return node.GetName().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                case ExamplesSearchMode.NameDescription:
                    return node.GetName().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           node.GetDescription().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                case ExamplesSearchMode.All:
                    var code = node.GetDescriptionExtra();
                    return node.GetName().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           node.GetDescription().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           (code != null && code.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
