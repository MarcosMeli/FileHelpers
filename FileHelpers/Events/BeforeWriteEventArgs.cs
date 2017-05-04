namespace FileHelpers.Events
{

    /// <summary>Arguments for the <see cref="BeforeWriteHandler{T}"/></summary>
    public class BeforeWriteEventArgs
        : WriteEventArgs
    {
        /// <summary>
        /// Check record just before processing.
        /// </summary>
        /// <param name="engine">Engine that will parse record</param>
        /// <param name="lineNumber">line number to be parsed</param>
        internal BeforeWriteEventArgs(EngineBase engine, int lineNumber)
            : base(engine, lineNumber)
        {
            SkipThisRecord = false;
        }

        /// <summary>Set this property as true if you want to bypass the current record.</summary>
        public bool SkipThisRecord { get; set; }
    }

    /// <summary>Arguments for the <see cref="BeforeWriteHandler{T}"/></summary>
    /// <typeparam name="T">Object type we are writing from</typeparam>
    public sealed class BeforeWriteEventArgs<T>
        : BeforeWriteEventArgs
        where T : class
    {
        /// <summary>
        /// Check record just before processing.
        /// </summary>
        /// <param name="engine">Engine that will parse record</param>
        /// <param name="record">object to be created</param>
        /// <param name="lineNumber">line number to be parsed</param>
        internal BeforeWriteEventArgs(EngineBase engine, T record, int lineNumber)
            : base(engine, lineNumber)
        {
            Record = record;
        }

        /// <summary>The current record.</summary>
        public T Record { get; private set; }

    }
}