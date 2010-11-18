using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Encapsulate stream reader provide some extra caching, and byte by byte
    /// read
    /// </summary>
    [Serializable]
    internal sealed class InternalStreamReader : TextReader
    {


        // Fields
        private bool _checkPreamble;
        private bool _closable;
        private bool _detectEncoding;
        private bool _isBlocked;
        private int _maxCharsPerBuffer;
        private byte[] _preamble;
        private byte[] byteBuffer;
        private int byteLen;
        private int bytePos;
        private char[] charBuffer;
        private int charLen;
        private int charPos;
        private Decoder decoder;
        internal const int DefaultBufferSize = 0x400;
        private const int DefaultFileStreamBufferSize = 0x1000;
        private Encoding encoding;
        private const int MinBufferSize = 0x80;
        private Stream stream;

        /// <summary>
        /// Create stream reader to be initialised later
        /// </summary>
        internal InternalStreamReader()
        {
        }

        /// <summary>
        /// Create a stream reader on a text file (assume UTF8)
        /// </summary>
        /// <param name="path">filename to reader</param>
        public InternalStreamReader(string path) : this(path, Encoding.UTF8)
        {
        }

        /// <summary>
        /// Create a stream reader specifying path and encoding
        /// </summary>
        /// <param name="path">path to the filename</param>
        /// <param name="encoding">encoding of the file</param>
        public InternalStreamReader(string path, Encoding encoding) : this(path, encoding, true, DefaultBufferSize)
        {
        }

        /// <summary>
        /// Open a file for reading allowing encoding,  detecting type and buffersize
        /// </summary>
        /// <param name="path">Filename to read</param>
        /// <param name="encoding">Encoding of file,  eg UTF8</param>
        /// <param name="detectEncodingFromByteOrderMarks">Detect type of file from contents</param>
        /// <param name="bufferSize">Buffer size for the read</param>
        public InternalStreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
        {
            if ((path == null) || (encoding == null))
            {
                throw new ArgumentNullException((path == null) ? "path" : "encoding");
            }
            if (path.Length == 0)
            {
                throw new ArgumentException("Empty path", "path");
            }
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize", "bufferSize must be positive");
            }
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan);
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
            for (int i = 0; i < byteLen - n; i++)
                this.byteBuffer[i] = this.byteBuffer[i + n];

            this.byteLen -= n;
        }

        /// <summary>
        /// Open the file and check the first few bytes for Unicode encoding
        /// values
        /// </summary>
        private void DetectEncoding()
        {
            if (this.byteLen >= 2)
            {
                this._detectEncoding = false;
                bool flag = false;
                if ((this.byteBuffer[0] == 0xfe) && (this.byteBuffer[1] == 0xff))
                {
                    this.encoding = new UnicodeEncoding(true, true);
                    CompressBuffer(2);
                    flag = true;
                }
                else if ((this.byteBuffer[0] == 0xff) && (this.byteBuffer[1] == 0xfe))
                {
                    if (((this.byteLen >= 4) && (this.byteBuffer[2] == 0)) && (this.byteBuffer[3] == 0))
                    {
                        this.encoding = new UTF32Encoding(false, true);
                        this.CompressBuffer(4);
                    }
                    else
                    {
                        this.encoding = new UnicodeEncoding(false, true);
                        this.CompressBuffer(2);
                    }
                    flag = true;
                }
                else if (((this.byteLen >= 3) && (this.byteBuffer[0] == 0xef)) && ((this.byteBuffer[1] == 0xbb) && (this.byteBuffer[2] == 0xbf)))
                {
                    this.encoding = Encoding.UTF8;
                    this.CompressBuffer(3);
                    flag = true;
                }
                else if ((((this.byteLen >= 4) && (this.byteBuffer[0] == 0)) && ((this.byteBuffer[1] == 0) && (this.byteBuffer[2] == 0xfe))) && (this.byteBuffer[3] == 0xff))
                {
                    this.encoding = new UTF32Encoding(true, true);
                    flag = true;
                }
                else if (this.byteLen == 2)
                {
                    this._detectEncoding = true;
                }
                if (flag)
                {
                    this.decoder = this.encoding.GetDecoder();
                    this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
                    this.charBuffer = new char[this._maxCharsPerBuffer];
                }
            }
        }

        /// <summary>
        /// Discard all data inside the internal buffer
        /// </summary>
        public void DiscardBufferedData()
        {
            this.byteLen = 0;
            this.charLen = 0;
            this.charPos = 0;
            this.decoder = this.encoding.GetDecoder();
            this._isBlocked = false;
        }

        /// <summary>
        /// clean up the stream object
        /// </summary>
        /// <param name="disposing">first call or second</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if ((this.Closable && disposing) && (this.stream != null))
                {
                    this.stream.Close();
                }
            }
            finally
            {
                if (this.Closable && (this.stream != null))
                {
                    this.stream = null;
                    this.encoding = null;
                    this.decoder = null;
                    this.byteBuffer = null;
                    this.charBuffer = null;
                    this.charPos = 0;
                    this.charLen = 0;
                    base.Dispose(disposing);
                }
            }
        }

        private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
        {
            this.stream = stream;
            this.encoding = encoding;
            this.decoder = encoding.GetDecoder();

            if (bufferSize < MinBufferSize)
                bufferSize = MinBufferSize;

            this.byteBuffer = new byte[bufferSize];
            this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
            this.charBuffer = new char[this._maxCharsPerBuffer];
            this.byteLen = 0;
            this.bytePos = 0;
            this._detectEncoding = detectEncodingFromByteOrderMarks;
            this._preamble = encoding.GetPreamble();
            this._checkPreamble = this._preamble.Length > 0;
            this._isBlocked = false;
            this._closable = true;
        }

        private bool IsPreamble()
        {
            if (this._checkPreamble)
            {
                int num = (this.byteLen >= this._preamble.Length) ? (this._preamble.Length - this.bytePos) : (this.byteLen - this.bytePos);
                int num2 = 0;
                while (num2 < num)
                {
                    if (this.byteBuffer[this.bytePos] != this._preamble[this.bytePos])
                    {
                        this.bytePos = 0;
                        this._checkPreamble = false;
                        break;
                    }
                    num2++;
                    this.bytePos++;
                }
                if (this._checkPreamble && (this.bytePos == this._preamble.Length))
                {
                    this.CompressBuffer(this._preamble.Length);
                    this.bytePos = 0;
                    this._checkPreamble = false;
                    this._detectEncoding = false;
                }
            }
            return this._checkPreamble;
        }

        /// <summary>
        /// Return the byte at the current position
        /// </summary>
        /// <returns>byte at current position or -1 on error</returns>
        public override int Peek()
        {
            if (this.stream == null)
            {
                throw new ObjectDisposedException(null, "The reader is closed");
            }
            if ((this.charPos != this.charLen) || (!this._isBlocked && (this.ReadBuffer() != 0)))
            {
                return this.charBuffer[this.charPos];
            }
            return -1;
        }

        /// <summary>
        /// Read a byte from the stream
        /// </summary>
        /// <returns></returns>
        public override int Read()
        {
            if (this.stream == null)
            {
                throw new ObjectDisposedException(null, "The reader is closed");
            }
            if ((this.charPos == this.charLen) && (this.ReadBuffer() == 0))
            {
                return -1;
            }
            int num = this.charBuffer[this.charPos];
            this.charPos++;
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
                     stream.Position + charPos - charLen;
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
            this.charLen = 0;
            this.charPos = 0;
            if (!this._checkPreamble)
            {
                this.byteLen = 0;
            }
            do
            {
                if (this._checkPreamble)
                {
                    int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
                    if (num == 0)
                    {
                        if (this.byteLen > 0)
                        {
                            this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
                        }
                        return this.charLen;
                    }
                    this.byteLen += num;
                }
                else
                {
                    this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
                    if (this.byteLen == 0)
                    {
                        return this.charLen;
                    }
                }
                this._isBlocked = this.byteLen < this.byteBuffer.Length;
                if (!this.IsPreamble())
                {
                    if (this._detectEncoding && (this.byteLen >= 2))
                    {
                        this.DetectEncoding();
                    }
                    this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
                }
            }
            while (this.charLen == 0);
            return this.charLen;
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
            if (this.stream == null)
            {
                throw new ObjectDisposedException(null, "The reader is closed");
            }

            if ((this.charPos == this.charLen) && (this.ReadBuffer() == 0))
            {
                return null;
            }
            StringBuilder builder = null;
            do
            {
                int currentCharPos = this.charPos;
                do
                {
                    char ch = this.charBuffer[currentCharPos];
                    switch (ch)
                    {
                        case '\r':
                        case '\n':
                            string str;
                            if (builder != null)
                            {
                                builder.Append(this.charBuffer, this.charPos, currentCharPos - this.charPos);
                                //str = new string(charBuffer, this.charPos, currentCharPos - this.charPos);
                                str = builder.ToString();
                            }
                            else
                            {
                                str = new string(this.charBuffer, this.charPos, currentCharPos - this.charPos);
                            }
                            this.charPos = currentCharPos + 1;
                            if (((ch == '\r') && ((this.charPos < this.charLen) || (this.ReadBuffer() > 0))) && (this.charBuffer[this.charPos] == '\n'))
                            {
                                this.charPos++;
                            }
                            return str;
                    }
                    currentCharPos++;
                }
                while (currentCharPos < this.charLen);
                currentCharPos = this.charLen - this.charPos;
                if (builder == null)
                {
                    builder = new StringBuilder(currentCharPos + 80);
                }
                builder.Append(this.charBuffer, this.charPos, currentCharPos);
            }
            while (this.ReadBuffer() > 0);
            return builder.ToString();
        }

        /// <summary>
        /// Is the stream able to be closed
        /// </summary>
        internal bool Closable
        {
            get
            {
                return this._closable;
            }
        }

        /// <summary>
        /// What is the streams current encoding
        /// </summary>
        public Encoding CurrentEncoding
        {
            get
            {
                return this.encoding;
            }
        }

        /// <summary>
        /// What is the underlying stream on input file
        /// </summary>
        public Stream BaseStream
        {
            get
            {
                return this.stream;
            }
        }

        /// <summary>
        /// Check that the stream has ended,  all data read
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                if (this.stream == null)
                {
                    throw new ObjectDisposedException(null, "The reader is closed");
                }
                if (this.charPos < this.charLen)
                {
                    return false;
                }
                return (this.ReadBuffer() == 0);
            }
        }
    }
}