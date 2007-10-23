using FileHelpers.RunTime;

namespace FileHelpers.Detection
{
    public class FormatOption
    {
        private int mCertainty;

        public int Certainty
        {
            get { return mCertainty; }
            set { mCertainty = value; }
        }

        internal ClassBuilder mClassBuilder;

        public ClassBuilder ClassBuilder
        {
            get { return mClassBuilder; }
        }

    }
}