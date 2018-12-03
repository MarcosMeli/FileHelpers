using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FileHelpers.Streams
{
    /// <summary>
    /// Enable reading a string as a stream
    /// </summary>
    [Serializable]
    internal sealed class InternalStringReader : TextReader
    {
        /// <summary>
        /// String we are analysing
        /// </summary>
        private string mS;

        /// <summary>
        /// Create a stream based on the string
        /// </summary>
        /// <param name="s"></param>
        public InternalStringReader(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            mS = s;
            Length = s.Length;
        }

        /// <summary>
        /// Length of the stream
        /// </summary>
        internal int Length { get; private set; }

        /// <summary>
        /// Position within the stream
        /// </summary>
        internal int Position { get; private set; }

        /// <summary>
        /// Close the stream
        /// </summary>
        public override void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// Close the stream and release resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            mS = null;
            Position = 0;
            Length = 0;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Peek at the next byte along in the stream
        /// </summary>
        /// <returns>Next character as an integer</returns>
        public override int Peek()
        {
            if (mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (Position == Length)
                return -1;
            return mS[Position];
        }

        /// <summary>
        /// Read the next byte along in the string
        /// </summary>
        /// <returns>next character as an integer</returns>
        public override int Read()
        {
            if (mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (Position == Length)
                return -1;
            return mS[Position = Position + 1];
        }

        /// <summary>
        /// Replaces text into the buffer at index for count characters
        /// </summary>
        /// <param name="buffer">
        /// An array of Unicode characters to which characters in this instance
        /// are copied.
        /// </param>
        /// <param name="index">
        /// The index in destination at which the copy operation begins.
        /// </param>
        /// <param name="count">
        /// The number of characters in this instance to copy to destination.
        /// </param>
        /// <returns>number of characters actually copied</returns>
        public override int Read([In, Out] char[] buffer, int index, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if ((buffer.Length - index) < count)
                throw new ArgumentException("offset");
            if (mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            int num = Length - Position;
            if (num > 0) {
                if (num > count)
                    num = count;
                mS.CopyTo(Position, buffer, index, num);
                Position = Position + num;
            }
            return num;
        }

        /// <summary>
        /// Read a line from the buffer
        /// </summary>
        /// <returns>Line without line end</returns>
        public override string ReadLine()
        {
            if (mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            int num = Position;
            while (num < Length) {
                char ch = mS[num];
                switch (ch) {
                    case '\r':
                    case '\n':
                    {
                        string str = mS.Substring(Position, num - Position);
                        Position = num + 1;
                        if (((ch == '\r') && (Position < Length)) &&
                            (mS[Position] == '\n'))
                            Position = Position + 1;
                        return str;
                    }
                }
                num++;
            }
            if (num > Position) {
                string str2 = mS.Substring(Position, num - Position);
                Position = num;
                return str2;
            }
            return null;
        }

        /// <summary>
        /// Read the buffer to the end
        /// </summary>
        /// <returns>String containing all remaining text</returns>
        public override string ReadToEnd()
        {
            string str;
            if (mS == null)
                throw new ObjectDisposedException(null, "The Reader is Closed");
            if (Position == 0)
                str = mS;
            else
                str = mS.Substring(Position, Length - Position);
            Position = Length;
            return str;
        }
    }
}