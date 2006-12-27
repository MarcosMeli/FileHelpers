using System;

namespace FileHelpers
{
	/// <summary>A class to read generic CSV files delimited for any char.</summary>
	public sealed class DelimitedFileEngine : FileHelperEngine
	{
		#region "  Constructor  "

		/// <summary>
		/// A FileHelperEngine with delimiters that can be changed at RinTime
		/// </summary>
		/// <remarks>
		/// Useful when you need to export the same info with 2 or more different delimieters.
		/// </remarks>
		/// <param name="recordType">The record mapping class.</param>
		public DelimitedFileEngine(Type recordType): base(recordType)
		{
			if (mRecordInfo.mFields[0] is DelimitedField == false)
				throw new BadUsageException("The Delimited Engine only accepts Record Types marked with DelimitedRecordAttribute");
		}

		#endregion

		/// <summary>The Delimiter used by this class.</summary>
		public string Delimiter
		{
			get
			{
				return ((DelimitedField) mRecordInfo.mFields[0]).mSeparator;
			}
			set
			{
				for(int i = 0; i < mRecordInfo.mFieldCount ;i++)
					((DelimitedField) mRecordInfo.mFields[i]).mSeparator = value;
			}
		}
	}
}
