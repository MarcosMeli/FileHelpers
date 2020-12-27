using System;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert an input value to a boolean,  allows for true false values
    /// </summary>
    internal sealed class BooleanConverter : ConverterBase
    {
        private readonly string mTrueString;
        private readonly string mFalseString;
        private readonly string mTrueStringLower;
        private readonly string mFalseStringLower;

        /// <summary>
        /// Simple boolean converter
        /// </summary>
        public BooleanConverter() { }

        /// <summary>
        /// Boolean converter with true false values
        /// </summary>
        /// <param name="trueStr">True string</param>
        /// <param name="falseStr">False string</param>
        public BooleanConverter(string trueStr, string falseStr)
        {
            mTrueString = trueStr;
            mFalseString = falseStr;
            mTrueStringLower = trueStr.ToLower();
            mFalseStringLower = falseStr.ToLower();
        }

        /// <summary>
        /// convert a string to a boolean value
        /// </summary>
        /// <param name="from">string to convert</param>
        /// <returns>boolean value</returns>
        public override object StringToField(string from)
        {
            object val;
            string testTo = from.ToLower();

            if (mTrueString == null)
            {
                testTo = testTo.Trim();
                switch (testTo)
                {
                    case "true":
                    case "1":
                    case "y":
                    case "t":
                        val = true;
                        break;

                    case "false":
                    case "0":
                    case "n":
                    case "f":

                    // I don't think that this case is possible without overriding the CustomNullHandling
                    // and it is possible that defaulting empty fields to be false is not correct
                    case "":
                        val = false;
                        break;

                    default:
                        throw new ConvertException(from,
                            typeof(bool),
                            "The string: " + from
                                           + " can't be recognized as boolean using default true/false values.");
                }
            }
            else
            {
                //  Most of the time the strings should match exactly.  To improve performance
                //  we skip the trim if the exact match is true
                if (testTo == mTrueStringLower)
                    val = true;
                else if (testTo == mFalseStringLower)
                    val = false;
                else
                {
                    testTo = testTo.Trim();
                    if (testTo == mTrueStringLower)
                        val = true;
                    else if (testTo == mFalseStringLower)
                        val = false;
                    else
                    {
                        throw new ConvertException(from,
                            typeof(bool),
                            "The string: " + from
                                           + " can't be recognized as boolean using the true/false values: " + mTrueString + "/" +
                                           mFalseString);
                    }
                }
            }

            return val;
        }

        /// <summary>
        /// Convert to a true false string
        /// </summary>
        public override string FieldToString(object from)
        {
            bool b = Convert.ToBoolean(from);
            if (b)
            {
                if (mTrueString == null)
                    return "True";
                else
                    return mTrueString;
            }
            else if (mFalseString == null)
                return "False";
            else
                return mFalseString;
        }
    }
}