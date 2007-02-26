using System;

namespace FileHelpers
{
	/// <summary>
	/// This class allows you to set some options of the delimited records but at runtime.
	/// With this options the library is more flexible than never.
	/// </summary>
	public sealed class DelimitedRecordOptions: RecordOptions
	{
		
		internal DelimitedRecordOptions(RecordInfo info)
				:base(info)
		{
	}
		
		/// <summary>
		/// The delimiter used to identify each field in the data.
		/// </summary>
		public string Delimiter
		{
			get
			{
				return ((DelimitedField) mRecordInfo.mFields[0]).Separator;
			}
			set
			{
				for(int i = 0; i < mRecordInfo.mFieldCount ;i++)
					((DelimitedField) mRecordInfo.mFields[i]).Separator = value;
			}

		}

	}
}
