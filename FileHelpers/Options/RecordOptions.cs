using System;
using System.Diagnostics;
using System.ComponentModel;

namespace FileHelpers
{
	/// <summary>
	/// This class allows you to set some options of the records but at runtime.
	/// With this options the library is more flexible than never.
	/// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class RecordOptions
	{
		
#if NET_2_0
    [DebuggerDisplay("FileHelperEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
        internal RecordInfo mRecordInfo;
	
		internal RecordOptions(RecordInfo info)
		{
			mRecordInfo = info;
            mRecordConditionInfo = new RecordConditionInfo(info);
		}
		
		/// <summary>Indicates the number of first lines to be discarded.</summary>
		public int IgnoreFirstLines
		{
			get { return mRecordInfo.mIgnoreFirst; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreFirst= value;
			}
		}

		/// <summary>Indicates the number of lines at the end of file to be discarded.</summary>
		public int IgnoreLastLines
		{
			get { return mRecordInfo.mIgnoreLast; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreLast = value;
			}
		}

		/// <summary>Indicates that the engine must ignore the empty lines while reading.</summary>
		public bool IgnoreEmptyLines
		{
			get { return mRecordInfo.mIgnoreEmptyLines; }
			set { mRecordInfo.mIgnoreEmptyLines= value; }
		}

        /// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        public RecordConditionInfo RecordCondition
        {
            get 
            {
                return mRecordConditionInfo;
            }
        }

        private RecordConditionInfo mRecordConditionInfo;


        /// <summary>Indicates that the engine must ignore the lines with this comment marker.</summary>
        public string CommentMarker
        {
            get { return mRecordInfo.mCommentMarker; }
            set { mRecordInfo.mCommentMarker = value; }
        }

        /// <summary>Indicates if the comment can have spaces or tabs at left (true by default)</summary>
        public bool CommentInAnyPlace
        {
            get { return mRecordInfo.mCommentAnyPlace; }
            set { mRecordInfo.mCommentAnyPlace = value; }
        }


        /// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        public sealed class RecordConditionInfo
        {
            RecordInfo mRecordInfo;
            internal RecordConditionInfo(RecordInfo ri)
            {
                mRecordInfo = ri;
            }

            /// <summary>The condition used to include or exclude records.</summary>
            public RecordCondition Condition
            {
                get { return mRecordInfo.mRecordCondition; }
                set { mRecordInfo.mRecordCondition = value; }
            }

            /// <summary>The selector used by the <see cref="RecordCondition"/>.</summary>
            public string Selector
            {
                get { return mRecordInfo.mRecordConditionSelector; }
                set { mRecordInfo.mRecordConditionSelector = value; }
            }
        }

    }

}
