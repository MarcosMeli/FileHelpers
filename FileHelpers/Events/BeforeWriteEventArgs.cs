namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="BeforeWriteHandler{T}"/></summary>
    public sealed class BeforeWriteEventArgs<T>
        : WriteEventArgs<T>
        where T : class
    {
        internal BeforeWriteEventArgs(EventEngineBase<T> engine, T record, int lineNumber)
            : base(engine, record, lineNumber)
        {
            SkipThisRecord = false;
        }

        /// <summary>Set this property as true if you want to bypass the current record.</summary>
        public bool SkipThisRecord { get; set; }
    }
}