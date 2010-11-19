using System.Windows.Forms;

namespace FileHelpers
{
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
}