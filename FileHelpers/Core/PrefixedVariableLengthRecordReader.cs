

using System;
using System.IO;
using System.Text;

namespace FileHelpers
{
    public sealed class PrefixedVariableLengthRecordReader : IRecordReader
    {
        private readonly Stream stream;
        private readonly LengthIndicatorType indicatorType;
        private readonly int indicatorLength;
        private readonly Encoding encoding;
        private byte[] buffer;

        public PrefixedVariableLengthRecordReader(string fileName, LengthIndicatorType indicatorType, int indicatorLength) :
            this(new FileStream(fileName, FileMode.Open, FileAccess.Read), indicatorType, indicatorLength, Encoding.Default)
        {
        }

        public PrefixedVariableLengthRecordReader(string fileName, LengthIndicatorType indicatorType, int indicatorLength, Encoding encoding) :
            this(new FileStream(fileName, FileMode.Open, FileAccess.Read), indicatorType, indicatorLength, encoding)
        {
        }

        public PrefixedVariableLengthRecordReader(Stream stream, LengthIndicatorType indicatorType, int indicatorLength) :
            this(stream, indicatorType, indicatorLength, Encoding.Default)
        {
        }

        public PrefixedVariableLengthRecordReader(Stream stream, LengthIndicatorType indicatorType, int indicatorLength, Encoding encoding)
        {
            this.stream = stream;
            this.indicatorType = indicatorType;
            this.indicatorLength = indicatorLength;
            this.encoding = encoding;
        }

        public string ReadRecordString()
        {
            int length = 0;

            switch(indicatorType)
            {
                case LengthIndicatorType.MSB:
                    length = ReadMSBLengthIndicator();
                    break;
                case LengthIndicatorType.LSB:
                    length = ReadLSBLengthIndicator();
                    break;
                case LengthIndicatorType.ASCII:
                    length = ReadASCIILengthIndicator();
                    break;
            }

            if (length == 0)
                return null;

            ReadBytes(length);
            return encoding.GetString(buffer, 0, length);
        }

        public void Close()
        {
            stream.Close();
        }

        private void ReadBytes(int count)
        {
            if (buffer == null || buffer.Length < count)
                buffer = new byte[count];


            int offset = 0;
            while (offset < count)
            {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new Exception(string.Format("End of stream while reading {0} bytes", count));

                offset += read;
            }
        }

        private int ReadMSBLengthIndicator()
        {
            ReadBytes(indicatorLength);
            int ret = 0;
            for (int i = 0; i < indicatorLength; i++)
                ret = unchecked((ret << 8) | buffer[i]);

            return ret;
        }

        private int ReadLSBLengthIndicator()
        {
            ReadBytes(indicatorLength);
            int ret = 0;
            for (int i = 0; i < indicatorLength; i++)
                ret = unchecked(ret | buffer[i]);

            return ret;
        }

        private int ReadASCIILengthIndicator()
        {
            ReadBytes(indicatorLength);
            return int.Parse(Encoding.ASCII.GetString(buffer, 0, indicatorLength));
        }
    }
}