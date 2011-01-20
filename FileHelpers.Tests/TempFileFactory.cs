using System;
using System.IO;

namespace FileHelpers.Tests
{
    /// <summary>
    /// Create a temporary filename for testing and delete it automatically after use
    /// </summary>
    /// <example>
    /// using (var filename = new TempFileFactory())
    /// {
    ///      // ProcessAppend expects a string filename and gets one...
    ///      ProcessAppend(testdata, engine, filename, twoRecords, "\r\n\r\n", "Dos");
    ///      
    /// }  //  File is automatically deleted at end of using statement
    /// </example>
    public class TempFileFactory
        : IDisposable
    {
        /// <summary>
        /// Create a temporary file for testing, on disposal will clean it up
        /// </summary>
        public TempFileFactory()
        {
            FileName = Path.GetTempFileName();
        }

        /// <summary>
        /// Temporary filename
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Clean up file from system
        /// </summary>
        public void Dispose()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
        }

        /// <summary>
        /// Allow filename factory to return string value for direct use
        /// </summary>
        /// <param name="factory">Tempfile to convert to string</param>
        /// <returns>Filename as text</returns>
        public static implicit operator string(TempFileFactory factory)
        {
            return factory.FileName;
        }
    }
}