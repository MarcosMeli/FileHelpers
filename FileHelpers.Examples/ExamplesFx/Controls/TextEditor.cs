using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace ExamplesFx.Controls
{
    public partial class TextEditor : UserControl
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        DocumentFactory factory = new ICSharpCode.TextEditor.Document.DocumentFactory();

        public override string Text
        {
            get { return txtCode.Text; }
            set
            {
                if (txtCode.Text == value)
                    return;
                
                txtCode.IsReadOnly = true;
                var doc2 = factory.CreateDocument();
                doc2.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                doc2.TextContent = value;
                doc2.ReadOnly = true;
                txtCode.Document = doc2;
                txtCode.Refresh();
            }
        }
    }
}
