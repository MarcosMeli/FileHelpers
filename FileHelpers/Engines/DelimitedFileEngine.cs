using System;
using System.Diagnostics;

namespace FileHelpers
{
	/// <summary>
	/// Is a version of the <see cref="FileHelperEngine"/> exclusive for 
	/// delimited records that allow you to change the delimiter an other options at runtime
	/// </summary>
	/// <remarks>
	/// Useful when you need to export or import the same info with 2 or more different delimiters or little different options.
	/// </remarks>
#if NET_2_0
    [DebuggerDisplay("DelimitedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
    public sealed class DelimitedFileEngine : FileHelperEngine
    {

        /// <summary>
        /// Create a version of the <see cref="FileHelperEngine"/> exclusive for 
        /// delimited records that allow you to change the delimiter an other options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with 2 or more different delimiters.
        /// </remarks>
        /// <param name="recordType">The record mapping class.</param>
        public DelimitedFileEngine(Type recordType)
            : base(recordType)
        {
            if (mRecordInfo.mFields[0] is DelimitedField == false)
                throw new BadUsageException("The Delimited Engine only accepts record types marked with DelimitedRecordAttribute");
        }

		
		/// <summary>Allow changes in the record layout like delimiters and others common settings.</summary>
		public new DelimitedRecordOptions Options
		{
			get { return (DelimitedRecordOptions) mOptions; }
			
		}
	}


#if NET_2_0

	/// <summary>
	/// Is a version of the <see cref="FileHelperEngine"/> exclusive for 
	/// delimited records that allow you to change the delimiter an other options at runtime
	/// </summary>
	/// <remarks>
	/// Useful when you need to export or import the same info with 2 or more different delimiters or little different options.
	/// </remarks>
#if NET_2_0
    [DebuggerDisplay("DelimitedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
    public sealed class DelimitedFileEngine<T> : FileHelperEngine<T>
	{
	#region "  Constructor  "

		/// <summary>
		/// Create a version of the <see cref="FileHelperEngine"/> exclusive for 
		/// delimited records that allow you to change the delimiter an other options at runtime
		/// </summary>
		/// <remarks>
		/// Useful when you need to export or import the same info with 2 or more different delimiters.
		/// </remarks>
		public DelimitedFileEngine()
			: base()
		{
			if (mRecordInfo.mFields[0] is DelimitedField == false)
				throw new BadUsageException("The Delimited Engine only accepts Record Types marked with DelimitedRecordAttribute");
		}

	#endregion

		
		/// <summary>Allow changes in the record layout like delimiters and others common settings.</summary>
        public new DelimitedRecordOptions Options
		{
            get { return (DelimitedRecordOptions) mOptions; }
			
		}
	}

#endif
}
