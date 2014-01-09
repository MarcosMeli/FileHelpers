using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Read a variable length record from a file
    /// each record has a prefix that tells the program how many bytes
    /// to read for this record
    /// </summary>
    public sealed class PrefixedVariableLengthRecordReader : IRecordReader
    {
        /// <summary>
        /// Stream to read
        /// </summary>
        private readonly Stream stream;

        /// <summary>
        /// Indicates type of record length field, ASCII, or binary
        /// </summary>
        private readonly LengthIndicatorType indicatorType;

        private readonly int indicatorLength;
        private readonly Encoding encoding;
        private byte[] buffer;

        /// <summary>
        /// Read a record with a variable length prefix on them
        /// and process them record by record
        /// </summary>
        /// <param name="fileName">Name of file to read</param>
        /// <param name="indicatorType">Type of record length indicator</param>
        /// <param name="indicatorLength">Length of the indicator in bytes</param>
        public PrefixedVariableLengthRecordReader(string fileName,
            LengthIndicatorType indicatorType,
            int indicatorLength)
            :
                this(
                new FileStream(fileName, FileMode.Open, FileAccess.Read),
                indicatorType,
                indicatorLength,
                Encoding.Default) {}

        /// <summary>
        /// Read a record with a variable length prefix on them
        /// and process them record by record
        /// </summary>
        /// <param name="fileName">Name of file to read</param>
        /// <param name="indicatorType">Type of record length indicator</param>
        /// <param name="indicatorLength">Length of the indicator in bytes</param>
        /// <param name="encoding">Encoding on the file</param>
        public PrefixedVariableLengthRecordReader(string fileName,
            LengthIndicatorType indicatorType,
            int indicatorLength,
            Encoding encoding)
            :
                this(new FileStream(fileName, FileMode.Open, FileAccess.Read), indicatorType, indicatorLength, encoding) {}

        /// <summary>
        /// Read a record with a variable length prefix on them
        /// and process them record by record
        /// </summary>
        /// <param name="stream">Stream reader to process</param>
        /// <param name="indicatorType">Type of record length indicator</param>
        /// <param name="indicatorLength">Length of the indicator in bytes</param>
        public PrefixedVariableLengthRecordReader(Stream stream, LengthIndicatorType indicatorType, int indicatorLength)
            :
                this(stream, indicatorType, indicatorLength, Encoding.Default) {}

        /// <summary>
        /// Read a record with a variable length prefix on them
        /// and process them record by record
        /// </summary>
        /// <param name="stream">Stream reader to process</param>
        /// <param name="indicatorType">Type of record length indicator</param>
        /// <param name="indicatorLength">Length of the indicator in bytes</param>
        /// <param name="encoding">Encoding on the file</param>
        public PrefixedVariableLengthRecordReader(Stream stream,
            LengthIndicatorType indicatorType,
            int indicatorLength,
            Encoding encoding)
        {
            this.stream = stream;
            this.indicatorType = indicatorType;
            this.indicatorLength = indicatorLength;
            this.encoding = encoding;
        }

        /// <summary>
        /// Read a record from the file
        /// </summary>
        /// <returns>string representing a record</returns>
        public string ReadRecordString()
        {
            int length = 0;

            switch (indicatorType) {
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

        /// <summary>
        /// Close the file for reading
        /// </summary>
        public void Close()
        {
            stream.Close();
        }

        /// <summary>
        /// Read a specific number of bytes from the file
        /// </summary>
        /// <param name="count">Number of bytes to read</param>
        private void ReadBytes(int count)
        {
            if (buffer == null ||
                buffer.Length < count)
                buffer = new byte[count];


            int offset = 0;
            while (offset < count) {
                int read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new Exception(string.Format("End of stream while reading {0} bytes", count));

                offset += read;
            }
        }

        /// <summary>
        /// Read a Most Significant Byte prefix
        /// </summary>
        /// <returns>Number of bytes to read</returns>
        private int ReadMSBLengthIndicator()
        {
            ReadBytes(indicatorLength);
            int ret = 0;
            for (int i = 0; i < indicatorLength; i++)
                ret = unchecked((ret << 8) | buffer[i]);

            return ret;
        }

        /// <summary>
        /// Read a Least Significant Byte prefix
        /// </summary>
        /// <returns>Number of bytes to read</returns>
        private int ReadLSBLengthIndicator()
        {
            ReadBytes(indicatorLength);
            int ret = 0;
            for (int i = 0; i < indicatorLength; i++)
                ret = unchecked(ret | buffer[i]);

            return ret;
        }

        /// <summary>
        /// Read an ASCII record length prefix
        /// </summary>
        /// <returns>Number of bytes to read</returns>
        private int ReadASCIILengthIndicator()
        {
            ReadBytes(indicatorLength);
            return int.Parse(Encoding.ASCII.GetString(buffer, 0, indicatorLength));
        }
    }
}