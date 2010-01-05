

using System.IO;

namespace FileHelpers
{
    public sealed class NewLineDelimitedRecordReader : IRecordReader
    {
        private readonly TextReader reader;

        public NewLineDelimitedRecordReader(TextReader reader)
        {
            this.reader = reader;
        }

        public string ReadRecord()
        {
            return reader.ReadLine();
        }

        public void Close()
        {
            reader.Close();
        }
    }
}
