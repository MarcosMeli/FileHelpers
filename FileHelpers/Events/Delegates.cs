using System;
using System.ComponentModel;

namespace FileHelpers
{

	// ----  Read Operations  ----

	#if NET_1_1
	
	/// <summary>
	/// Called in read operations just before the record string is translated to a record.
	/// </summary>
	/// <param name="engine">The engine that generates the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void BeforeReadRecordHandler(EngineBase engine, BeforeReadRecordEventArgs e);

	/// <summary>
	/// Called in read operations just after the record was created from a record string.
	/// </summary>
	/// <param name="engine">The engine that generates the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void AfterReadRecordHandler(EngineBase engine, AfterReadRecordEventArgs e);


	
	// ----  Write Operations  ----

	/// <summary>
	/// Called in write operations just before the record is converted to a string to write it.
	/// </summary>
	/// <param name="engine">The engine that generates the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void BeforeWriteRecordHandler(EngineBase engine, BeforeWriteRecordEventArgs e);

	/// <summary>
	/// Called in write operations just after the record was converted to a string.
	/// </summary>
	/// <param name="engine">The engine that generates the event.</param>
	/// <param name="e">The event data.</param>
	public delegate void AfterWriteRecordHandler(EngineBase engine, AfterWriteRecordEventArgs e);

#else

    /// <summary>
    /// Called in read operations just before the record string is translated to a record.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeReadRecordHandler<T>(EngineBase engine, BeforeReadRecordEventArgs<T> e);

    /// <summary>
    /// Called in read operations just after the record was created from a record string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterReadRecordHandler<T>(EngineBase engine, AfterReadRecordEventArgs<T> e);



    // ----  Write Operations  ----

    /// <summary>
    /// Called in write operations just before the record is converted to a string to write it.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeWriteRecordHandler<T>(EngineBase engine, BeforeWriteRecordEventArgs<T> e);

    /// <summary>
    /// Called in write operations just after the record was converted to a string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterWriteRecordHandler<T>(EngineBase engine, AfterWriteRecordEventArgs<T> e);


    
    /// <summary>
    /// Called in read operations just before the record string is translated to a record.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeReadRecordHandler(EngineBase engine, BeforeReadRecordEventArgs e);

    /// <summary>
    /// Called in read operations just after the record was created from a record string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterReadRecordHandler(EngineBase engine, AfterReadRecordEventArgs e);



    // ----  Write Operations  ----

    /// <summary>
    /// Called in write operations just before the record is converted to a string to write it.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeWriteRecordHandler(EngineBase engine, BeforeWriteRecordEventArgs e);

    /// <summary>
    /// Called in write operations just after the record was converted to a string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterWriteRecordHandler(EngineBase engine, AfterWriteRecordEventArgs e);

	#endif

}
