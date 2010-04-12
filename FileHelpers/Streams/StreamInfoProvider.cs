using System.IO;

namespace FileHelpers
{
    internal sealed class StreamInfoProvider
    {
        private delegate long GetValue();


        //private readonly bool mHasInfo;
        //private readonly Stream mStream;
        //private readonly GetValue mLengthCalculator = () => -1;
        private readonly GetValue mPositionCalculator = () => -1;
        private readonly long mLength = -1;

        public StreamInfoProvider(TextReader reader)
        {
            if (reader is StreamReader)
            {
                var stream = ((StreamReader)reader).BaseStream;
                mLength = stream.Length;
                mPositionCalculator = () => stream.Position;
            }
            else if (reader is InternalStringReader)
            {
                var stream = (InternalStringReader)reader;
                mLength = stream.Length;
                mPositionCalculator = () => stream.Position;
            }
        }

        public StreamInfoProvider(TextWriter writer)
        {
            if (writer is StreamWriter)
            {
                var stream = ((StreamWriter)writer).BaseStream;
                mLength = stream.Length;
                mPositionCalculator = () => stream.Position;
            }

        }

        public long Position
        {
            get { return mPositionCalculator(); }
        }

        public long TotalBytes
        {
            get { return mLength; }
        }
    }
}
