

namespace FileHelpers
{
    public interface IRecordReader
    {
        string ReadRecordString();
        void Close();
    }
}
