using System;
using System.Text;
using FileHelpers.Helpers;

namespace FileHelpers
{
    internal sealed class ForwardReader
        : IDisposable
    {
        /// <summary>
        /// Return file record by record
        /// </summary>
        private readonly IRecordReader mReader;

        /// <summary>
        /// records already read
        /// </summary>
        private readonly string[] mFowardStrings;

        private int mForwardIndex;

        #region "  Constructors  "

        /// <summary>
        /// Read a Record handler forward,  optionally skipping n lines and starting at a record number > 0
        /// </summary>
        /// <param name="reader">Reader to get records</param>
        /// <param name="forwardLines">NUmber of lines to skip before reading</param>
        internal ForwardReader(IRecordReader reader, int forwardLines)
            : this(reader, forwardLines, 0) {}

        /// <summary>
        /// Read a Record handler forward,  optionally skipping n lines and starting at a record number > 0
        /// </summary>
        /// <param name="reader">Reader to get records</param>
        /// <param name="forwardLines">Number of lines to skip before reading</param>
        /// <param name="startLine">Lines already read from file</param>
        internal ForwardReader(IRecordReader reader, int forwardLines, int startLine)
        {
            mReader = reader;

            mFowardLines = forwardLines;
            mLineNumber = startLine;

            mFowardStrings = new string[mFowardLines + 1];
            mRemainingLines = mFowardLines + 1;

            for (int i = 0; i < mFowardLines + 1; i++) {
                mFowardStrings[i] = mReader.ReadRecordString();
                mLineNumber++;
                if (mFowardStrings[i] == null) {
                    mRemainingLines = i;
                    break;
                }
            }
        }

        #endregion

        private int mRemainingLines;

        #region "  LineNumber  "

        private int mLineNumber;

        /// <summary>
        /// Record number within the file - normally the line number
        /// </summary>
        public int LineNumber
        {
            get { return mLineNumber - 1 - mFowardLines; }
        }

        #endregion

        #region "  DiscardForward  "

        private bool mDiscardForward;

        public bool DiscardForward
        {
            get { return mDiscardForward; }
            set { mDiscardForward = value; }
        }

        #endregion

        #region "  FowardLines  "

        private readonly int mFowardLines;

        public int FowardLines
        {
            get { return mFowardLines; }
        }

        #endregion

        #region "  ReadNextLine  "

        public string ReadNextLine()
        {
            if (mRemainingLines <= 0)
                return null;
            else {
                string res = mFowardStrings[mForwardIndex];

                if (mRemainingLines == (mFowardLines + 1)) {
                    mFowardStrings[mForwardIndex] = mReader.ReadRecordString();
                    mLineNumber++;

                    if (mFowardStrings[mForwardIndex] == null)
                        mRemainingLines--;
                }
                else {
                    mRemainingLines--;
                    if (mDiscardForward)
                        return null;
                }

                mForwardIndex = (mForwardIndex + 1)%(mFowardLines + 1);

                return res;
            }
        }

        #endregion

        #region "  RemainingText  "

        public string RemainingText
        {
            get
            {
                var sb = new StringBuilder(100);

                for (int i = 0; i < mRemainingLines + 1; i++)
                    sb.Append(mFowardStrings[(mForwardIndex + i)%(mFowardLines + 1)] + StringHelper.NewLine);

                return sb.ToString();
            }
        }

        #endregion

        #region "  Close  "

        /// <summary>
        /// Close the record reader, which should in turn close the stream
        /// </summary>
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