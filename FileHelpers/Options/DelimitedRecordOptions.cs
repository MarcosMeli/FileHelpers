using System;

namespace FileHelpers
{
	public sealed class DelimitedRecordOptions: RecordOptions
	{
		
		internal DelimitedRecordOptions(RecordInfo info)
				:base(info)
		{
	}
		
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
