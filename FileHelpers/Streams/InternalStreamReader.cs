using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Encapsulate stream reader provide some extra caching, and byte by byte
    /// read
    /// </summary>
    
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
        private const int DefaultBufferSize = 0x400;
        private const int DefaultFileStreamBufferSize = 0x1000;
        private Encoding mEncoding;
        private const int MinBufferSize = 0x80;
        private Stream mStream;

        /// <summary>
        /// Create stream reader to be initialised later
        /// </summary>
        internal InternalStreamReader() {}

        /// <summary>
        /// Create a stream reader on a text file (assume UTF8)
        /// </summary>
        /// <param name="path">filename to reader</param>
        public InternalStreamReader(string path)
            : this(path, Encoding.UTF8) {}

        /// <summary>
        /// Create a stream reader specifying path and encoding
        /// </summary>
        /// <param name="path">path to the filename</param>
        /// <param name="encoding">encoding of the file</param>
        public InternalStreamReader(string path, Encoding encoding)
            : this(path, encoding, true, DefaultBufferSize) {}

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
                throw new ArgumentException("Empty path", "path");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be positive");
            var stream = new FileStream(path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize,
                FileOptions.SequentialScan);
            this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
        }


        /// <summary>
        /// Close the stream, cleanup
        /// </summary>
        public override void Close()
        {
            this.Dispose(true);
        }

        private void CompressBuffer(int n)
        {
            for (int i = 0; i < mByteLen - n; i++)
                this.mByteBuffer[i] = this.mByteBuffer[i + n];

            this.mByteLen -= n;
        }

        /// <summary>
        /// Open the file and check the first few bytes for Unicode encoding
        /// values
        /// </summary>
        private void DetectEncoding()
        {
            if (this.mByteLen >= 2) {
                this.mDetectEncoding = false;
                bool flag = false;
                if ((this.mByteBuffer[0] == 0xfe) &&
                    (this.mByteBuffer[1] == 0xff)) {
                    this.mEncoding = new UnicodeEncoding(true, true);
                    CompressBuffer(2);
                    flag = true;
                }
                else if ((this.mByteBuffer[0] == 0xff) &&
                         (this.mByteBuffer[1] == 0xfe)) {
                    if (((this.mByteLen >= 4) && (this.mByteBuffer[2] == 0)) &&
                        (this.mByteBuffer[3] == 0)) {
                        this.mEncoding = new UTF32Encoding(false, true);
                        this.CompressBuffer(4);
                    }
                    else {
                        this.mEncoding = new UnicodeEncoding(false, true);
                        this.CompressBuffer(2);
                    }
                    flag = true;
                }
                else if (((this.mByteLen >= 3) && (this.mByteBuffer[0] == 0xef)) &&
                         ((this.mByteBuffer[1] == 0xbb) && (this.mByteBuffer[2] == 0xbf))) {
                    this.mEncoding = Encoding.UTF8;
                    this.CompressBuffer(3);
                    flag = true;
                }
                else if ((((this.mByteLen >= 4) && (this.mByteBuffer[0] == 0)) &&
                          ((this.mByteBuffer[1] == 0) && (this.mByteBuffer[2] == 0xfe))) &&
                         (this.mByteBuffer[3] == 0xff)) {
                    this.mEncoding = new UTF32Encoding(true, true);
                    flag = true;
                }
                else if (this.mByteLen == 2)
                    this.mDetectEncoding = true;
                if (flag) {
                    this.mDecoder = this.mEncoding.GetDecoder();
                    this.mMaxCharsPerBuffer = this.mEncoding.GetMaxCharCount(this.mByteBuffer.Length);
                    this.mCharBuffer = new char[this.mMaxCharsPerBuffer];
                }
            }
        }

        /// <summary>
        /// Discard all data inside the internal buffer
        /// </summary>
        public void DiscardBufferedData()
        {
            this.mByteLen = 0;
            this.mCharLen = 0;
            this.mCharPos = 0;
            this.mDecoder = this.mEncoding.GetDecoder();
            this.mIsBlocked = false;
        }

        /// <summary>
        /// clean up the stream object
        /// </summary>
        /// <param name="disposing">first call or second</param>
        protected override void Dispose(bool disposing)
        {
            try {
                if ((this.Closable && disposing) &&
                    (this.mStream != null))
                    this.mStream.Close();
            }
            finally {
                if (this.Closable &&
                    (this.mStream != null)) {
                    this.mStream = null;
                    this.mEncoding = null;
                    this.mDecoder = null;
                    this.mByteBuffer = null;
                    this.mCharBuffer = null;
                    this.mCharPos = 0;
                    this.mCharLen = 0;
                    base.Dispose(disposing);
                }
            }
        }

        private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
        {
            this.mStream = stream;
            this.mEncoding = encoding;
            this.mDecoder = encoding.GetDecoder();

            if (bufferSize < MinBufferSize)
                bufferSize = MinBufferSize;

            this.mByteBuffer = new byte[bufferSize];
            this.mMaxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
            this.mCharBuffer = new char[this.mMaxCharsPerBuffer];
            this.mByteLen = 0;
            this.mBytePos = 0;
            this.mDetectEncoding = detectEncodingFromByteOrderMarks;
            this.mPreamble = encoding.GetPreamble();
            this.mCheckPreamble = this.mPreamble.Length > 0;
            this.mIsBlocked = false;
            this.mClosable = true;
        }

        private bool IsPreamble()
        {
            if (this.mCheckPreamble) {
                int num = (this.mByteLen >= this.mPreamble.Length)
                    ? (this.mPreamble.Length - this.mBytePos)
                    : (this.mByteLen - this.mBytePos);
                int num2 = 0;
                while (num2 < num) {
                    if (this.mByteBuffer[this.mBytePos] != this.mPreamble[this.mBytePos]) {
                        this.mBytePos = 0;
                        this.mCheckPreamble = false;
                        break;
                    }
                    num2++;
                    this.mBytePos++;
                }
                if (this.mCheckPreamble &&
                    (this.mBytePos == this.mPreamble.Length)) {
                    this.CompressBuffer(this.mPreamble.Length);
                    this.mBytePos = 0;
                    this.mCheckPreamble = false;
                    this.mDetectEncoding = false;
                }
            }
            return this.mCheckPreamble;
        }

        /// <summary>
        /// Return the byte at the current position
        /// </summary>
        /// <returns>byte at current position or -1 on error</returns>
        public override int Peek()
        {
            if (this.mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");
            if ((this.mCharPos != this.mCharLen) ||
                (!this.mIsBlocked && (this.ReadBuffer() != 0)))
                return this.mCharBuffer[this.mCharPos];
            return -1;
        }

        /// <summary>
        /// Read a byte from the stream
        /// </summary>
        /// <returns></returns>
        public override int Read()
        {
            if (this.mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");

            if ((this.mCharPos == this.mCharLen) &&
                (this.ReadBuffer() == 0))
                return -1;

            int num = this.mCharBuffer[this.mCharPos];
            this.mCharPos++;
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

        //public override int Read([In, Out] char[] buffer, int index, int count)
        //{
        //    if (this.stream == null)
        //    {
        //        throw new ObjectDisposedException(null, "The reader is closed");
        //    }
        //    if (buffer == null)
        //    {
        //        throw new ArgumentNullException("buffer", "buffer cant be null");
        //    }
        //    if ((index < 0) || (count < 0))
        //    {
        //        throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count");
        //    }
        //    if ((buffer.Length - index) < count)
        //    {
        //        throw new ArgumentException("Invalid Offlen");
        //    }
        //    int num = 0;
        //    bool readToUserBuffer = false;
        //    while (count > 0)
        //    {
        //        int num2 = this.charLen - this.charPos;
        //        if (num2 == 0)
        //        {
        //            num2 = this.ReadBuffer(buffer, index + num, count, out readToUserBuffer);
        //        }
        //        if (num2 == 0)
        //        {
        //            return num;
        //        }
        //        if (num2 > count)
        //        {
        //            num2 = count;
        //        }
        //        if (!readToUserBuffer)
        //        {
        //            Buffer.InternalBlockCopy(this.charBuffer, this.charPos * 2, buffer, (index + num) * 2, num2 * 2);
        //            this.charPos += num2;
        //        }
        //        num += num2;
        //        count -= num2;
        //        if (this._isBlocked)
        //        {
        //            return num;
        //        }
        //    }
        //    return num;
        //}

        private int ReadBuffer()
        {
            this.mCharLen = 0;
            this.mCharPos = 0;
            if (!this.mCheckPreamble)
                this.mByteLen = 0;

            do {
                if (this.mCheckPreamble) {
                    int num = this.mStream.Read(this.mByteBuffer, this.mBytePos, this.mByteBuffer.Length - this.mBytePos);
                    if (num == 0) {
                        if (this.mByteLen > 0) {
                            this.mCharLen += this.mDecoder.GetChars(this.mByteBuffer,
                                0,
                                this.mByteLen,
                                this.mCharBuffer,
                                this.mCharLen);
                        }
                        return this.mCharLen;
                    }
                    this.mByteLen += num;
                }
                else {
                    this.mByteLen = this.mStream.Read(this.mByteBuffer, 0, this.mByteBuffer.Length);
                    if (this.mByteLen == 0)
                        return this.mCharLen;
                }
                this.mIsBlocked = this.mByteLen < this.mByteBuffer.Length;
                if (!this.IsPreamble()) {
                    if (this.mDetectEncoding &&
                        (this.mByteLen >= 2))
                        this.DetectEncoding();
                    this.mCharLen += this.mDecoder.GetChars(this.mByteBuffer,
                        0,
                        this.mByteLen,
                        this.mCharBuffer,
                        this.mCharLen);
                }
            } while (this.mCharLen == 0);
            return this.mCharLen;
        }

        //private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
        //{
        //    this.charLen = 0;
        //    this.charPos = 0;
        //    if (!this._checkPreamble)
        //    {
        //        this.byteLen = 0;
        //    }
        //    int charIndex = 0;
        //    readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
        //    do
        //    {
        //        if (this._checkPreamble)
        //        {
        //            int num2 = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
        //            if (num2 == 0)
        //            {
        //                if (this.byteLen > 0)
        //                {
        //                    if (readToUserBuffer)
        //                    {
        //                        charIndex += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
        //                        this.charLen = 0;
        //                        return charIndex;
        //                    }
        //                    charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
        //                    this.charLen += charIndex;
        //                }
        //                return charIndex;
        //            }
        //            this.byteLen += num2;
        //        }
        //        else
        //        {
        //            this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
        //            if (this.byteLen == 0)
        //            {
        //                return charIndex;
        //            }
        //        }
        //        this._isBlocked = this.byteLen < this.byteBuffer.Length;
        //        if (!this.IsPreamble())
        //        {
        //            if (this._detectEncoding && (this.byteLen >= 2))
        //            {
        //                this.DetectEncoding();
        //                readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
        //            }
        //            this.charPos = 0;
        //            if (readToUserBuffer)
        //            {
        //                charIndex += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + charIndex);
        //                this.charLen = 0;
        //            }
        //            else
        //            {
        //                charIndex = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, charIndex);
        //                this.charLen += charIndex;
        //            }
        //        }
        //    }
        //    while (charIndex == 0);
        //    this._isBlocked &= charIndex < desiredChars;
        //    return charIndex;
        //}

        public override string ReadLine()
        {
            if (this.mStream == null)
                throw new ObjectDisposedException(null, "The reader is closed");

            if ((this.mCharPos == this.mCharLen) &&
                (this.ReadBuffer() == 0))
                return null;
            StringBuilder builder = null;
            do {
                int currentCharPos = this.mCharPos;
                do {
                    char ch = this.mCharBuffer[currentCharPos];
                    switch (ch) {
                        case '\r':
                        case '\n':
                            string str;
                            if (builder != null) {
                                builder.Append(this.mCharBuffer, this.mCharPos, currentCharPos - this.mCharPos);
                                //str = new string(charBuffer, this.charPos, currentCharPos - this.charPos);
                                str = builder.ToString();
                            }
                            else
                                str = new string(this.mCharBuffer, this.mCharPos, currentCharPos - this.mCharPos);
                            this.mCharPos = currentCharPos + 1;
                            if (((ch == '\r') && ((this.mCharPos < this.mCharLen) || (this.ReadBuffer() > 0))) &&
                                (this.mCharBuffer[this.mCharPos] == '\n'))
                                this.mCharPos++;
                            return str;
                    }
                    currentCharPos++;
                } while (currentCharPos < this.mCharLen);
                currentCharPos = this.mCharLen - this.mCharPos;
                if (builder == null)
                    builder = new StringBuilder(currentCharPos + 80);
                builder.Append(this.mCharBuffer, this.mCharPos, currentCharPos);
            } while (this.ReadBuffer() > 0);
            return builder.ToString();
        }

        /// <summary>
        /// Is the stream able to be closed
        /// </summary>
        internal bool Closable
        {
            get { return this.mClosable; }
        }

        /// <summary>
        /// What is the streams current encoding
        /// </summary>
        public Encoding CurrentEncoding
        {
            get { return this.mEncoding; }
        }

        /// <summary>
        /// What is the underlying stream on input file
        /// </summary>
        public Stream BaseStream
        {
            get { return this.mStream; }
        }

        /// <summary>
        /// Check that the stream has ended,  all data read
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                if (this.mStream == null)
                    throw new ObjectDisposedException(null, "The reader is closed");
                if (this.mCharPos < this.mCharLen)
                    return false;
                return (this.ReadBuffer() == 0);
            }
        }
    }
}