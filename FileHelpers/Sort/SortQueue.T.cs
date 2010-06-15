using System;
using System.IO;
using System.Text;

namespace FileHelpers
{
    internal sealed class SortQueue<T>
        :IDisposable
        where T : class
    {
        private readonly string mFile;
        private readonly bool mDeleteFile;
        public FileHelperAsyncEngine<T> Engine { get; private set; }
        public T Current { get; private set; }

        public SortQueue(Encoding enconding, string file, bool deleteFile)
        {
            mFile = file;
            mDeleteFile = deleteFile;
            Engine = new FileHelperAsyncEngine<T>(enconding);
            Engine.BeginReadFile(file, EngineBase.DefaultReadBufferSize*4);
            MoveNext();
        }

        public void MoveNext()
        {
           Current = Engine.ReadNext();
        }

        public void Dispose()
        {
            Engine.Close();
            if (mDeleteFile)
                File.Delete(mFile);
                
            GC.SuppressFinalize(this);
        }
    }
}