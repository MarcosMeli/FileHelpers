namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public sealed class AfterReadEventArgs<T> 
        : ReadEventArgs<T>
        where T : class
    {
        //internal AfterReadEventArgs(EventEngineBase<T> engine, string line, bool lineChanged, T newRecord)
        //    : this(engine, line, lineChanged, newRecord, -1)
        //{}

        internal AfterReadEventArgs(EventEngineBase<T> engine, string line, bool lineChanged, T newRecord, int lineNumber)
            : base(engine, line, lineNumber)
		{
		    SkipThisRecord = false;
		    Record = newRecord;
            RecordLineChanged = lineChanged;
		}

        /// <summary>The current record.</summary>
        public T Record { get; set; }

	}


}
