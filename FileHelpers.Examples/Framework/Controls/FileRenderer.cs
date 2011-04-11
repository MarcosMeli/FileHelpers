using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace ExamplesFramework
{
    public partial class FileRenderer : UserControl
    {
        public ExampleFile File { get; set; }

        public FileRenderer()
        {
            InitializeComponent();
            ColorTop = Color.RoyalBlue;
            ColorBotton = Color.Navy;
        }

         public FileRenderer(ExampleFile file)
             :this()
         {
             File = file;
             RenderFile();
         }

        private void RenderFile()
        {
            //if (demo.Status == ExampleFile.FileType.HtmlFile)
            //{
            //    //  Hide the code editor
            //    txtCode.Visible = false;
            //    this.InfoSheet.Visible = true;

            //    HtmlWrapper html = new HtmlWrapper(demo.Contents, CurrentExample.Files);
            //    string text = html.ToString();
            //    this.InfoSheet.DocumentText = text;
            //    int retries = 0;
            //    while (this.InfoSheet.DocumentText != text && retries < 10)
            //    {
            //        System.Threading.Thread.Sleep(200);
            //        retries++;
            //    }
            //    this.InfoSheet.Invalidate();
            //    return;
            //}

            //this.InfoSheet.Visible = false;
            //this.txtCode.Visible = true;
            lblFileName.Text = File.Filename;
            txtCode.IsReadOnly = true;
            var doc = new ICSharpCode.TextEditor.Document.DocumentFactory();
            var doc2 = doc.CreateDocument();
            doc2.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            doc2.TextContent = File.Contents;
            doc2.ReadOnly = true;
            txtCode.Document = doc2;
            txtCode.Refresh();
        }

        #region "  Label Paint  "

        private void lblFileName_Paint(object sender, PaintEventArgs e)
        {
            using (var gradient = new LinearGradientBrush(lblFileName.ClientRectangle, ColorTop, ColorBotton, LinearGradientMode.Vertical))
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                e.Graphics.FillRectangle(gradient, e.ClipRectangle);
                e.Graphics.DrawString(lblFileName.Text, lblFileName.Font, Brushes.WhiteSmoke, 10, 4);
            }
        }

        private Color mColorTop;
        public Color ColorTop
        {
            get { return mColorTop; }
            set
            {
                if (mColorTop == value)
                    return;

                mColorTop = value;
                lblFileName.Invalidate();
            }
        }

        private Color mColorBotton;
        public Color ColorBotton
        {
            get { return mColorBotton; }
            set
            {
                if (mColorBotton == value)
                    return;
                mColorBotton = value;
                lblFileName.Invalidate();
            }
        }

        #endregion

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCode.Text, TextDataFormat.Text);
        }
    }
}
