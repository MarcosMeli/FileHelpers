using System;
using System.IO;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// One sorted 'chunk' of an input file.
    /// </summary>
    /// <typeparam name="T">object type we are sorting</typeparam>
    internal sealed class SortQueue<T>
        :IDisposable
        where T : class
    {
        private readonly string mFile;
        private readonly bool mDeleteFile;
        public FileHelperAsyncEngine<T> Engine { get; private set; }
        public T Current { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="encoding">encoding of the file</param>
        /// <param name="file">filename of the chunk</param>
        /// <param name="deleteFile">do we remove file after afterwards</param>
        public SortQueue(Encoding encoding, string file, bool deleteFile)
        {
            mFile = file;
            mDeleteFile = deleteFile;
            Engine = new FileHelperAsyncEngine<T>(encoding);
            Engine.BeginReadFile(file, EngineBase.DefaultReadBufferSize*4);
            MoveNext();
        }

        /// <summary>
        /// Move to the next record along, sets current
        /// </summary>
        public void MoveNext()
        {
           Current = Engine.ReadNext();
        }

        /// <summary>
        /// close engine and if requested delete the file
        /// </summary>
        public void Dispose()
        {
            Engine.Close();
            if (mDeleteFile)
                File.Delete(mFile);

            GC.SuppressFinalize(this);
        }
    }
}