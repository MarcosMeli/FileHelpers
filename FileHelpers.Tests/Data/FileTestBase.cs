using System.Collections.Generic;
using FileHelpers;
using System;
using System.IO;

namespace FileHelpers
{
     public abstract class FileTestBase
    {
        protected abstract string GetFullPathName();

        public string Path
        {
            get
            {
                return System.IO.Path.Combine(System.IO.Path.GetFullPath(System.IO.Path.Combine("..", "Data")), GetFullPathName());
            }
        }

        public T Execute<T>(Func<string, T> func)
        {
            return func(Path);
        }

        public T[] ReadWithEngine<T>() where T : class
        {
            var engine = new FileHelperEngine<T>();
            return ReadWithEngine(engine);
        }

        public FileHelperAsyncEngine<T> BeginRead<T>() where T : class
        {
            var engine = new FileHelperAsyncEngine<T>();
            engine.BeginReadFile(Path);
            return engine;
        }

        public T[] ReadWithEngine<T>(FileHelperEngine<T> engine) where T : class
        {
            return engine.ReadFile(Path);
        }

        public T[] ReadWithAsyncEngine<T>() where T : class
        {
            var engine = new FileHelperAsyncEngine<T>();
            return ReadWithAsyncEngine(engine);
        }

        public T[] ReadWithAsyncEngine<T>(FileHelperAsyncEngine<T> engine) where T : class
        {
            var res = new List<T>();

            using (engine.BeginReadFile(Path))
            {
                while (engine.ReadNext() != null)
                    res.Add(engine.LastRecord);
            }

            return res.ToArray();
        }

    }
}
