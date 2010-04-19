using System;
using System.Text;
using FileHelpers.Events;

namespace FileHelpers
{
    public abstract class EventEngineBase <T> 
        : EngineBase
        where T : class
    {
        public EventEngineBase(Type recordType)
            : base(recordType)
        {
        }

        public EventEngineBase(Type recordType, Encoding encoding)
            : base(recordType, encoding)
        {
        }

        internal EventEngineBase(RecordInfo ri)
            : base(ri)
        {
        }

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadHandler<T> BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadHandler<T> AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteHandler<T> BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteHandler<T> AfterWriteRecord;

        protected bool MustNotifyRead
        {
            get
            {
                return BeforeReadRecord != null ||
                       AfterReadRecord != null ||
                       mRecordInfo.NotifyRead;
            }
        }

        protected bool MustNotifyWrite
        {
            get
            {
                return BeforeWriteRecord != null ||
                       AfterWriteRecord != null ||
                       mRecordInfo.NotifyWrite;
            }
        }
#if ! MINI

        protected bool OnBeforeReadRecord(BeforeReadEventArgs<T> e)
        {
            if (mRecordInfo.NotifyRead)
                ((INotifyRead<T>)e.Record).BeforeRead(e);

            if (BeforeReadRecord != null)
                BeforeReadRecord(this, e);

            return e.SkipThisRecord;
        }

        protected bool OnAfterReadRecord(string line, T record, bool lineChanged, int lineNumber)
        {
            var e = new AfterReadEventArgs<T>(this, line, lineChanged, record, lineNumber);

            if (mRecordInfo.NotifyRead)
                ((INotifyRead<T>)record).AfterRead(e);

            if (AfterReadRecord != null)
                AfterReadRecord(this, e);

            return e.SkipThisRecord;
        }

        protected bool OnBeforeWriteRecord(T record, int lineNumber)
        {
            var e = new BeforeWriteEventArgs<T>(this, record, lineNumber);

            if (mRecordInfo.NotifyWrite)
                ((INotifyWrite<T>)record).BeforeWrite(e);

            if (BeforeWriteRecord != null)
                BeforeWriteRecord(this, e);

            return e.SkipThisRecord;
        }

        protected string OnAfterWriteRecord(string line, T record)
        {
            var e = new AfterWriteEventArgs<T>(this, record, LineNumber, line);

            if (mRecordInfo.NotifyWrite)
                ((INotifyWrite<T>)record).AfterWrite(e);

            if (AfterWriteRecord != null)
                AfterWriteRecord(this, e);

            return e.RecordLine;
        }

#endif

    }
}