

using System.IO;

namespace FileHelpers
{
    internal sealed class NewLineDelimitedRecordReader : IRecordReader
    {
        private readonly TextReader reader;

        public NewLineDelimitedRecordReader(TextReader reader)
        {
            this.reader = reader;
        }

        public string ReadRecordString()
        {
            return reader.ReadLine();
        }

        public void Close()
        {
            reader.Close();
        }
    }
}
