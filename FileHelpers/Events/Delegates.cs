using System;
using System.ComponentModel;

namespace FileHelpers
{

	// ----  Read Operations  ----

	/// <summary>Called in read operations just before the record string is translated to a record.</summary>
	public delegate void BeforeReadRecordHandler(EngineBase engine, BeforeReadRecordEventArgs e);
	/// <summary>Called in read operations just after the record was created from a record string.</summary>
	public delegate void AfterReadRecordHandler(EngineBase engine, AfterReadRecordEventArgs e);


	// ----  Write Operations  ----

	/// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
	public delegate void BeforeWriteRecordHandler(EngineBase engine, BeforeWriteRecordEventArgs e);
	/// <summary>Called in write operations just after the record was converted to a string.</summary>
	public delegate void AfterWriteRecordHandler(EngineBase engine, AfterWriteRecordEventArgs e);



}
