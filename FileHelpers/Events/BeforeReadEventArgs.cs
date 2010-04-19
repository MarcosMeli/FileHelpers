namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="BeforeReadHandler{T}"/></summary>
    public sealed class BeforeReadEventArgs<T>
        : ReadEventArgs<T> 
        where T : class
    {

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