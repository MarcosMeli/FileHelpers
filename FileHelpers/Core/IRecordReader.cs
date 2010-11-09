

namespace FileHelpers
{
    /// <summary>
    /// Basic read record interface
    /// </summary>
    public interface IRecordReader
    {
        /// <summary>
        /// Read a record from the data source
        /// </summary>
        /// <returns>A single record for parsing</returns>
        string ReadRecordString();

        /// <summary>
        /// close the interface and return
        /// </summary>
        void Close();
    }
}
