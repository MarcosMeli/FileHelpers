using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FileHelpers.Events;

namespace FileHelpers
{
    /// <summary>
    /// Base for engine events
    /// </summary>
    /// <typeparam name="T">Specific engine</typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class EventEngineBase<T>
        : EngineBase
        where T : class
    {
        /// <summary>
        /// Define an event based on an egine, based on a record
        /// </summary>
        /// <param name="recordType">Type of the record</param>
        protected EventEngineBase(Type recordType)
            : base(recordType) {}

        /// <summary>
        /// Define an event based on a record with a specific encoding
        /// </summary>
        /// <param name="recordType">Type of the record</param>
        /// <param name="encoding">Encoding specified</param>
        protected EventEngineBase(Type recordType, Encoding encoding)
            : base(recordType, encoding) {}

        /// <summary>
        /// Event based upon supplied record information
        /// </summary>
        /// <param name="ri"></param>
        internal EventEngineBase(RecordInfo ri)
            : base(ri) {}

        /// <summary>
        /// Called in read operations just before the record string is
        /// translated to a record.
        /// </summary>
        public event BeforeReadHandler<T> BeforeReadRecord;

        /// <summary>
        /// Called in read operations just after the record was created from a
        /// record string.
        /// </summary>
        public event AfterReadHandler<T> AfterReadRecord;

        /// <summary>
        /// Called in write operations just before the record is converted to a
        /// string to write it.
        /// </summary>
        public event BeforeWriteHandler<T> BeforeWriteRecord;

        /// <summary>
        /// Called in write operations just after the record was converted to a
        /// string.
        /// </summary>
        public event AfterWriteHandler<T> AfterWriteRecord;

        /// <summary>
        /// Check whether we need to notify the read to anyone
        /// </summary>
        protected bool MustNotifyRead
        {
            get
            {
                return BeforeReadRecord != null ||
                       AfterReadRecord != null ||
                       RecordInfo.NotifyRead;
            }
        }

        /// <summary>
        /// Determine whether we have to run notify write on every iteration
        /// </summary>
        protected bool MustNotifyWrite
        {
            get
            {
                return BeforeWriteRecord != null ||
                       AfterWriteRecord != null ||
                       RecordInfo.NotifyWrite;
            }
        }


        /// <summary>
        /// Provide a hook to preprocess a record
        /// </summary>
        /// <param name="e">Record details before read</param>
        /// <returns>True if record to be skipped</returns>
        protected bool OnBeforeReadRecord(BeforeReadEventArgs<T> e)
        {
            if (RecordInfo.NotifyRead)
                ((INotifyRead<T>) e.Record).BeforeRead(e);

            if (BeforeReadRecord != null)
                BeforeReadRecord(this, e);

            return e.SkipThisRecord;
        }

        /// <summary>
        /// Post process a record
        /// </summary>
        /// <param name="line">Record read</param>
        /// <param name="record">Type of record</param>
        /// <param name="lineChanged">Has the line been updated so that the engine switches to this version</param>
        /// <param name="lineNumber">Number of line in file</param>
        /// <returns>true if record to be skipped</returns>
        protected bool OnAfterReadRecord(string line, T record, bool lineChanged, int lineNumber)
        {
            var e = new AfterReadEventArgs<T>(this, line, lineChanged, record, lineNumber);

            if (RecordInfo.NotifyRead)
                ((INotifyRead<T>) record).AfterRead(e);

            if (AfterReadRecord != null)
                AfterReadRecord(this, e);

            return e.SkipThisRecord;
        }

        /// <summary>
        /// Before a write is executed perform this check to see
        /// if we want to modify or reject the record.
        /// </summary>
        /// <param name="record">Instance to process</param>
        /// <param name="lineNumber">Number of line within file</param>
        /// <returns>true if record is to be dropped</returns>
        protected bool OnBeforeWriteRecord(T record, int lineNumber)
        {
            var e = new BeforeWriteEventArgs<T>(this, record, lineNumber);

            if (RecordInfo.NotifyWrite)
                ((INotifyWrite<T>) record).BeforeWrite(e);

            if (BeforeWriteRecord != null)
                BeforeWriteRecord(this, e);

            return e.SkipThisRecord;
        }

        /// <summary>
        /// After we have written a record,  do we want to process it.
        /// </summary>
        /// <param name="line">Line that will be output</param>
        /// <param name="record">Record we are processing</param>
        /// <returns>Record to be written</returns>
        protected string OnAfterWriteRecord(string line, T record)
        {
            var e = new AfterWriteEventArgs<T>(this, record, LineNumber, line);

            if (RecordInfo.NotifyWrite)
                ((INotifyWrite<T>) record).AfterWrite(e);

            if (AfterWriteRecord != null)
                AfterWriteRecord(this, e);

            return e.RecordLine;
        }


    }
}