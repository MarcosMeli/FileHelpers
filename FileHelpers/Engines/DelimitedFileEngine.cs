using System;

namespace FileHelpers
{
	public sealed class DelimitedFileEngine : FileHelperEngine
	{
		#region "  Constructor  "

		/// <summary>
		/// A FileHelperEngine with delimiters that can be changed at RunTime
		/// </summary>
		/// <remarks>
		/// Useful when you need to export the same info with 2 or more different delimiters.
		/// </remarks>
		/// <param name="recordType">The record mapping class.</param>
		public DelimitedFileEngine(Type recordType)
			: base(recordType)
		{
			if (mRecordInfo.mFields[0] is DelimitedField == false)
				throw new BadUsageException("The Delimited Engine only accepts Record Types marked with DelimitedRecordAttribute");
		}

		#endregion

		
		/// <summary>Allow changes in the record layout like delimiters and others settings.</summary>
		public new DelimitedRecordOptions Options
		{
			get { return (DelimitedRecordOptions) mOptions; }
			
		}
	}
}
