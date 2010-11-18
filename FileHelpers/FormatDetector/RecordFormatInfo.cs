using FileHelpers.Dynamic;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Describes the format of a record
    /// </summary>
    public sealed class RecordFormatInfo
    {
        /// <summary>
        /// A number between 0 to 100 that indicates how acurate is the Record Format
        /// </summary>
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

        /// <summary>
        /// The <see cref="FixedLengthClassBuilder"/> casted version of the <see cref="ClassBuilder"/> or null if other builder.
        /// </summary>
        public FixedLengthClassBuilder ClassBuilderAsFixed
        {
            get { return mClassBuilder as FixedLengthClassBuilder; }
        }

        /// <summary>
        /// The <see cref="DelimitedClassBuilder"/> casted version of the <see cref="ClassBuilder"/> or null if other builder.
        /// </summary>
        public DelimitedClassBuilder ClassBuilderAsDelimited
        {
            get { return mClassBuilder as DelimitedClassBuilder; }
        }
    }
}