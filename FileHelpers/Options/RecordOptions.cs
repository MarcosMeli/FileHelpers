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
			mIgnoreCommentInfo = new IgnoreCommentInfo(info);
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

		private RecordConditionInfo mRecordConditionInfo;
		
		/// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        public RecordConditionInfo RecordCondition
        {
            get { return mRecordConditionInfo; }
        }


		private IgnoreCommentInfo mIgnoreCommentInfo;

		/// <summary>Indicates that the engine must ignore the lines with this comment marker.</summary>
		public IgnoreCommentInfo IgnoreCommentedLines
		{
			get { return mIgnoreCommentInfo; }
		}
 
        /// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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



		/// <summary>Indicates that the engine must ignore the lines with this comment marker.</summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public sealed class IgnoreCommentInfo
		{
			RecordInfo mRecordInfo;
			internal IgnoreCommentInfo(RecordInfo ri)
			{
				mRecordInfo = ri;
			}

			/// <summary>
			/// <para>Indicates that the engine must ignore the lines with this comment marker.</para>
			/// <para>An emty string or null indicates that the engine dont look for comments</para>
			/// </summary>
			public string CommentMarker
			{
				get { return mRecordInfo.mCommentMarker; }
				set
				{
					if (value != null)
						value = value.Trim();
					mRecordInfo.mCommentMarker = value;
				}
			}

			/// <summary>Indicates if the comment can have spaces or tabs at left (true by default)</summary>
			public bool InAnyPlace
			{
				get { return mRecordInfo.mCommentAnyPlace; }
				set { mRecordInfo.mCommentAnyPlace = value; }
			}
		}
    }

}
