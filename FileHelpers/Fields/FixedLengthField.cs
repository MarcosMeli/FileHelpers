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
			if (mIsOptional && from.Length == 0 )
				return ExtractedInfo.Empty;

			ExtractedInfo res;

			if (from.Length < this.mLength)
				throw new FileHelperException("The string '" + from + "' (length " + from.Length.ToString() + ") for the field " + mFieldInfo.Name + " don´t match the record length: " + mLength.ToString());
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