using FileHelpers.RunTime;

namespace FileHelpers.Detection
{
    public class FormatOption
    {
        private int mConfidence = 0;

        public int Confidence
        {
            get { return mConfidence; }
            set { mConfidence = value; }
        }

        internal ClassBuilder mClassBuilder;

        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
        }

    }
}