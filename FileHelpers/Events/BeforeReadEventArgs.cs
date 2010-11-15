namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="BeforeReadHandler{T}"/></summary>
    public sealed class BeforeReadEventArgs<T>
        : ReadEventArgs<T> 
        where T : class
    {

        /// <summary>
        /// Record before being parsed by the engine
        /// </summary>
        /// <param name="engine">Engine that will analyse the record</param>
        /// <param name="record">Object to be created</param>
        /// <param name="line">Record read from the source</param>
        /// <param name="lineNumber">record number read</param>
        internal BeforeReadEventArgs(EventEngineBase<T> engine, T record, string line, int lineNumber)
            : base(engine, line, lineNumber)
        {
            Record = record;
            SkipThisRecord = false;
        }

        /// <summary>The current record that was just assigned not yet filled</summary>
        public T Record { get; private set; }

    }
}