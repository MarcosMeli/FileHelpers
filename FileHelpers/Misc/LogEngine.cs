using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers
{
    public class LogEngine
        :IDisposable
    {

        System.IO.StreamWriter mWriter;

		public LogEngine(string filename)
            : this(filename, LogEngineMode.CreateNew)
        { }

        public LogEngine(string filename, LogEngineMode mode)
            : this(filename, mode, Encoding.Default)
        { }

        public LogEngine(string filename, LogEngineMode mode, Encoding encoding)
        {

            switch (mode)
            {
                case LogEngineMode.CreateNew:
                    if (System.IO.File.Exists(filename))
                        throw new FileHelpersException("The file: " + filename + " already exists try using the LogEngineMode.Override or LogEngineMode.Append");

                    mWriter = new System.IO.StreamWriter(filename, false, encoding);
                    break;
                case LogEngineMode.Override:
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);
                    mWriter = new System.IO.StreamWriter(filename, false, encoding);
                    break;
                case LogEngineMode.Append:
                    mWriter = new System.IO.StreamWriter(filename, true, encoding);
                    break;
            }
            
        }

        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        public void Flush()
        {
            if (mWriter != null)
                mWriter.Flush();
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (mWriter != null)
                mWriter.Close();

            mWriter = null;
        }

        #endregion


        private string mPrefix;

        public string Prefix
        {
            get { return mPrefix; }
            set { mPrefix = value; }
        }


        private string mDelimiter = "\t";

        public string Delimiter
        {
            get { return mDelimiter; }
            set { mDelimiter = value; }
        }

        public void Log(params object[] values)
        {
            mWriter.Write(Prefix);
            for (int i = 0; i < values.Length; i++)
            {
                if (i != 0 || ! string.IsNullOrEmpty(Prefix))
                    mWriter.Write(Delimiter);

                if (values[i] != null)
                mWriter.Write(values[i].ToString());
            }
            mWriter.Write(Environment.NewLine);
        }
    }
    public enum LogEngineMode
    { 
        CreateNew,
        Override,
        Append
    }
}
