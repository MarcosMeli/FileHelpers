#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System.Reflection;
using System;
using System.Text;

namespace FileHelpers
{
	internal sealed class FixedLengthField : FieldBase
	{


		#region "  Properties  "

		internal int mFieldLength;
		internal FieldAlignAttribute mAlign = new FieldAlignAttribute(AlignMode.Left, ' ');

		internal FixedMode mFixedMode = FixedMode.ExactLength;


		#endregion

		#region "  Constructor  "

		internal FixedLengthField(FieldInfo fi, int length, FieldAlignAttribute align) : base(fi)
		{
			this.mFieldLength = length;

			if (align != null)
				this.mAlign = align;
			else
			{
                if (IsNumericType(fi.FieldType))
					mAlign = new FieldAlignAttribute(AlignMode.Right, ' ');
			}
		}

        private bool IsNumericType(Type type)
        {
            return  type == typeof(Int16) ||
                    type == typeof(Int32) ||
                    type == typeof(Int64) ||
                    type == typeof(UInt16) ||
                    type == typeof(UInt32) ||
                    type == typeof(UInt64) ||
                    type == typeof(byte) ||
                    type == typeof(sbyte) ||
                    type == typeof(decimal) ||
                    type == typeof(float) ||
                    type == typeof(double) ||
                    type == typeof(Int16?) ||
                    type == typeof(Int32?) ||
                    type == typeof(Int64?) ||
                    type == typeof(UInt16?) ||
                    type == typeof(UInt32?) ||
                    type == typeof(UInt64?) ||
                    type == typeof(byte?) ||
                    type == typeof(sbyte?) ||
                    type == typeof(decimal?) ||
                    type == typeof(float?) ||
                    type == typeof(double?);
        }

		#endregion

		#region "  Overrides String Handling  "

        internal override ExtractedInfo ExtractFieldString(LineInfo line)
		{
			if (line.CurrentLength == 0)
			{
				if (mIsOptional)
					return ExtractedInfo.Empty;
				else
					throw new BadUsageException("End Of Line found processing the field: " + mFieldInfo.Name + " at line "+ line.mReader.LineNumber.ToString() + ". (You need to mark it as [FieldOptional] if you want to avoid this exception)");
			}
			
			ExtractedInfo res;

			if (line.CurrentLength < this.mFieldLength)
				if (mFixedMode == FixedMode.AllowLessChars || 
					mFixedMode == FixedMode.AllowVariableLength)
					res = new ExtractedInfo(line);
				else
					throw new BadUsageException("The string '" + line.CurrentString + "' (length " + line.CurrentLength.ToString() + ") at line "+ line.mReader.LineNumber.ToString() + " has less chars than the defined for " + mFieldInfo.Name + " (" + mFieldLength.ToString() + "). You can use the [FixedLengthRecord(FixedMode.AllowLessChars)] to avoid this problem.");
			else if (line.CurrentLength > mFieldLength  && 
						mIsArray == false &&
                        mIsLast &&
				        mFixedMode != FixedMode.AllowMoreChars && 
						mFixedMode != FixedMode.AllowVariableLength)
				throw new BadUsageException("The string '" + line.CurrentString + "' (length " + line.CurrentLength.ToString() + ") at line "+ line.mReader.LineNumber.ToString() + " has more chars than the defined for the last field " + mFieldInfo.Name + " (" + mFieldLength.ToString() + ").You can use the [FixedLengthRecord(FixedMode.AllowMoreChars)] to avoid this problem.");
			else
				res = new ExtractedInfo(line, line.mCurrentPos + mFieldLength);

			return res;
		}

        internal override void CreateFieldString(StringBuilder sb, object fieldValue)
		{
			string field = base.CreateFieldString(fieldValue);

            // Discard longer field values
			if (field.Length > mFieldLength)
				field = field.Substring(0, mFieldLength); 

			if (mAlign.Align == AlignMode.Left)
			{
				sb.Append(field);
				sb.Append(mAlign.AlignChar, mFieldLength - field.Length);
			}
			else if (mAlign.Align == AlignMode.Right)
			{
				sb.Append(mAlign.AlignChar, mFieldLength - field.Length);
				sb.Append(field);
			}
			else
			{
				int middle = (mFieldLength - field.Length) / 2;

				sb.Append(mAlign.AlignChar, middle);
				sb.Append(field);
				sb.Append(mAlign.AlignChar,  mFieldLength - field.Length - middle);
//				if (middle > 0)
//					res = res.PadLeft(mFieldLength - middle, mAlign.AlignChar).PadRight(mFieldLength, mAlign.AlignChar);
			}
		}

		#endregion
	}
}