

namespace FileHelpers
{
    public interface IRecordReader
    {
        string ReadRecord();
        void Close();
    }
}
