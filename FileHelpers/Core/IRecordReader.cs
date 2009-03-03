#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
    public interface IRecordReader
    {
        string ReadRecord();
        void Close();
    }
}
