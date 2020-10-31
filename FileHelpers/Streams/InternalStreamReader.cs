using System;
using System.IO;
using System.Text;

namespace FileHelpers.Streams
{
    /// <summary>
    /// Encapsulate stream reader provide some extra caching, and byte by byte
    /// read
    /// </summary>
    [Serializable]
    internal sealed class InternalStreamReader : TextReader
    {
        // Fields
        private bool mCheckPreamble;
        private bool mClosable;
        private bool mDetectEncoding;
        private bool mIsBlocked;
        private int mMaxCharsPerBuffer;
        private byte[] mPreamble;
        private byte[] mByteBuffer;
        private int mByteLen;
        private int mBytePos;
        private char[] mCharBuffer;
        private int mCharLen;
        private int mCharPos;
        private Decoder mDecoder;
        private Encoding mEncoding;
        private const int MinBufferSize = 0x80;
        private Stream mStream;

        /// <summary>
        /// Open a file for reading allowing encoding,  detecting type and buffersize
        /// </summary>
        /// <param name="path">Filename to read</param>
        /// <param name="encoding">Encoding of file,  eg UTF8</param>
        /// <param name="detectEncodingFromByteOrderMarks">Detect type of file from contents</param>
        /// <param name="bufferSize">Buffer size for the read</param>
        public InternalStreamReader(string path,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            if ((path == null) ||
                (encoding == null)) {
                throw new ArgumentNullException((path == null)
                    ? "path"
                    : "encoding");
            }
            if (path.Length == 0)
                throw new ArgumentException("Empty path", nameof(path));
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "bufferSize must be positive");
            var stream = new FileStream(path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize,
                FileOptions.SequentialScan);
            Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
        }


        /// <summary>
        /// Close the stream, cleanup
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

        private void CompressBuffer(int n)
        {
            for (int i = 0; i < mByteLen - n; i++)
                mByteBuffer[i] = mByteBuffer[i + n];

            mByteLen -= n;
        }

        /// <summary>
        /// Open the file and check the first few bytes for Unicode encoding
        /// values
        /// </summary>
        private void DetectEncoding()
        {
            if (mByteLen >= 2) {
                mDetectEncoding = false;
                bool flag = false;
                if ((mByteBuffer[0] == 0xfe) &&
                    (mByteBuffer[1] == 0xff)) {
                    mEncoding = new UnicodeEncoding(true, true);
                    CompressBuffer(2);
                    flag = true;
                }
                else if ((mByteBuffer[0] == 0xff) &&
                         (mByteBuffer[1] == 0xfe)) {
                    if (((mByteLen >= 4) && (mByteBuffer[2] == 0)) &&
                        (mByteBuffer[3] == 0)) {
                        mEncoding = new UTF32Encoding(false, true);
                        CompressBuffer(4);
                    }
                    else {
                        mEncoding = new UnicodeEncoding(false, true);
                        CompressBuffer(2);
                    }
                    flag = true;
                }
                else if (((mByteLen >= 3) && (mByteBuffer[0] == 0xef)) &&
                         ((mByteBuffer[1] == 0xbb) && (mByteBuffer[2] == 0xbf))) {
                    mEncoding = Encoding.UTF8;
                    CompressBuffer(3);
                    flag = true;
                }
                else if ((((mByteLen >= 4) && (mByteBuffer[0] == 0)) &&
                          ((mByteBuffer[1] == 0) && (mByteBuffer[2] == 0xfe))) &&
                         (mByteBuffer[3] == 0xff)) {
                    mEncoding = new UTF32Encoding(true, true);
                    flag = true;
                }
                else if (mByteLen == 2)
                    mDetectEncoding = true;
                if (flag) {
                    mDecoder = mEncoding.GetDecoder();
                    mMaxCharsPerBuffer = mEncoding.GetMaxCharCount(mByteBuffer.Length);
                    mCharBuffer = new char[mMaxCharsPerBuffer];
                }
            }
        }

        /// <summary>
        /// Discard all data inside the internal buffer
        /// </summary>
        public void DiscardBufferedData()
        {
            mByteLen = 0;
            mCharLen = 0;
            mCharPos = 0;
            mDecoder = mEncoding.GetDecoder();
            mIsBlocked = false;
        }

        /// <summary>
        /// clean up the stream object
        /// </summary>
        /// <param name="disposing">first call or second</param>
        protected override void Dispose(bool disposing)
        {
            try {
                if ((Closable && disposing) &&
                    (mStream != null))
                    mStream.Close();
            }
            finally {
                if (Closable &&
                    (mStream != null)) {
                    mStream = null;
                    mEncoding = null;
                    mDecoder = null;
                    mByteBuffer = null;
                    mCharBuffer = null;
                    mCharPos = 0;
                    mCharLen = 0;
                    base.Dispose(disposing);
                }
            }
        }

        private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
        {
            mStream = stream;
            mEncoding = encoding;
            mDecoder = encoding.GetDecoder();

            if (bufferSize < MinBufferSize)
                bufferSize = MinBufferSize;

            mByteBuffer = new byte[bufferSize];
            mMaxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
            mCharBuffer = new char[mMaxCharsPerBuffer];
            mByteLen = 0;
            mBytePos = 0;
            mDetectEncoding = detectEncodingFromByteOrderMarks;
            mPreamble = encoding.GetPreamble();
            mCheckPreamble = mPreamble.Length > 0;
            mIsBlocked = false;
            mClosable = true;
        }

        private bool IsPreamble()
        {
            if (mCheckPreamble) {
                int num = (mByteLen >= mPreamble.Length)
                    ? (mPreamble.Length - mBytePos)
                    : (mByteLen - mBytePos);
                int num2 = 0;
                while (num2 < num) {
                    if (mByteBuffer[mBytePos] != mPreamble[mBytePos]) {
                        mBytePos = 0;
                        mCheckPreamble = false;
                        break;
                    }
                    num2++;
                    mBytePos++;
                }
                if (mCheckPreamble &&
                    (mBytePos == mPreamble.Length)) {
                    CompressBuffer(mPreamble.Length);
                    mBytePos = 0;
                    mCheckPreamble = false;
                    mDetectEncoding = false;
                }
            }
            return mCheckPreamble;
        }

        /// <summary>
        /// Return the byte at the current position
        /// </summary>
        /// <returns>byte at current position or -1 on error</returns>
        public override int Peek()
        {
            if (mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");
            if ((mCharPos != mCharLen) ||
                (!mIsBlocked && (ReadBuffer() != 0)))
                return mCharBuffer[mCharPos];
            return -1;
        }

        /// <summary>
        /// Read a byte from the stream
        /// </summary>
        /// <returns></returns>
        public override int Read()
        {
            if (mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");

            if ((mCharPos == mCharLen) &&
                (ReadBuffer() == 0))
                return -1;

            int num = mCharBuffer[mCharPos];
            mCharPos++;
            return num;
        }

        /// <summary>
        /// Position within the file
        /// </summary>
        public long Position
        {
            get
            {
                return
                    mStream.Position + mCharPos - mCharLen;
            }
        }

        private int ReadBuffer()
        {
            mCharLen = 0;
            mCharPos = 0;
            if (!mCheckPreamble)
                mByteLen = 0;

            do {
                if (mCheckPreamble) {
                    int num = mStream.Read(mByteBuffer, mBytePos, mByteBuffer.Length - mBytePos);
                    if (num == 0) {
                        if (mByteLen > 0) {
                            mCharLen += mDecoder.GetChars(mByteBuffer,
                                0,
                                mByteLen,
                                mCharBuffer,
                                mCharLen);
                        }
                        return mCharLen;
                    }
                    mByteLen += num;
                }
                else {
                    mByteLen = mStream.Read(mByteBuffer, 0, mByteBuffer.Length);
                    if (mByteLen == 0)
                        return mCharLen;
                }
                mIsBlocked = mByteLen < mByteBuffer.Length;
                if (!IsPreamble()) {
                    if (mDetectEncoding &&
                        (mByteLen >= 2))
                        DetectEncoding();
                    mCharLen += mDecoder.GetChars(mByteBuffer,
                        0,
                        mByteLen,
                        mCharBuffer,
                        mCharLen);
                }
            } while (mCharLen == 0);
            return mCharLen;
        }

        public override string ReadLine()
        {
            if (mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");

            if ((mCharPos == mCharLen) &&
                (ReadBuffer() == 0))
                return null;
            StringBuilder builder = null;
            do {
                int currentCharPos = mCharPos;
                do {
                    char ch = mCharBuffer[currentCharPos];
                    switch (ch) {
                        case '\r':
                        case '\n':
                            string str;
                            if (builder != null) {
                                builder.Append(mCharBuffer, mCharPos, currentCharPos - mCharPos);
                                //str = new string(charBuffer, this.charPos, currentCharPos - this.charPos);
                                str = builder.ToString();
                            }
                            else
                                str = new string(mCharBuffer, mCharPos, currentCharPos - mCharPos);
                            mCharPos = currentCharPos + 1;
                            if (((ch == '\r') && ((mCharPos < mCharLen) || (ReadBuffer() > 0))) &&
                                (mCharBuffer[mCharPos] == '\n'))
                                mCharPos++;
                            return str;
                    }
                    currentCharPos++;
                } while (currentCharPos < mCharLen);
                currentCharPos = mCharLen - mCharPos;
                if (builder == null)
                    builder = new StringBuilder(currentCharPos + 80);
                builder.Append(mCharBuffer, mCharPos, currentCharPos);
            } while (ReadBuffer() > 0);
            return builder.ToString();
        }

        /// <summary>
        /// Is the stream able to be closed
        /// </summary>
        internal bool Closable
        {
            get { return mClosable; }
        }

        /// <summary>
        /// What is the streams current encoding
        /// </summary>
        public Encoding CurrentEncoding
        {
            get { return mEncoding; }
        }

        /// <summary>
        /// What is the underlying stream on input file
        /// </summary>
        public Stream BaseStream
        {
            get { return mStream; }
        }

        /// <summary>
        /// Check that the stream has ended,  all data read
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                if (mStream == null)
                    throw new ObjectDisposedException(null, "The reader is closed");
                if (mCharPos < mCharLen)
                    return false;
                return (ReadBuffer() == 0);
            }
        }
    }
}