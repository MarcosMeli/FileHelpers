using System.Diagnostics;

namespace FileHelpers.Options
{
    /// <summary>
    /// This class allows you to set some options of the fixed length records
    /// but at runtime.
    /// With this options the library is more flexible than never.
    /// </summary>
    public sealed class FixedRecordOptions : RecordOptions
    {
        /// <summary>
        /// This class allows you to set some options of the fixed length
        /// records but at runtime.
        /// With this options the library is more flexible than never.
        /// </summary>
        /// <param name="info">Record information</param>
        internal FixedRecordOptions(IRecordInfo info)
            : base(info) {}


        /// <summary>
        /// Indicates the behavior when variable length records are found in a
        /// [<see cref="FixedLengthRecordAttribute"/>]. (Note: nothing in
        /// common with [FieldOptional])
        /// </summary>
        public FixedMode FixedMode
        {
            get { return ((FixedLengthField) mRecordInfo.Fields[0]).FixedMode; }
            set
            {
                for (int i = 0; i < mRecordInfo.FieldCount; i++)
                    ((FixedLengthField) mRecordInfo.Fields[i]).FixedMode = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int mRecordLength = int.MinValue;

        /// <summary>
        /// The sum of the individual field lengths.
        /// </summary>
        public int RecordLength
        {
            get
            {
                if (mRecordLength != int.MinValue)
                    return mRecordLength;

                mRecordLength = 0;
                foreach (FixedLengthField field in mRecordInfo.Fields)
                    mRecordLength += field.FieldLength;

                return mRecordLength;
            }
        }
    }
}