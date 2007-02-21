using System;

namespace FileHelpers
{
	/// <summary>
	/// This class allows you to set some options of the records but at runtime.
	/// With this options the library is more flexible than never.
	/// </summary>
	public abstract class RecordOptions
	{
		
		internal RecordInfo mRecordInfo;
	
		internal RecordOptions(RecordInfo info)
		{
			mRecordInfo = info;
		}
		
		public int IgnoreFirstLines
		{
			get { return mRecordInfo.mIgnoreFirst; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreFirst= value;
			}
		}

		public int IgnoreLastLines
		{
			get { return mRecordInfo.mIgnoreLast; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreLast = value;
			}
		}

		
		public bool IgnoreEmptyLines
		{
			get { return mRecordInfo.mIgnoreEmptyLines; }
			set { mRecordInfo.mIgnoreEmptyLines= value; }
		}

		public RecordCondition RecordCondition
		{
			get { return mRecordInfo.mRecordCondition; }
			set { mRecordInfo.mRecordCondition = value; }
		}

		public string RecordConditionSelector
		{
			get { return mRecordInfo.mRecordConditionSelector; }
			set { mRecordInfo.mRecordConditionSelector = value; }
		}

	}
}
