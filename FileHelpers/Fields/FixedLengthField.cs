

using System.Reflection;
using System;
using System.Text;

namespace FileHelpers
{
	internal sealed class FixedLengthField
        : FieldBase
	{

		#region "  Properties  "

        internal int FieldLength { get; private set; }
        internal FieldAlignAttribute Align { get; private set; }
        internal FixedMode FixedMode { get; set; }

	    #endregion

		#region "  Constructor  "

        private FixedLengthField()
            :base()
        {
        }

	    internal FixedLengthField(FieldInfo fi, int length, FieldAlignAttribute align) : base(fi)
		{
	        FixedMode = FixedMode.ExactLength;
	        Align = new FieldAlignAttribute(AlignMode.Left, ' ');
	        this.FieldLength = length;

			if (align != null)
				this.Align = align;
			else
			{
                if (TypeHelper.IsNumericType(fi.FieldType))
					Align = new FieldAlignAttribute(AlignMode.Right, ' ');
			}
		}


		#endregion

		#region "  Overrides String Handling  "

        internal override ExtractedInfo ExtractFieldString(LineInfo line)
		{
			if (line.CurrentLength == 0)
			{
				if (IsOptional)
					return ExtractedInfo.Empty;
				else
					throw new BadUsageException("End Of Line found processing the field: " + FieldInfo.Name + " at line "+ line.mReader.LineNumber.ToString() + ". (You need to mark it as [FieldOptional] if you want to avoid this exception)");
			}
			
			ExtractedInfo res;

			if (line.CurrentLength < this.FieldLength)
				if (FixedMode == FixedMode.AllowLessChars || 
					FixedMode == FixedMode.AllowVariableLength)
					res = new ExtractedInfo(line);
				else
					throw new BadUsageException("The string '" + line.CurrentString + "' (length " + line.CurrentLength.ToString() + ") at line "+ line.mReader.LineNumber.ToString() + " has less chars than the defined for " + FieldInfo.Name + " (" + FieldLength.ToString() + "). You can use the [FixedLengthRecord(FixedMode.AllowLessChars)] to avoid this problem.");
			else if (line.CurrentLength > FieldLength  && 
						IsArray == false &&
                        IsLast &&
				        FixedMode != FixedMode.AllowMoreChars && 
						FixedMode != FixedMode.AllowVariableLength)
				throw new BadUsageException("The string '" + line.CurrentString + "' (length " + line.CurrentLength.ToString() + ") at line "+ line.mReader.LineNumber.ToString() + " has more chars than the defined for the last field " + FieldInfo.Name + " (" + FieldLength.ToString() + ").You can use the [FixedLengthRecord(FixedMode.AllowMoreChars)] to avoid this problem.");
			else
				res = new ExtractedInfo(line, line.mCurrentPos + FieldLength);

			return res;
		}

        internal override void CreateFieldString(StringBuilder sb, object fieldValue)
		{
			string field = base.CreateFieldString(fieldValue);

            // Discard longer field values
			if (field.Length > FieldLength)
				field = field.Substring(0, FieldLength); 

			if (Align.Align == AlignMode.Left)
			{
				sb.Append(field);
				sb.Append(Align.AlignChar, FieldLength - field.Length);
			}
			else if (Align.Align == AlignMode.Right)
			{
				sb.Append(Align.AlignChar, FieldLength - field.Length);
				sb.Append(field);
			}
			else
			{
				int middle = (FieldLength - field.Length) / 2;

				sb.Append(Align.AlignChar, middle);
				sb.Append(field);
				sb.Append(Align.AlignChar,  FieldLength - field.Length - middle);
//				if (middle > 0)
//					res = res.PadLeft(mFieldLength - middle, mAlign.AlignChar).PadRight(mFieldLength, mAlign.AlignChar);
			}
		}

	    protected override FieldBase CreateClone()
	    {
	        var res = new FixedLengthField();
	        res.Align = Align;
	        res.FieldLength = FieldLength;
	        res.FixedMode = FixedMode;
	        return res;
	    }

	    #endregion
	}
}