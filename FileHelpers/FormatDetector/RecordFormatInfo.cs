using FileHelpers.RunTime;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Describes the format of a record
    /// </summary>
    public sealed class RecordFormatInfo
    {
        internal int mConfidence = 0;

        /// <summary>
        /// A number between 0 to 100 that indicates how acurate is the Record Format
        /// </summary>
        public int Confidence
        {
            get { return mConfidence; }
        }

        internal ClassBuilder mClassBuilder;

        /// <summary>
        /// The <see cref="ClassBuilder"/> for the current format.
        /// </summary>
        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
        }

    }
}