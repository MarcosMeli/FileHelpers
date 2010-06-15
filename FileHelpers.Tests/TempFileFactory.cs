using System;
using System.IO;

namespace FileHelpers.Tests
{
    public class TempFileFactory
        :IDisposable
    {
        public TempFileFactory()
        {
            FileName = Path.GetTempFileName();
        }

        public string FileName { get; set; }
        public void Dispose()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            
        }

        public static implicit operator string(TempFileFactory factory)
        {
            return factory.FileName;
        }
    }
}