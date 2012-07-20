using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileHelpers
{
    /// <summary>
    /// Calculate statistics on stream,  position and total size
    /// </summary>
    internal sealed class StreamInfoProvider
    {
        /// <summary>
        /// Delegate to the stream values returned
        /// </summary>
        /// <returns></returns>
        private delegate long GetValue();

        /// <summary>
        /// Position within the stream -1 is beginning
        /// </summary>
        private readonly GetValue mPositionCalculator = () => -1;

        /// <summary>
        /// Length of the stream -1 is unknown
        /// </summary>
        private readonly long mLength = -1;

        /// <summary>
        /// Provide as much information about the input stream as we can,  size
        /// and position
        /// </summary>
        /// <param name="reader">reader we are analysing</param>
        public StreamInfoProvider(TextReader reader)
        {
            if (reader is StreamReader)
            {
                var stream = ((StreamReader)reader).BaseStream;
                mLength = stream.Length;
                // Uses the buffer position
                mPositionCalculator = () => stream.Position;
            }
            else if (reader is InternalStreamReader)
            {
                var reader2 = ((InternalStreamReader)reader);
                var stream = reader2.BaseStream;

                mLength = stream.Length;
                // Real Position
                mPositionCalculator = () => reader2.Position;
            }
            else if (reader is InternalStringReader)
            {
                var stream = (InternalStringReader)reader;
                mLength = stream.Length;
                mPositionCalculator = () => stream.Position;
            }
        }

        /// <summary>
        /// Provide as much information about the output stream as we can,
        /// size and position
        /// </summary>
        /// <param name="writer">writer we are analysing</param>
        public StreamInfoProvider(TextWriter writer)
        {
            var streamWriter = writer as StreamWriter;
            if (streamWriter == null)
                return;

            var stream = streamWriter.BaseStream;
            mLength = stream.Length;
            mPositionCalculator = () => stream.Position;
        }

        /// <summary>
        /// Position within the stream
        /// </summary>
        public long Position
        {
            get { return mPositionCalculator(); }
        }

        /// <summary>
        /// Total number of bytes within the stream
        /// </summary>
        public long TotalBytes
        {
            get { return mLength; }
        }
    }
}
