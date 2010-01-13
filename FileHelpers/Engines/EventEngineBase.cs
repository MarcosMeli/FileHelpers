using System;
using System.Text;
using FileHelpers.Events;

namespace FileHelpers
{
    public abstract class EventEngineBase <T> 
        : EngineBase
        where T : class
    {
        public EventEngineBase(Type recordType) : base(recordType)
        {
        }

        public EventEngineBase(Type recordType, Encoding encoding) : base(recordType, encoding)
        {
        }

        internal EventEngineBase(RecordInfo ri) : base(ri)
        {
        }

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadRecordHandler<T> BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadRecordHandler<T> AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteRecordHandler<T> BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteRecordHandler<T> AfterWriteRecord;

#if ! MINI

        protected bool OnBeforeReadRecord(BeforeReadRecordEventArgs<T> e)
        {

            if (BeforeReadRecord != null)
            {
                BeforeReadRecord(this, e);

                return e.SkipThisRecord;
            }

            return false;
        }

        protected bool OnAfterReadRecord(string line, T record, bool lineChanged)
        {
            if (mRecordInfo.NotifyRead)
                ((INotifyRead)record).AfterRead(this, line);

            if (AfterReadRecord != null)
            {
                AfterReadRecordEventArgs<T> e = null;
                e = new AfterReadRecordEventArgs<T>(line, lineChanged, record, LineNumber);
                AfterReadRecord(this, e);
                return e.SkipThisRecord;
            }

            return false;
        }

        protected bool OnBeforeWriteRecord(T record)
        {
            if (mRecordInfo.NotifyWrite)
                ((INotifyWrite)record).BeforeWrite(this);

            if (BeforeWriteRecord != null)
            {
                BeforeWriteRecordEventArgs<T> e = null;
                e = new BeforeWriteRecordEventArgs<T>(record, LineNumber);
                BeforeWriteRecord(this, e);

                return e.SkipThisRecord;
            }

            return false;
        }

        protected string OnAfterWriteRecord(string line, T record)
        {

            if (AfterWriteRecord != null)
            {
                AfterWriteRecordEventArgs<T> e = null;
                e = new AfterWriteRecordEventArgs<T>(record, LineNumber, line);
                AfterWriteRecord(this, e);
                return e.RecordLine;
            }
            return line;
        }

#endif

    }
}