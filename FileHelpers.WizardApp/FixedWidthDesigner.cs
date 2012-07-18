using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FileHelpers.WizardApp
{
    [Designer(typeof(FixedWidthDesigner.FileBrowserDesigner))]
    public partial class FixedWidthDesigner : UserControl
    {

        private class FileBrowserDesigner : ControlDesigner
        {
            protected override void PostFilterProperties(System.Collections.IDictionary properties)
            {
                base.PostFilterProperties(properties);

                properties.Remove("Font");
            }
        }

        public void AddColumn(ColumnInfo column)
        {
        }

        internal void RecalculatePositions()
        {
            Graphics g = this.CreateGraphics();

            CalculateCharWidth(g);

            int x = mTextLeft;

            foreach (ColumnInfo column in mColumns)
            {
                column.mFileBrowser = this;
                column.mControlLeft = x;
                column.mControlWith = column.Width * mCharWidth;
                x += column.mControlWith;
            }
        }

        private ToolStripMenuItem cmdShowInfo;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmdDeleteColumn;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripTextBox txtColumnName;

        int mTextTop = 25;

        private int mFontSize = 16;

        public int FontSize
        {
            get { return mFontSize; }
            set
            {
                mFontSize = value;
                mCharWidth = -1;
                this.Font = new System.Drawing.Font("Courier New", mFontSize, System.Drawing.FontStyle.Regular);
                this.Invalidate();
            }
        }

        public int TextTop
        {
            get { return mTextTop; }
            set
            {
                mTextTop = value;
                this.Invalidate();
            }
        }
        int mTextLeft = 10;

        public int TextLeft
        {
            get { return mTextLeft; }
            set
            {
                mTextLeft = value;
                this.Invalidate();
            }
        }

        private List<ColumnInfo> mColumns = new List<ColumnInfo>();

        public List<ColumnInfo> Columns
        {
            get { return mColumns; }
        }

        private Color ColorEvenRule = Color.DarkOrange;
        private Color ColorOddRule = Color.RoyalBlue;
        private Color ColorOverRule = Color.Black;
        private Color ColorOverColumn = Color.LightGoldenrodYellow;

        private Pen PenEvenRule;
        private Pen PenOddRule;
        private Pen PenOverRule;

        int mCharWidth = -1;
        private ContextMenuStrip cmnuOptions;
        int mOverColumn = -1;

        protected override void OnPaint(PaintEventArgs e)
        {

            CalculateCharWidth(e.Graphics);

            int width;
            int left = mTextLeft;
            int rulesTop = mTextTop - 2;
            int rulesNumberTop = rulesTop - 15;

            bool even = true;

            for (int i = 0; i < mColumns.Count; i++)
            {
                width = mCharWidth * mColumns[i].Width;

                Brush backBrush;

                if (i == mOverColumn)
                    backBrush = new SolidBrush(ColorOverColumn);
                else
                    backBrush = new SolidBrush(mColumns[i].Color);

                e.Graphics.FillRectangle(backBrush, left, 0, width, this.Height - 1);
                backBrush.Dispose();

                Pen rulePen;

                rulePen = even ? PenEvenRule : PenOddRule;
                even = !even;

                if (i == mOverColumn)
                    rulePen = PenOverRule;

                e.Graphics.DrawLine(rulePen, left + 1, rulesTop, left + width - 1, rulesTop);

                Brush widthBrush;

                if (i == mOverColumn)
                    widthBrush = new SolidBrush(Color.Black);
                else
                    widthBrush = Brushes.DarkRed;

                e.Graphics.DrawString(mColumns[i].Width.ToString(), this.Font, widthBrush, left + width / 2 - 10, rulesNumberTop);

                left += width;
            }

            Brush b = new SolidBrush(this.ForeColor);
            e.Graphics.DrawString(Text, this.Font, b, mTextLeft, mTextTop);
            b.Dispose();

            int closer = CalculateCloser(Control.MousePosition.X);
            if (closer >= 0)
            {
                ColumnInfo col = mColumns[closer];

                if (closer > 0)
                {
                    if (col.CloserToLeft(Control.MousePosition.X))
                        col = mColumns[closer - 1];
                }
                Pen p = new Pen(Color.Red, 3);
                e.Graphics.DrawLine(p, col.mControlLeft + col.mControlWith, 0, col.mControlLeft + col.mControlWith, this.Height);
                p.Dispose();
            }



        }

        private void CalculateCharWidth(Graphics g)
        {
            if (mCharWidth == -1)
                mCharWidth = (int)TextRenderer.MeasureText("m", this.Font).Width - 4;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mCloserColumn != null)
            {
                if (mCloserToLeft)
                {
                    if (mCloserLeftColumn == null) return;

                    int ant = mCloserLeftColumn.Width;
                    int newWidth = mOriginalLeftWidth + ((e.X - mCloserInitialX) / mCharWidth);
                    if (newWidth < 1) newWidth = 1;
                    mCloserLeftColumn.Width = newWidth;
                    if (ant != mCloserLeftColumn.Width)
                        this.Invalidate();
                }
                else
                {
                    int ant = mCloserColumn.Width;
                    int newWidth = mOriginalWidth + ((e.X - mCloserInitialX) / mCharWidth);
                    if (newWidth < 1) newWidth = 1;
                    mCloserColumn.Width = newWidth;
                    if (ant != mCloserColumn.Width)
                        this.Invalidate();
                }
            }
            else
            {
                int oldCol = mOverColumn;
                mOverColumn = CalculateColumn(e.X);

                int oldClose = mCloserEdge;
                mCloserEdge = CalculateCloser(e.X);

                if (mCloserEdge > 0 && mColumns[mCloserEdge].CloserToLeft(e.X))
                    mCloserEdge--;

                if (oldCol != mOverColumn || oldClose != mCloserEdge)
                    this.Invalidate();
            }
        }

        int mCloserEdge;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            int closer = CalculateCloser(e.X);
            mCloserColumn = mColumns[closer];

            if (e.Button == MouseButtons.Right)
            {
                mPosMouseDown = e.Location;
                cmnuOptions.Show(this, e.Location);
                txtColumnName.Text = mCloserColumn.Name;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (closer > 0)
                {
                    mCloserLeftColumn = mColumns[closer - 1];
                    mOriginalLeftWidth = mCloserLeftColumn.Width;
                }

                mCloserToLeft = mCloserColumn.CloserToLeft(e.X);
                mCloserInitialX = e.X;
                mOriginalWidth = mCloserColumn.Width;
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mCloserColumn = null;
            mCloserLeftColumn = null;
            this.Invalidate();
        }

        private int CalculateCloser(int x)
        {
            int res = -1;
            int dist = int.MaxValue;

            for (int i = 0; i < mColumns.Count; i++)
            {
                int currDist = mColumns[i].CalculateDistance(x);

                if (currDist < dist)
                {
                    dist = currDist;
                    res = i;
                }
            }
            return res;
        }

        ColumnInfo mCloserColumn = null;
        bool mCloserToLeft = false;
        int mCloserInitialX = -1;
        int mOriginalWidth = -1;
        int mOriginalLeftWidth = -1;
        ColumnInfo mCloserLeftColumn = null;
        private Point mPosMouseDown;

        private int CalculateColumn(int x)
        {
            if (x < mTextLeft)
                return -1;

            for (int i = 0; i < mColumns.Count; i++)
            {
                if (mColumns[i].ContainsPoint(x))
                    return i;
            }

            return -1;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            mCharWidth = -1;
        }


        public FixedWidthDesigner()
        {
            InitializeComponent();

            mColumns.AddRange(new ColumnInfo[] 
                { 
                    new ColumnInfo(11), 
                    new ColumnInfo(38), 
                    new ColumnInfo(72-50),
                    new ColumnInfo(110-72), 
                    new ColumnInfo(151-110), 
                    new ColumnInfo(169-151),
                    new ColumnInfo(15)
                });

            PenEvenRule = new Pen(ColorEvenRule);
            PenOddRule = new Pen(ColorOddRule);
            PenOverRule = new Pen(ColorOverRule);

            this.Font = new System.Drawing.Font("Courier New", mFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.VerticalScroll.Enabled = true;
            this.DoubleBuffered = true;

            RecalculatePositions();

        }

        private void cmdDeleteColumn_Click(object sender, EventArgs e)
        {
            int closer = CalculateCloser(MousePosition.X);
            if (closer == -1) return;

            mColumns.RemoveAt(closer);
            RecalculatePositions();
            this.Invalidate();

        }

        private void addColumnHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ColumnInfo colAnte = null;
            //foreach (var col in Columns.ToArray())
            //{
            //    if (col.ContainsPoint(mPosMouseDown.X))
            //    {
            //        Columns.Insert(Columns.IndexOf(col), new ColumnInfo());
            //    }
            //    mPosMouseDown    
            //}
            
        }


    }


    public class ColumnInfo
    {
        internal FixedWidthDesigner mFileBrowser;

        private int mWidth;

        public int Width
        {
            get { return mWidth; }
            set
            {
                if (mWidth == value)
                    return;

                mWidth = value;
                if (mFileBrowser != null)
                    mFileBrowser.RecalculatePositions();
            }
        }

        private string mName = string.Empty;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        private Color mColor;

        public Color Color
        {
            get { return mColor; }
            set { mColor = value; }
        }

        public static bool even = false;

        public ColumnInfo()
        {
            if (even)
                Color = Color.AliceBlue;
            else
                Color = Color.White;
            even = !even;
        }

        public ColumnInfo(int width)
            : this()
        {
            this.Width = width;
        }

        internal int mControlLeft;
        internal int mControlWith;

        internal bool ContainsPoint(int x)
        {
            return x >= mControlLeft && x < mControlLeft + mControlWith;
        }

        internal int CalculateDistance(int x)
        {
            return Math.Min(Math.Abs(mControlLeft - x), Math.Abs((mControlWith + mControlLeft) - x));
        }


        internal bool CloserToLeft(int x)
        {
            if (Math.Abs(mControlLeft - x) < Math.Abs((mControlWith + mControlLeft) - x))
                return true;
            else
                return false;
        }
    }
}

