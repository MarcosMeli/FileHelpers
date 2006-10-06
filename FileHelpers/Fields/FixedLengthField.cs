#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System.Reflection;

namespace FileHelpers
{
	internal sealed class FixedLengthField : FieldBase
	{
		#region "  Properties  "

		internal int mLength;
		internal FieldAlignAttribute mAlign = new FieldAlignAttribute(AlignMode.Left, ' ');

		#endregion

		#region "  Constructor  "

		internal FixedLengthField(FieldInfo fi, int length, FieldAlignAttribute align) : base(fi)
		{
			this.mLength = length;

			if (align != null)
				this.mAlign = align;
		}

		#endregion

		#region "  Overrides String Handling  "

		protected override ExtractedInfo ExtractFieldString(string from, ForwardReader reader)
		{
			if (from.Length == 0)
			{
				if (mIsOptional)
					return ExtractedInfo.Empty;
				else
					throw new BadUsageException("End Of Line found processing the field: " + mFieldInfo.Name + " at line "+ reader.LineNumber.ToString() + ". (You need to mark it as [FieldOptional] if you want to avoid this exception)");
			}
			
			ExtractedInfo res;

			if (from.Length < this.mLength)
				if (mFixedMode == FixedMode.AllowLessChars || mFixedMode == FixedMode.AllowVariableLength)
					res = new ExtractedInfo(from);
				else
					throw new BadUsageException("The string '" + from + "' (length " + from.Length.ToString() + ") at line "+ reader.LineNumber.ToString() + " has less chars than the defined for " + mFieldInfo.Name + " (" + mLength.ToString() + "). You can use the [FixedLengthRecord(FixedMode.AllowLessChars)] to avoid this problem.");
			else if (mIsLast && from.Length > mLength && mFixedMode != FixedMode.AllowMoreChars && mFixedMode != FixedMode.AllowVariableLength)
				throw new BadUsageException("The string '" + from + "' (length " + from.Length.ToString() + ") at line "+ reader.LineNumber.ToString() + " has more chars than the defined for the last field " + mFieldInfo.Name + " (" + mLength.ToString() + ").You can use the [FixedLengthRecord(FixedMode.AllowMoreChars)] to avoid this problem.");
			else
				res = new ExtractedInfo(from.Substring(0, this.mLength));

			return res;
		}

		protected override string CreateFieldString(object record)
		{
			string res;
			res = base.CreateFieldString(record);

			if (res.Length > this.mLength)
			{
				res = res.Substring(0, this.mLength);
			}

			if (mAlign.Align == AlignMode.Left)
				res = res.PadRight(mLength, mAlign.AlignChar);
			else if (mAlign.Align == AlignMode.Right)
				res = res.PadLeft(mLength, mAlign.AlignChar);
			else
			{
				int middle = (mLength - res.Length)/2;
				if (middle > 0)
					res = res.PadLeft(mLength - middle, mAlign.AlignChar).PadRight(mLength, mAlign.AlignChar);
			}

			return res;
		}

		#endregion
	}
}