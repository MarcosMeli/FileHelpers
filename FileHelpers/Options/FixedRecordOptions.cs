using System;

namespace FileHelpers
{
	/// <summary>
	/// This class allows you to set some options of the fixed length records but at runtime.
	/// With this options the library is more flexible than never.
	/// </summary>
	public sealed class FixedRecordOptions: RecordOptions
	{
		
		internal FixedRecordOptions(RecordInfo info)
				:base(info)
		{
		}

		public FixedMode FixedMode
		{
			get
			{
				return ((FixedLengthField) mRecordInfo.mFields[0]).mFixedMode;
			}
			set
			{
				for(int i = 0; i < mRecordInfo.mFieldCount; i++)
				{
					((FixedLengthField) mRecordInfo.mFields[i]).mFixedMode = value;
				}
			}
		}
		
	
	}
}
