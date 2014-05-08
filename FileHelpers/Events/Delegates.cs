using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{

	// ----  Read Operations  ----
    /// <summary>
    /// Called in read operations just before the record string is translated to a record.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeReadHandler<T>(EngineBase engine, BeforeReadEventArgs<T> e) where T : class;

    /// <summary>
    /// Called in read operations just after the record was created from a record string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterReadHandler<T>(EngineBase engine, AfterReadEventArgs<T> e) where T : class;



    // ----  Write Operations  ----
    /// <summary>
    /// Called in write operations just before the record is converted to a string to write it.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void BeforeWriteHandler<T>(EngineBase engine, BeforeWriteEventArgs<T> e) where T : class;

    /// <summary>
    /// Called in write operations just after the record was converted to a string.
    /// </summary>
    /// <param name="engine">The engine that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void AfterWriteHandler<T>(EngineBase engine, AfterWriteEventArgs<T> e) where T : class;



    // ---- Error Operations  ----
    /// <summary>
    /// Called on extract field exception
    /// </summary>
    /// <param name="sender">The sender that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void ReadFieldErrorHandler(object sender, ReadFieldErrorEventArgs e);

    /// <summary>
    /// Called on read line containing errors
    /// </summary>
    /// <param name="sender">The sender that generates the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void ReadLineErrorHandler(object sender, ReadLineErrorEventArgs e);

}
