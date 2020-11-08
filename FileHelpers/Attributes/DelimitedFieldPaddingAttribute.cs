using System;
 

namespace FileHelpers
{
    /// <summary>
    /// Supports padding when applied to a Delimited record field or property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DelimitedFieldPaddingAttribute : Attribute
    {
        /// <summary>
        /// Total length of the fields in the output file.
        /// </summary>
        public int TotalLength { get; private set; }

        /// <summary>
        /// The position of the alignment.
        /// </summary>
        public AlignMode AlignMode { get; private set; }

        /// <summary>
        /// The character for padding.
        /// </summary>
        public char PaddingChar { get; private set; }


        /// <summary>
        /// Adds padding for a delimited file record field
        /// </summary>
        /// <param name="totalLength">Total length</param>
        /// <param name="alignMode">Alignment position</param>
        /// <param name="paddingChar">Character used for padding</param>
        public DelimitedFieldPaddingAttribute(int totalLength, AlignMode alignMode, char paddingChar)
        {

            this.PaddingChar = paddingChar;

            this.TotalLength = totalLength;

            this.AlignMode = alignMode;

        }
    }
}
