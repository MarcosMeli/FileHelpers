using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FileHelpers
{
    [Serializable]
    internal sealed class InternalStringReader : TextReader
    {
        // Fields
        private string mS;

        // Methods
        public InternalStringReader(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            this.mS = s;
            this.Length = (s == null) ? 0 : s.Length;
        }

        internal int Length { get; private set; }

        internal int Position { get; private set; }

        public override void Close()
        {
            this.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            this.mS = null;
            this.Position = 0;
            this.Length = 0;
            base.Dispose(disposing);
        }

        public override int Peek()
        {
            if (this.mS == null)
            {
                throw new ObjectDisposedException(null, "The Reader is Closed");
            }
            if (this.Position == this.Length)
            {
                return -1;
            }
            return this.mS[this.Position];
        }

        public override int Read()
        {
            if (this.mS == null)
            {
                throw new ObjectDisposedException(null, "The Reader is Closed");
            }
            if (this.Position == this.Length)
            {
                return -1;
            }
            return this.mS[this.Position = this.Position + 1];
        }

        public override int Read([In, Out] char[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((buffer.Length - index) < count)
            {
                throw new ArgumentException("offset");
            }
            if (this.mS == null)
            {
                throw new ObjectDisposedException(null, "The Reader is Closed");
            }
            int num = this.Length - this.Position;
            if (num > 0)
            {
                if (num > count)
                {
                    num = count;
                }
                this.mS.CopyTo(this.Position, buffer, index, num);
                this.Position = this.Position + num;
            }
            return num;
        }

        public override string ReadLine()
        {
            if (this.mS == null)
            {
                throw new ObjectDisposedException(null, "The Reader is Closed");
            }
            int num = this.Position;
            while (num < this.Length)
            {
                char ch = this.mS[num];
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        {
                            string str = this.mS.Substring(this.Position, num - this.Position);
                            this.Position = num + 1;
                            if (((ch == '\r') && (this.Position < this.Length)) && (this.mS[this.Position] == '\n'))
                            {
                                this.Position = this.Position + 1;
                            }
                            return str;
                        }
                }
                num++;
            }
            if (num > this.Position)
            {
                string str2 = this.mS.Substring(this.Position, num - this.Position);
                this.Position = num;
                return str2;
            }
            return null;
        }

        public override string ReadToEnd()
        {
            string str;
            if (this.mS == null)
            {
                throw new ObjectDisposedException(null, "The Reader is Closed");
            }
            if (this.Position == 0)
            {
                str = this.mS;
            }
            else
            {
                str = this.mS.Substring(this.Position, this.Length - this.Position);
            }
            this.Position = this.Length;
            return str;
        }
    }
}