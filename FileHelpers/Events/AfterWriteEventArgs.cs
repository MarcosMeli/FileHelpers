namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterWriteEventArgs{T}"/></summary>
    public sealed class AfterWriteEventArgs<T>
        : WriteEventArgs<T>
        where T : class
    {
		internal AfterWriteEventArgs(EventEngineBase<T> engine, T record, int lineNumber, string line)
            : base(engine, record, lineNumber)
		{
			RecordLine = line;
		}

	    /// <summary>The line to be written to the destination. WARNING: you can change the line value and the engines will write it to the destination.</summary>
	    public string RecordLine { get; set; }
	}

}
