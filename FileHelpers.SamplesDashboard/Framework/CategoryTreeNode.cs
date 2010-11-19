using System.Windows.Forms;

namespace FileHelpers
{
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