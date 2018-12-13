using System.Reflection;
using System.Text;
using FileHelpers.Helpers;

namespace FileHelpers
{
    /// <summary>
    /// Fixed length field that has length and alignment
    /// </summary>
    public sealed class FixedLengthField
        : FieldBase
    {
        #region "  Properties  "

        /// <summary>
        /// Field length of this field in the record
        /// </summary>
        internal int FieldLength { get; private set; }

        /// <summary>
        /// Alignment of this record
        /// </summary>
        internal FieldAlignAttribute Align { get; private set; }

        /// <summary>
        /// Whether we allow more or less characters to be handled
        /// </summary>
        internal FixedMode FixedMode { get; set; }

        #endregion

        #region "  Constructor  "

        /// <summary>
        /// Simple fixed length field constructor
        /// </summary>
        private FixedLengthField() {}

        /// <summary>
        /// Create a fixed length field from field information
        /// </summary>
        /// <param name="fi">Field definitions</param>
        /// <param name="length">Length of this field</param>
        /// <param name="align">Alignment, left or right</param>
        /// <param name="defaultCultureName">Default culture name used for each properties if no converter is specified otherwise. If null, the default decimal separator (".") will be used.</param>
        internal FixedLengthField(FieldInfo fi, int length, FieldAlignAttribute align, string defaultCultureName=null)
            : base(fi, defaultCultureName)
        {
            FixedMode = FixedMode.ExactLength;
            Align = new FieldAlignAttribute(AlignMode.Left, ' ');
            FieldLength = length;

            if (align != null)
                Align = align;
            else {
                if (TypeHelper.IsNumericType(fi.FieldType))
                    Align = new FieldAlignAttribute(AlignMode.Right, ' ');
            }
        }

        #endregion

        #region "  Overrides String Handling  "

        /// <summary>
        /// Get the value from the record
        /// </summary>
        /// <param name="line">line to extract from</param>
        /// <returns>Information extracted from record</returns>
        internal override ExtractedInfo ExtractFieldString(LineInfo line)
        {
            if (line.CurrentLength == 0) {
                if (IsOptional)
                    return ExtractedInfo.Empty;
                else {
                    throw new BadUsageException("End Of Line found processing the field: " + FieldInfo.Name +
                                                " at line " + line.mReader.LineNumber.ToString()
                                                +
                                                ". (You need to mark it as [FieldOptional] if you want to avoid this exception)");
                }
            }

            //ExtractedInfo res;

            if (line.CurrentLength < FieldLength) {
                if (FixedMode == FixedMode.AllowLessChars ||
                    FixedMode == FixedMode.AllowVariableLength)
                    return new ExtractedInfo(line);
                else {
                    throw new BadUsageException("The string '" + line.CurrentString + "' (length " +
                                                line.CurrentLength.ToString() + ") at line "
                                                + line.mReader.LineNumber.ToString() +
                                                " has less chars than the defined for " + FieldInfo.Name
                                                + " (" + FieldLength.ToString() +
                                                "). You can use the [FixedLengthRecord(FixedMode.AllowLessChars)] to avoid this problem.");
                }
            }
            else if (line.CurrentLength > FieldLength &&
                     IsArray == false &&
                     IsLast &&
                     FixedMode != FixedMode.AllowMoreChars &&
                     FixedMode != FixedMode.AllowVariableLength) {
                throw new BadUsageException("The string '" + line.CurrentString + "' (length " +
                                            line.CurrentLength.ToString() + ") at line "
                                            + line.mReader.LineNumber.ToString() +
                                            " has more chars than the defined for the last field "
                                            + FieldInfo.Name + " (" + FieldLength.ToString() +
                                            ").You can use the [FixedLengthRecord(FixedMode.AllowMoreChars)] to avoid this problem.");
            }
            else
                return new ExtractedInfo(line, line.mCurrentPos + FieldLength);
        }

        /// <summary>
        /// Create a fixed length string representation (pad it out or truncate it)
        /// </summary>
        /// <param name="sb">buffer to add field to</param>
        /// <param name="fieldValue">value we are updating with</param>
        /// <param name="isLast">Indicates if we are processing last field</param>
        internal override void CreateFieldString(StringBuilder sb, object fieldValue, bool isLast)
        {
            string field = base.CreateFieldString(fieldValue);

            // Discard longer field values
            if (field.Length > FieldLength)
                field = field.Substring(0, FieldLength);

            if (Align.Align == AlignMode.Left) {
                sb.Append(field);
                sb.Append(Align.AlignChar, FieldLength - field.Length);
            }
            else if (Align.Align == AlignMode.Right) {
                sb.Append(Align.AlignChar, FieldLength - field.Length);
                sb.Append(field);
            }
            else {
                int middle = (FieldLength - field.Length)/2;

                sb.Append(Align.AlignChar, middle);
                sb.Append(field);
                sb.Append(Align.AlignChar, FieldLength - field.Length - middle);
//				if (middle > 0)
//					res = res.PadLeft(mFieldLength - middle, mAlign.AlignChar).PadRight(mFieldLength, mAlign.AlignChar);
            }
        }

        /// <summary>
        /// Create a clone of the fixed length record ready to get updated by
        /// the base settings
        /// </summary>
        /// <returns>new fixed length field definition just like this one minus
        /// the base settings</returns>
        protected override FieldBase CreateClone()
        {
            var res = new FixedLengthField {
                Align = Align,
                FieldLength = FieldLength,
                FixedMode = FixedMode
            };
            return res;
        }

        #endregion
    }
}