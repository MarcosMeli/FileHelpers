using System;
using System.Diagnostics;

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


		/// <summary>Indicates the behavior when variable length records are found in a [<see cref="FixedLengthRecordAttribute"/>]. (Note: nothing in common with [FieldOptional])</summary>
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

#if NET_2_0
        [DebuggerDisplay("FileHelperEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
        private int mRecordLength = int.MinValue;

        /// <summary>
        /// The sum of the indivial field lengths.
        /// </summary>
        public int RecordLength
        {
            get 
            {
                if (mRecordLength != int.MinValue)
                    return mRecordLength;

                mRecordLength = 0;
                foreach (FixedLengthField field in mRecordInfo.mFields)
                {
                    mRecordLength += field.mFieldLength;
                }

                return mRecordLength;
            }
        }

		
	
	}
}
