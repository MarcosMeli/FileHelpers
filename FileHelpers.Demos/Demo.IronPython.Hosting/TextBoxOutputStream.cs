using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Demo.IronPython.Hosting
{
    class TextBoxOutputStream : Stream
    {
        private const int buffSize = 512;
        private TextBox textBox;
        private StringBuilder buffer;
        private bool allowAllFlushes = true;

        public bool AllowAllFlushes
        {
            get { return allowAllFlushes; }
            set { allowAllFlushes = value; }
        }

        public TextBoxOutputStream(TextBox textBox)
        {
            this.textBox = textBox;
            buffer = new StringBuilder(buffSize);
        }

        public void realFlush()
        {
            textBox.AppendText(buffer.ToString());
            buffer.Length = 0;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.buffer.Length + count > buffSize)
                realFlush();
            this.buffer.Append(Encoding.Default.GetString(buffer, offset, count));
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            if (allowAllFlushes)
                realFlush();
        }

        #region Unsupported operations
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seeking unsupported on this Stream");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Seeking unsupported on this Stream");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Reading unsupported on this Stream");
        }

        public override long Length
        {
            get { throw new NotSupportedException("Seeking unsupported on this Stream"); }
        }

        public override long Position
        {
            get { throw new NotSupportedException("Seeking unsupported on this Stream"); }
            set { throw new NotSupportedException("Seeking unsupported on this Stream"); }
        }
        #endregion
    }
}
