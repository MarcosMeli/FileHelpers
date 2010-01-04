#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Text;

namespace FileHelpers
{
    internal sealed class ForwardReader
        : IDisposable
    {
        //internal static readonly char[] mEOF = StringHelper.NewLine.ToCharArray();

        readonly IRecordReader mReader;

        private readonly string[] mFowardStrings;
        private int mForwardIndex = 0;

        internal int mCapacityHint = 64;

        #region "  Constructors  "


        //internal ForwardReader(IRecordReader reader)
        //    : this(reader, 0, 0)
        //{ }

        internal ForwardReader(IRecordReader reader, int forwardLines)
            : this(reader, forwardLines, 0)
        { }

        internal ForwardReader(IRecordReader reader, int forwardLines, int startLine) 
        {
            mReader = reader;

            mFowardLines = forwardLines;
            mLineNumber = startLine;

            mFowardStrings = new string[mFowardLines + 1];
            mRemaingLines = mFowardLines + 1;

            for (int i = 0; i < mFowardLines + 1; i++)
            {
                mFowardStrings[i] = mReader.ReadRecord();
                mLineNumber++;
                if (mFowardStrings[i] == null)
                {
                    mRemaingLines = i;
                    break;
                }
            }

        }


        #endregion

        #region "  RemainingLines  "

        private int mRemaingLines = 0;

        public int RemainingLines
        {
            get { return mRemaingLines; }
        }

        #endregion

        #region "  LineNumber  "

        private int mLineNumber = 0;

        public int LineNumber
        {
            get { return mLineNumber - 1 - mFowardLines; }
        }

        #endregion

        //		
        //		int mPos = 0;
        //		int MaxRecordSize = 1024 * 8;
        //		char[] mBuffer;
        //	


        #region "  DiscardForward  "

        private bool mDiscardForward = false;

        public bool DiscardForward
        {
            get { return mDiscardForward; }
            set { mDiscardForward = value; }
        }

        #endregion

        #region "  FowardLines  "

        private readonly int mFowardLines = 0;

        public int FowardLines
        {
            get { return mFowardLines; }
        }

        #endregion

        #region "  ReadNextLine  "

        public string ReadNextLine()
        {
            if (mRemaingLines <= 0)
                return null;
            else
            {
                string res = mFowardStrings[mForwardIndex];

                if (mRemaingLines == (mFowardLines + 1))
                {
                    mFowardStrings[mForwardIndex] = mReader.ReadRecord();
                    mLineNumber++;

                    if (mFowardStrings[mForwardIndex] == null)
                    {
                        mRemaingLines--;
                    }

                }
                else
                {
                    mRemaingLines--;
                    if (mDiscardForward)
                        return null;

                }

                mForwardIndex = (mForwardIndex + 1) % (mFowardLines + 1);

                return res;
            }

        }

        #endregion

        #region "  RemainingText  "

        public string RemainingText
        {
            get
            {
                StringBuilder sb = new StringBuilder(100);

                for (int i = 0; i < mRemaingLines + 1; i++)
                {
                    sb.Append(mFowardStrings[(mForwardIndex + i) % (mFowardLines + 1)] + StringHelper.NewLine);
                }

                return sb.ToString();
            }
        }

        #endregion

        #region "  Close  "

        public void Close()
        {
            if (mReader != null)
                mReader.Close();
        }

        #endregion

       
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
