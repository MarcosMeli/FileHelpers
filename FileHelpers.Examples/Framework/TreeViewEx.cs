using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ExamplesFramework
{
    // Quick Search, right clicking, menu, etc
    public class TreeViewEx
        : TreeView
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
    }

}
