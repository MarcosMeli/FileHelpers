using System.ComponentModel;

namespace FileHelpers.Events
{
    /// <summary>Base class of <see cref="BeforeWriteEventArgs{T}"/> and <see cref="AfterWriteEventArgs{T}"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class WriteEventArgs<T> 
        : FileHelpersEventArgs<T> 
        where T : class
    {
        /// <summary>
        /// Write events are based on this
        /// </summary>
        /// <param name="engine">Engine parsing data</param>
        /// <param name="record">Object we are creating / populating</param>
        /// <param name="lineNumber">Record number</param>
        internal WriteEventArgs(EventEngineBase<T> engine, T record, int lineNumber)
            :base(engine, lineNumber)
        {
            Record = record;
        }

        /// <summary>The current record.</summary>
        public T Record { get; private set; }

    }
}