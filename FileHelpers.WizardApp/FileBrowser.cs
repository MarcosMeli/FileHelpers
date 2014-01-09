using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace FileHelpers.WizardApp
{
    [Designer(typeof (FileBrowser.FileBrowserDesigner))]
    public class FileBrowser : ScrollableControl
    {
        private class FileBrowserDesigner : ControlDesigner
        {
            protected override void PostFilterProperties(System.Collections.IDictionary properties)
            {
                base.PostFilterProperties(properties);

                properties.Remove("Font");
            }
        }

        public FileBrowser()
        {
            mColumns.AddRange(new ColumnInfo[] {
                new ColumnInfo(11),
                new ColumnInfo(38),
                new ColumnInfo(72 - 50),
                new ColumnInfo(110 - 72),
                new ColumnInfo(151 - 110),
                new ColumnInfo(169 - 151),
                new ColumnInfo(15)
            });
            PenEvenRule = new Pen(ColorEvenRule);
            PenOddRule = new Pen(ColorOddRule);
            PenOverRule = new Pen(ColorOverRule);

            this.Font = new System.Drawing.Font("Courier New",
                mFontSize,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Pixel,
                ((byte) (0)));
            this.VerticalScroll.Enabled = true;
            this.DoubleBuffered = true;
        }

        private int mTextTop = 25;

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

        private int mTextLeft = 10;

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

        private int mCharWidth = -1;
        private int mOverColumn = -1;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (mCharWidth == -1)
                mCharWidth = (int) TextRenderer.MeasureText("m", this.Font).Width - 5;

            int width;
            int left = mTextLeft;
            int rulesTop = mTextTop - 2;
            int rulesNumberTop = rulesTop - 13;

            bool even = true;

            for (int i = 0; i < mColumns.Count; i++) {
                width = mCharWidth*mColumns[i].Width;

                Brush backBrush;

                if (i == mOverColumn)
                    backBrush = new SolidBrush(ColorOverColumn);
                else
                    backBrush = new SolidBrush(mColumns[i].Color);

                e.Graphics.FillRectangle(backBrush, left, 0, width, this.Height - 1);
                backBrush.Dispose();

                Pen rulePen;

                rulePen = even
                    ? PenEvenRule
                    : PenOddRule;
                even = !even;

                if (i == mOverColumn)
                    rulePen = PenOverRule;

                e.Graphics.DrawLine(rulePen, left + 1, rulesTop, left + width - 1, rulesTop);

                Brush widthBrush;

                if (i == mOverColumn)
                    widthBrush = new SolidBrush(Color.Black);
                else
                    widthBrush = Brushes.DarkRed;

                e.Graphics.DrawString(mColumns[i].Width.ToString(),
                    this.Font,
                    widthBrush,
                    left + width/2 - 10,
                    rulesNumberTop);

                left += width;
            }

            Brush b = new SolidBrush(this.ForeColor);
            e.Graphics.DrawString(Text, this.Font, b, mTextLeft, mTextTop);
            b.Dispose();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int oldCol = mOverColumn;
            mOverColumn = CalculateColumn(e.X);

            if (oldCol != mOverColumn)
                this.Invalidate();
        }


        private int CalculateColumn(int x)
        {
            if (x < mTextLeft)
                return -1;

            int left = mTextLeft;

            for (int i = 0; i < mColumns.Count; i++) {
                left += mCharWidth*mColumns[i].Width;
                if (x < left)
                    return i;
            }

            return -1;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            mCharWidth = -1;
        }

        public class ColumnInfo
        {
            private int mWidth;

            public int Width
            {
                get { return mWidth; }
                set { mWidth = value; }
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
        }
    }
}