namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public class AfterReadEventArgs
        : ReadEventArgs
        
    {
        /// <summary>
        /// After the record is read,  allow details to be inspected.
        /// </summary>
        /// <param name="engine">Engine that parsed the record</param>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineChanged">Was it changed before</param>
        /// <param name="lineNumber">Record number read</param>
        internal AfterReadEventArgs(EngineBase engine,
            string line,
            bool lineChanged,
            int lineNumber)
            : base(engine, line, lineNumber)
        {
            SkipThisRecord = false;
            RecordLineChanged = lineChanged;
        }

    }

    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public sealed class AfterReadEventArgs<T>
        : AfterReadEventArgs
        where T : class
    {
        /// <summary>
        /// After the record is read,  allow details to be inspected.
        /// </summary>
        /// <param name="engine">Engine that parsed the record</param>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineChanged">Was it changed before</param>
        /// <param name="newRecord">Object created</param>
        /// <param name="lineNumber">Record number read</param>
        internal AfterReadEventArgs(EventEngineBase<T> engine,
            string line,
            bool lineChanged,
            T newRecord,
            int lineNumber)
            : base(engine, line, lineChanged, lineNumber)
        {
            Record = newRecord;
        }

        /// <summary>The current record.</summary>
        public T Record { get; set; }
    }
}